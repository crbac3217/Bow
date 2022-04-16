using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HwarangDefaultShoot", menuName = "Shoot/Hwarang/Default", order = 110)]
public class HwarangDefaultShoot : Shoot
{
    public override void InvokeShoot(AttackArgs aa)
    {
        
        GameObject arrow = aa.projectile;
        var arrowrigid = arrow.GetComponent<Rigidbody2D>();
        var arrowProj = arrow.GetComponent<HwarangDefaultProjectile>();
        foreach (DamageType damage in aa.apc.damageTypes)
        {
            if (damage.value > 0)
            {
                float multipliedval = damage.value + (damage.value * aa.multval * aa.accuracyVal);
                if (aa.damageMult > 0f)
                {
                    multipliedval += multipliedval * aa.damageMult;
                }
                DamageType tempDamage = new DamageType
                {
                    damageElement = damage.damageElement,
                    value = (int)multipliedval
                };
                arrowProj.damages.Add(tempDamage);
            }
        }
        arrowProj.accuracyMult = aa.apc.ps.accuracyMult;
        arrowProj.critChance = aa.apc.stats[3].value;
        arrowProj.accuracy = aa.apc.stats[4].value;
        arrowrigid.velocity = aa.dir.normalized * aa.multval * aa.speedMult;
        arrowProj.particleDisplacementValue = aa.heldPercent;
    }
}
