using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AiHandler : MonoBehaviour
{
    [Header("Variables")]
    public AggressionType aggressionType;
    public int damage;
    public bool canJump;
    public bool canFly;
    public bool isAffectedByCC;
    public float defaultSpeed;
    public float checkInterval;
    public float weight;
    public float chaseSpeed;
    public float detectionDistance;
    public float kiteDistance;
    public float jumpStr;
    public LayerMask playerLayer, nodeLayer;
    [Header("Objects")]
    public List<EnemyAttack> attacksPrefab = new List<EnemyAttack>();
    public EnemyController ec;
    public GameObject visuals;
    [Header("ForDebugPurposes")]
    public List<EnemyAttack> attacks = new List<EnemyAttack>();
    public PlayerControl pc;
    public bool isInteracting;
    private Path currentPath = new Path();
    private NodeEvaluator tempEval;
    private List<NodeEvaluator> openList = new List<NodeEvaluator>();
    private List<NodeEvaluator> closedList = new List<NodeEvaluator>();
    public Animator anim;
    private SpriteRenderer spren;
    private Rigidbody2D rb;
    private Collider2D col;
    private float speed;
    private bool canMove = true;
    private int snareStack = 0;
    private bool isGrounded = false;
    public Node curNode, nextNode;
    private Connection curCon;
    private bool disappear;
    private float height;
    private float width;
    private bool done;
    private Path tempPath = new Path();
    private int calls = 0;
    public float nodeSearch = 2;
    public AudioClip jumpClip, hitClip, deadClip;
    public AudioSource voiceSource, foleySource;
    public List<AudioClip> foleys, voices = new List<AudioClip>();
    public List<string> foleyNames, voiceNames = new List<string>();
    private Dictionary<string, AudioClip> foleyDic = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> voiceDic = new Dictionary<string, AudioClip>();

    private void Start()
    {
        voiceSource = GetComponent<AudioSource>();
        foleySource = visuals.GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
        visuals.GetComponent<AnimationTriggerReceiver>().ai = this;
        speed = defaultSpeed;
        rb = GetComponent<Rigidbody2D>();
        spren = visuals.GetComponent<SpriteRenderer>();
        anim = visuals.GetComponent<Animator>();
        ec = GetComponent<EnemyController>();
        height = transform.localScale.y * col.bounds.extents.y;
        width = transform.localScale.x * col.bounds.extents.x;
        foreach (EnemyAttack at in attacksPrefab)
        {
            EnemyAttack temp = Instantiate(at);
            attacks.Add(temp);
            temp.aiHandler = this;
            temp.SetUp();
        }
        if (foleys.Count > 0)
        {
            for (int i = 0; i < foleys.Count; i++)
            {
                foleyDic.Add(foleyNames[i], foleys[i]);
            }
        }
        if (voices.Count > 0)
        {
            for (int i = 0; i < voices.Count; i++)
            {
                voiceDic.Add(voiceNames[i], voices[i]);
            }
        }
        SetUp();
        StartCoroutine(StopMoving(1));
        StartCoroutine(CheckPlayer());
    }
    public virtual void SetUp()
    {

    }
    private void FixedUpdate()
    {
        if (transform.position.y < -10)
        {
            if (col)
            {
                ec.Dead(false);
            }
        }
        if (canMove && nextNode)
        {
            MoveToNextNode();
        }
        if (disappear)
        {
            spren.color = new Color(spren.color.r, spren.color.g, spren.color.b, spren.color.a - 0.01f);
            if (spren.color.a < 0.03f)
            {
                Destroy(this.gameObject);
            }
        }
        OnUpdate();
    }
    public virtual void OnUpdate()
    {

    }
    #region StopMoveAndCC
    public void AddSnareStack()
    {
        snareStack++;
        anim.SetBool("Moving", false);
        rb.velocity = Vector2.zero;
        canMove = false;
    }
    public void RemoveSnareStack()
    {
        snareStack--;
        if (snareStack <= 0)
        {
            canMove = true;
        }
    }
    public void Hit()
    {
        if (isAffectedByCC && ec.hp > 0)
        {
            AddSnareStack();
            anim.SetTrigger("Hit");
        }
    }
    public virtual void Dead()
    {
        AddSnareStack();
        rb.bodyType = RigidbodyType2D.Static;
        Destroy(col);
        anim.SetTrigger("Death");
        StopCoroutine(CheckPlayer());
    }
    public void KnockBack(Vector2 originPos, float strength)
    {
        if (isAffectedByCC)
        {
            float multipliedVal = strength * 2 / weight;
            if (isAffectedByCC)
            {
                StartCoroutine(KnockB(originPos, multipliedVal));
            }
        }
    }
    private IEnumerator KnockB(Vector2 dir, float strength)
    {
        Hit();
        isAffectedByCC = false;
        if (dir.x > transform.position.x)
        {
            rb.velocity = Vector2.left * strength;
        }
        else
        {
            rb.velocity = Vector2.right * strength;
        }
        yield return new WaitForSeconds(1f);
        RemoveSnareStack();
        rb.velocity = Vector2.zero;
        isAffectedByCC = true;
    }
    public IEnumerator StopMoving(float time)
    {
        AddSnareStack();
        yield return new WaitForSeconds(time);
        RemoveSnareStack();
    }
    public IEnumerator StopMovingNoDisrupt(float time)
    {
        isAffectedByCC = false;
        AddSnareStack();
        yield return new WaitForSeconds(time);
        RemoveSnareStack();
        isAffectedByCC = true;
    }
    public void Snared(float time)
    {
        if (isAffectedByCC)
        {
            StartCoroutine(Snare(time));
        }
    }
    private IEnumerator Snare(float time)
    {
        isAffectedByCC = false;
        Hit();
        yield return new WaitForSeconds(time);
        RemoveSnareStack();
        isAffectedByCC = true;
    }
    public void DeadAnimFinished()
    {
        disappear = true;
        ec.Drop();
    }
    public IEnumerator HitAggro()
    {
        if (!isInteracting)
        {
            detectionDistance += 10;
            yield return new WaitForSeconds(1.5f);
            detectionDistance -= 10;
        }
        else
        {
            yield return null;
        }
        
    }
    #endregion StopMoveAndCC
    #region PathFinding
    public void SetCurNode()
    {
        curNode = CurrentNode();
    }
    private Node CurrentNode()
    {
        Node temp = null;
        float min = 2;
        if (canFly)
        {
            foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, nodeSearch, nodeLayer))
            {
                if (Vector2.Distance(col.transform.position, transform.position) < min)
                {
                    min = Vector2.Distance(col.transform.position, transform.position);
                    temp = col.GetComponent<Node>();
                }
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, LayerMask.NameToLayer("Ground"));
            Vector2 origin = new Vector2();
            if (hit)
            {
                origin = hit.point;
            }
            else
            {
                origin = transform.position;
            }
            foreach (Collider2D col in Physics2D.OverlapCircleAll(origin, nodeSearch, nodeLayer))
            {
                if (Vector2.Distance(col.transform.position, origin) < min)
                {
                    min = Vector2.Distance(col.transform.position, transform.position);
                    temp = col.GetComponent<Node>();
                }
            }
        }
        if (temp)
        {
            return temp;
        }
        else
        {
            //this can be polished
            if (col)
            {
                ec.Dead(false);
            }
            return null;
        }
    }
    public void CreatePath()
    {
        SetCurNode();
        currentPath = PathAStar(pc.pm.nearestNode);
        SetNextNodeInPath(curNode);
    }
    public void RefreshNodeInPath()
    {
        SetCurNode();
        SetNextNodeInPath(curNode);
    }
    public void SetNextNodeInPath(Node cur)
    {
        if (currentPath.nodes[currentPath.nodes.Count - 1] == cur)
        {
            if (SelectedAttack())
            {
                CheckAttackOrMove();
            }
            else
            {
                StopMoving(checkInterval);
            }
        }else if(!currentPath.nodes.Contains(cur))
        {
            StopMoving(checkInterval);
        }
        else
        {
            nextNode = NextNode(cur);
            if (nextNode == cur)
            {
                if (pc.transform.position.x > transform.position.x)
                {
                    visuals.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else
                {
                    visuals.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1f, transform.localScale.y);
                }
                StopMoving(checkInterval);
            }
            curCon = CurrentConnection();
        }
    }
    private Node NextNode(Node n)
    {
        //if (aggro == AggressionType.Melee)
        if (currentPath.nodes[currentPath.nodes.IndexOf(n) + 1] == pc.pm.nearestNode && SelectedAttack())
        {
            CheckAttackOrMove();
        }
        return currentPath.nodes[currentPath.nodes.IndexOf(n) + 1];
    }
    private Connection CurrentConnection()
    {
        foreach (Connection c in curNode.connections)
        {
            if (c.nodeTo == nextNode)
            {
                return c;
            }
        }
        return null;
    }
    private Path PathAStar(Node node)
    {
        done = false;
        openList.Clear();
        closedList.Clear();
        tempPath = new Path();
        calls = 0;
        NodeEvaluator start = new NodeEvaluator
        {
            node = curNode,
            ParentEval = 0,
            Hscore = 0,
            Gscore = 0,
            Fscore = 0
        };
        tempEval = start;
        closedList.Add(start);
        while (!done)
        {
            CheckSurroundingNode(node);
            if (openList.Count > 0)
            {
                MoveOntoNextNode(node);
            }
            else
            {
                EndIncomplete();
            }
            calls++;
        }
        return tempPath;
    }
    private void CheckSurroundingNode(Node node)
    {
        foreach (Connection con in tempEval.node.connections)
        {
            bool avail = true;
            if (con.jump)
            {
                if (!canJump || jumpStr < con.distance*2)
                {
                    avail = false;
                }
            }
            foreach (NodeEvaluator closed in closedList)
            {
                if (con.nodeTo == closed.node)
                {
                    avail = false;
                }
            }
            if (avail)
            {
                openList.Add(Neval(con.nodeTo, tempEval.Gscore, tempEval.node, node));
            }
        }
    }
    private NodeEvaluator Neval(Node n, float pGscore, Node pNode,Node EndNode)
    {
        var temp = new NodeEvaluator
        {
            node = n,
            pNode = pNode,
            ParentEval = pGscore,
            Hscore = Vector2.Distance(n.position, EndNode.position),
            Gscore = pGscore + Vector2.Distance(tempEval.node.position, n.position),
            Fscore = 0
        }; 
        return temp;
    }
    private void MoveOntoNextNode(Node node)
    {
        NodeEvaluator min = openList[0];
        foreach (NodeEvaluator neval in openList)
        {
            if (neval.Fscore < min.Fscore)
            {
                min = neval;
            }
        }
        openList.Remove(min);
        closedList.Add(min);
        tempEval = min;
        if (min.node == node)
        {
            Completed(min);
        } else if (calls >= 100)
        {
            EndIncomplete();
        }
    }
    private void Completed(NodeEvaluator min)
    {
        done = true;
        bool pathFin = false;
        NodeEvaluator evalToAdd = min;
        tempPath.totalDistance = min.Fscore;
        List<Node> nodesReversed = new List<Node>();
        while (!pathFin)
        {
            nodesReversed.Add(evalToAdd.node);
            if (evalToAdd.node == curNode)
            {
                pathFin = true;
            }
            foreach (NodeEvaluator neval in closedList)
            {
                if (neval.node == evalToAdd.pNode && neval.Gscore == evalToAdd.ParentEval)
                {
                    evalToAdd = neval;
                }
            };
        }
        for (int i = nodesReversed.Count - 1; i >= 0; i--)
        {
            tempPath.nodes.Add(nodesReversed[i]);
        }
        tempPath.didConnect = true;
    }
    private void EndIncomplete()
    {
        done = true;
        tempPath.didConnect = false;
        tempPath.nodes.Add(curNode);
        Node tempNode = curNode;
        int Counter = 5;
        while (Counter > 0)
        {
            bool finforNode = false;
            foreach (Connection con in tempNode.connections)
            {
                if (!con.jump && !finforNode && !tempPath.nodes.Contains(con.nodeTo))
                {
                    tempPath.nodes.Add(con.nodeTo);
                    tempPath.totalDistance += con.distance;
                    Counter--;
                    tempNode = con.nodeTo;
                    finforNode = true;
                }
            }
            if (!finforNode)
            {
                Counter = 0;
            }
        }
    }
    #endregion PathFinding
    #region Actions
    public virtual void MoveToNextNode()
    {
        if (canFly)
        {
            if (Vector2.Distance(transform.position, nextNode.position) <= 0.02f)
            {
                RefreshNodeInPath();
            }
            else
            {
                Vector2 dir = (nextNode.position - (Vector2)transform.position).normalized;
                if (dir.x > 0)
                {
                    visuals.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else
                {
                    visuals.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
                }
                rb.velocity = dir * speed;
                anim.SetBool("Moving", true);
            }
        }
        else
        {
            float horspeed = rb.velocity.x;
            if (Mathf.Abs(nextNode.position.x - transform.position.x) > 0.02f)
            {
                if (canJump)
                {
                    if (isGrounded)
                    {
                        if (nextNode.position.x > transform.position.x)
                        {
                            horspeed = speed;
                            anim.SetBool("Moving", true);
                            visuals.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                        }
                        else
                        {
                            horspeed = speed * -1;
                            anim.SetBool("Moving", true);
                            visuals.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
                        }
                    }
                }
                else
                {
                    if (nextNode.position.x > transform.position.x)
                    {
                        horspeed = speed;
                        anim.SetBool("Moving", true);
                        visuals.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    }
                    else
                    {
                        horspeed = speed * -1;
                        anim.SetBool("Moving", true);
                        visuals.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
                    }
                }
            }
            else
            {
                horspeed = 0;
                if (Mathf.Abs(nextNode.position.y - transform.position.y) < Mathf.Abs(height + 0.05f))
                {
                    RefreshNodeInPath();
                }
            }
            if (curCon.jump && isGrounded && horspeed != 0)
            {
                anim.SetTrigger("Jump");
                foleySource.clip = jumpClip;
                foleySource.Play();
                isGrounded = false;
                float xDist = Mathf.Abs(curNode.position.x - nextNode.position.x);
                float time = xDist / speed;
                float iniH = curNode.position.y - nextNode.position.y;
                float verspeed = (((9.81f * time * time) / 2) - iniH) / time;
                if (verspeed == float.NaN)
                {
                    verspeed = 0;
                }
                verspeed = Mathf.Clamp(verspeed + height, 0, jumpStr * 3);
                rb.velocity = new Vector2(horspeed, verspeed);
                isGrounded = false;
            }
            rb.velocity = new Vector2(horspeed, rb.velocity.y);
        }
    }
    public IEnumerator CheckPlayer()
    {
        isInteracting = CheckIfPlayerInRange();
        if (isInteracting)
        {
            speed = chaseSpeed;
            CheckAttackOrMove();
        }
        else
        {
            speed = defaultSpeed;
            anim.SetBool("Moving", false);
        }
        yield return new WaitForSeconds (checkInterval);
        StartCoroutine(CheckPlayer());
    }
    public bool CheckIfPlayerInRange()
    {
        if (Vector2.Distance(pc.transform.position, transform.position) < detectionDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void CheckAttackOrMove()
    {
        if (canMove)
        {
            EnemyAttack tempSel = SelectedAttack();
            if (tempSel)
            {
                tempSel.Activate();
                StartCoroutine(tempSel.CoolDown());
            }
            else
            {
                CreatePath();
            }
        }
    }
    private EnemyAttack SelectedAttack()
    {
        List<EnemyAttack> candidates = new List<EnemyAttack>();
        foreach (EnemyAttack at in attacks)
        {
            if (at.avail && at.range.avail)
            {
                candidates.Add(at);
            }
        }
        if (candidates.Count > 0)
        {
            int rand = Random.Range(0, candidates.Count);
            return candidates[rand];
        }
        else
        {
            return null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canFly)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                if (collision.contacts[0].point.y < transform.position.y && Mathf.Abs(collision.contacts[0].point.x - transform.position.x) < width / 2)
                {
                    if (!isGrounded)
                    {
                        isGrounded = true;
                        anim.SetTrigger("Land");
                    }
                }
                else
                {
                    if (!isGrounded && canJump)
                    {
                        KnockBack(collision.contacts[0].point, 0.5f);
                    }
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!canFly)
        {
            if (collision.gameObject.CompareTag("Ground") && !isGrounded)
            {
                if (collision.contacts[0].point.y < transform.position.y && Mathf.Abs(collision.contacts[0].point.x - transform.position.x) < width / 2)
                {
                    isGrounded = true;
                    anim.SetTrigger("Land");
                }
                else
                {
                    if (canJump)
                    {
                        KnockBack(collision.contacts[0].point, 0.5f);
                    }
                }
            }
        }
    }
    public void AttackTriggered(string attackName)
    {
        foreach (EnemyAttack at in attacks.ToList())
        {
            if (at.AttackName == attackName)
            {
                if (at.type == AttackType.Melee)
                {
                    if (isInteracting)
                    {
                        at.DealDamage(pc);
                    }
                }
                else if (at.type == AttackType.Shoot)
                {
                    if (isInteracting)
                    {
                        at.ShootProjectile(pc);
                    }
                }
                else if (at.type == AttackType.etc)
                {
                    if (isInteracting)
                    {
                        at.AttackEtc(pc);
                    }
                }
            }
        }
    }
    public void AdditionalAttackTriggered(string attackName)
    {
        foreach (EnemyAttack at in attacks)
        {
            if (at.AttackName == attackName)
            {
                at.AdditionalTrigger();
            }
        }
    }
    public void TriggerAnimation(string name, bool whileMove, float duration)
    {
        if (ec.hp > 0)
        {
            anim.SetTrigger(name);
            if (!whileMove)
            {
                StartCoroutine(StopMoving(duration));
            }
        }
    }
    public void TriggerAnimationNoDisrupt(string name, bool whileMove, float duration)
    {
        if (ec.hp > 0)
        {
            anim.SetTrigger(name);
            if (!whileMove)
            {
                StartCoroutine(StopMovingNoDisrupt(duration));
            }
        }
    }
    public void TriggerAdditionalAnimation(string attackName, bool whileMove, float time)
    {
        if (ec.hp > 0)
        {
            string tempName = attackName + "Trigger";
            anim.SetTrigger(tempName);
            if (!whileMove)
            {
                StartCoroutine(StopMoving(time));
            }
        }
    }
    #endregion Actions
    #region sound
    public void AttackSoundTriggered(string attackName)
    {
        foreach (EnemyAttack at in attacks.ToList())
        {
            if (at.AttackName == attackName)
            {
                foleySource.clip = at.attacknoises[at.noiseCounter];
                foleySource.Play();
                at.noiseCounter++;
                if (at.noiseCounter == at.attacknoises.Count)
                {
                    at.noiseCounter = 0;
                }
            }
        }
    }
    public void playVoice(string voiceName)
    {
        voiceSource.clip = voiceDic[voiceName];
        voiceSource.Play();
    }
    public void playFoley(string foleyName)
    {
        foleySource.clip = foleyDic[foleyName];
        foleySource.Play();
    }
    #endregion sound
}
[System.Serializable]
public class NodeEvaluator
{
    public Node node;
    public Node pNode;
    public float ParentEval;
    public float Fscore;
    public float Gscore;
    public float Hscore;
}
public enum AggressionType { Skittish = 0, Reactive = 1, Melee = 2, Shooting = 3, Kiting = 4, Boss = 5 }

