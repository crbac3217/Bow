using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowHomingProj : EnemyProjectile
{
    public override void SetUp()
    {
        base.SetUp();
        StartCoroutine(Home());
        Decel = true;
    }
    private IEnumerator Home()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 0.8f));
        Decel = false;
        Accel = true;
        dir = pc.transform.position - transform.position;
    }
}
