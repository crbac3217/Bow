
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettedProjectile : Projectile
{
    public GameObject target;
    public Vector2 startPos, tempPos;
    public float speed, disp, findTargetRange;
    public bool ReachedTemp;
    public override void SetUp()
    {
        base.SetUp();
        ReachedTemp = false;
        startPos = transform.position;
        if (target)
        {
            target = target.GetComponent<AiHandler>().visuals;
            tempPos = Vector2.Perpendicular((Vector2)target.transform.position - startPos).normalized * disp;
        }
    }
    public override void Flying()
    {
        base.Flying();
        if (target)
        {
            Homing();
        }else if (!target)
        {
            ChangeTarget();
        }
    }
    public override void InvokeParticle(Color col)
    {
        onHitParticle.Play();
    }
    public void Homing()
    {
        Vector2 toTarget = Vector2.zero;
        if (Vector2.Distance(target.transform.position, transform.position) <= disp && !ReachedTemp)
        {
            toTarget = (tempPos - startPos).normalized;
        }
        else
        {
            if (!ReachedTemp)
            {
                ReachedTemp = true;
            }
            toTarget = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;
        }
        rb.velocity = toTarget * speed;
    }
    private void ChangeTarget()
    {
        target = NewEnemy();
        if (!target)
        {
            disappear = true;
            isHit = true;
        }
        else
        {
            target = target.GetComponent<AiHandler>().visuals;
            startPos = transform.position;
            tempPos = Vector2.Perpendicular((Vector2)target.transform.position - startPos).normalized * disp;
        }
    }
    private GameObject NewEnemy()
    {
        List<GameObject> candidates = new List<GameObject>();
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, findTargetRange))
        {
            if (col.CompareTag("Enemy"))
            {
                candidates.Add(col.gameObject);
            }
        }
        if (candidates.Count > 0)
        {
            return (candidates[Random.Range(0, candidates.Count)]);
        }
        else
        {
            return null;
        }
    }
}
