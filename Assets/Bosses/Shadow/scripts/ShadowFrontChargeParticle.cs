using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFrontChargeParticle : EnemyParticle
{
    public Transform pos;
    private Collider2D col;
    public override void SetUp()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }
    public void ActivateCollider()
    {
        col.enabled = true;
    }
}
