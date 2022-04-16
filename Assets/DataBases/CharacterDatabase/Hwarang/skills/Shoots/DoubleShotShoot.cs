using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleShotShoot", menuName = "Shoot/Hwarang/DoubleShot", order = 110)]
public class DoubleShotShoot : Shoot
{
    public float additionalDamageMultiplier;
    public override void InvokeShoot(AttackArgs Aa)
    {
        DoubleTriple dt = Aa.projectile.GetComponent<DoubleTriple>();
        dt.delem = DamageElement.None;
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
