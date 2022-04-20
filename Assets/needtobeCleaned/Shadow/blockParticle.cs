using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockParticle : EnemyParticle
{
    public float liveSec;
    private Collider2D col;
    public override void SetUp()
    {
        StartCoroutine(Live());
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }
    public void ActivateCollider()
    {
        col.enabled = true;
    }
    private IEnumerator Live()
    {
        yield return new WaitForSeconds(liveSec);
        GetComponent<Animator>().SetTrigger("End");
    }
}
