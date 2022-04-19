using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsuraProjFast : EnemyProjectile
{
    public override void SetUp()
    {
        base.SetUp();
        speed = Random.Range(speed - 2, speed);
        StartCoroutine(ChangeTargetted());
        Decel = true;
    }
    private IEnumerator ChangeTargetted()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        Decel = false;
        Targetted = true;
        Accel = true;
    }
}
