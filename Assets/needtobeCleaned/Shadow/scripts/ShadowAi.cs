using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAi : BossAi
{
    public List<EnemyAttack> phase2attacksPref, phase2 = new List<EnemyAttack>();
    public bool buried, onGround;
    public GameObject digParticle;
    public float groundLevel;
    private Vector2 groundspritePos = new Vector2(0, 0.269f);
    private Vector2 airspritePos = new Vector2(0, 0.368f);
    public LayerMask ground;
    public void Dig()
    {
        GetComponent<Collider2D>().enabled = false;
        var dig = Instantiate(digParticle, transform.position, Quaternion.identity);
        campar.StartCoroutine(campar.CamShake(Vector2.up * 0.03f, 0.3f));
        buried = true;
        onGround = true;
    }
    public void Emerge()
    {
        GetComponent<Collider2D>().enabled = true;
        var dig = Instantiate(digParticle, transform.position, Quaternion.identity);
        campar.StartCoroutine(campar.CamShake(Vector2.up * 0.03f, 0.3f));
        buried = false;
        onGround = false;
    }
    public override void SetUp()
    {
        base.SetUp();
        foreach (EnemyAttack at in phase2attacksPref)
        {
            EnemyAttack temp = Instantiate(at);
            phase2.Add(temp);
            temp.aiHandler = this;
            temp.SetUp();
        }
        Vector2 raypos = new Vector2(visuals.transform.position.x, visuals.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(raypos, Vector2.down, ground);
        if (hit)
        {
            groundLevel = hit.point.y;
        }
        else
        {
            groundLevel = transform.position.y - 1;
        }
        buried = false;
    }
    public override void OnUpdate()
    {
        if (onGround)
        {
            if ((Vector2)visuals.transform.localPosition != groundspritePos)
            {
                visuals.transform.localPosition = Vector2.MoveTowards(visuals.transform.localPosition, groundspritePos, 0.05f);
                if (Vector2.Distance(visuals.transform.localPosition, groundspritePos) < 0.05f)
                {
                    visuals.transform.localPosition = groundspritePos;
                }
            }
            if (transform.position.y != groundLevel)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, groundLevel), 0.05f);
                if (Mathf.Abs(transform.position.y - groundLevel) < 0.05f)
                {
                    transform.position = new Vector2(transform.position.x, groundLevel);
                }
            }
        }
        else if(!onGround && (Vector2)visuals.transform.localPosition != airspritePos)
        {
            visuals.transform.localPosition = Vector2.MoveTowards(visuals.transform.localPosition, airspritePos,  0.05f);
            if (Vector2.Distance(visuals.transform.localPosition, airspritePos)<0.05f)
            {
                visuals.transform.localPosition = airspritePos;
            }
        }
    }
    public void ChangePhase()
    {
        attacks.Clear();
        anim.SetInteger("Phase", 2);
        anim.SetTrigger("ChangePhase");
        StopMovingNoDisrupt(1);
        attacks = new List<EnemyAttack>(phase2);
        chaseSpeed = 1.3f;
    }
}
