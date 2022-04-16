using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmpleShotShoot", menuName = "Shoot/Hwarang/AmpleShot", order = 110)]
public class AmpleShotShoot : Shoot
{
    public float additionalDamageMultiplier;
    public override void InvokeShoot(AttackArgs Aa)
    {
        AmpleShotPlayerside dt = Aa.projectile.GetComponent<AmpleShotPlayerside>();
        dt.dir = Aa.dir.normalized;
        foreach (DamageType damage in Aa.apc.damageTypes)
        {
            if (damage.value > 0)
            {
                float multipliedval = damage.value + (damage.value * Aa.multval * Aa.accuracyVal) + (damage.value * additionalDamageMultiplier);
                DamageType tempDamage = new DamageType
                {
                    damageElement = damage.damageElement,
                    value = (int)multipliedval
                };
                dt.damages.Add(tempDamage);
            }
        }
        skill.EndSkill(Aa.apc);
        skill.ShotSelectedToggleOff(Aa.apc);
    }
}