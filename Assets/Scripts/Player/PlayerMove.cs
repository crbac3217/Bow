using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Node nearestNode;
    public PlayerControl pc;
    public GameObject body;
    public LayerMask layerMask;
    public List<Modifier> moveMods = new List<Modifier>();
    public Vector2 modifiedVelocity = new Vector2();
    public float currentSpeed, maxSpeed, modifierSpeed, acceleration;
    public bool isRight, isMoving, rightPressed, leftPressed, dashing;

    public void Begin()
    {
        pc = GetComponent<PlayerControl>();
        currentSpeed = 0;
        if (transform.localScale.x > 0)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
            acceleration *= -1;
        }
        SetNode();
        StartCoroutine(ChangeNode());
    }
    private IEnumerator ChangeNode()
    {
        SetNode();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ChangeNode());
    }
    public void ResetStat()
    {
        maxSpeed = 1 + Mathf.Log(pc.stats[0].value);
    }
    private void FixedUpdate()
    {
        if (!pc.pf.compFrozen)
        {
            if (GetComponent<Rigidbody2D>().velocity.x > 0.01f && !isRight)
            {
                isRight = true;
                TurnAround();
            }
            else if (GetComponent<Rigidbody2D>().velocity.x < -0.01f && isRight)
            {
                isRight = false;
                TurnAround();
            }
            if (rightPressed)
            {
                acceleration += 0.01f;
            }
            if (leftPressed)
            {
                acceleration -= 0.01f;
            }
            acceleration = Mathf.Clamp(acceleration, -0.02f, 0.02f);
            if (GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(currentSpeed + modifierSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            if (isMoving)
            {
                Move();
            }
        }
        else
        {
            if (dashing)
            {
                GetComponent<Rigidbody2D>().velocity = modifiedVelocity;
            }
            
        }
        if (transform.position.y < -5 && !pc.pf.compFrozen)
        {
            pc.ph.Dead();
        }
    }
    public void SetNode()
    {
        Node temp = null;
        float minval = 2;
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, 1, layerMask))
        {
            if (Vector2.Distance(transform.position, col.transform.position) < minval)
            {
                minval = Vector2.Distance(transform.position, col.transform.position);
                temp = col.gameObject.GetComponent<Node>();
            }
        }
        if (!temp)
        {
            return;
        }
        else
        {
            nearestNode = temp;
        }
    }
    public void Move()
    {
        if (acceleration > 0)
        {
            currentSpeed = Mathf.Clamp(currentSpeed + pc.stats[0].value * acceleration, maxSpeed/2, maxSpeed);
        }
        else
        {
            currentSpeed = Mathf.Clamp(currentSpeed + pc.stats[0].value * acceleration, -maxSpeed, - maxSpeed/2);
        }
        pc.pa.bodyAnim.SetBool("isRunning", true);
        pc.pa.headAnim.SetBool("isRunning", true);
    }
    public void MoveRight()
    {
        rightPressed = true;
        isMoving = true;
        foreach (Modifier mod in moveMods)
        {
            mod.OnModifierActive(pc);
        }
    }
    public void MoveLeft()
    {
        leftPressed = true;
        isMoving = true;
        foreach (Modifier mod in moveMods)
        {
            mod.OnModifierActive(pc);
        }
    }
    private void TurnAround()
    {
            body.transform.localScale = new Vector2(body.transform.localScale.x * -1, transform.localScale.y);
    }
    public void LetGoRight()
    {
        rightPressed = false;
        currentSpeed = 0;
        if (!leftPressed)
        {
            currentSpeed = 0;
            isMoving = false;
            pc.pa.bodyAnim.SetBool("isRunning", false);
            pc.pa.headAnim.SetBool("isRunning", false);
        }
    }
    public void LetGoLeft()
    {
        leftPressed = false;
        currentSpeed = 0;
        if (!rightPressed)
        {
            currentSpeed = 0;
            isMoving = false;
            pc.pa.bodyAnim.SetBool("isRunning", false);
            pc.pa.headAnim.SetBool("isRunning", false);
        }  
    }
}
