using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotProjectile : Projectile
{
    public DamageElement delem;
    public bool doCrit = false;

    public override void DealDamage(EnemyController ec)
    {
        Debug.Log("dealtDamage");
        base.DealDamage(ec);
        if (doCrit)
        {
            foreach (DamageType dt in damages)
            {
                if (dt.damageElement == delem)
                {
                    ec.CritEffect(dt.value, ec.damageCrits[(int)delem - 1]);
                }
            }
        }
    }
}
