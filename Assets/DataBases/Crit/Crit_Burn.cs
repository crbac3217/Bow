using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crit_Burn", menuName = "Crits/Hwarang/Burn", order = 111)]
public class Crit_Burn : Crit
{
    public override void TickEffect(int value, EnemyController ec)
    {
        base.TickEffect(value, ec);
        List<DamageType> dt = new List<DamageType>();
        float multval = value * 0.75f;
        DamageType dtype = new DamageType
        {
            damageElement = DamageElement.Fire,
            value = (int)multval
        };
        dt.Add(dtype);
        ec.CalculateDamage(dt, false, 0);
    }
}
