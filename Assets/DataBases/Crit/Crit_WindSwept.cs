using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crit_WindSwept", menuName = "Crits/Hwarang/WindSwept", order = 110)]
public class Crit_WindSwept : Crit
{
    private int valuet = 0;
    public override void StatusEffect(int value, EnemyController ec)
    {
        valuet = Mathf.Clamp(Mathf.RoundToInt(value / 2), 1, 100);
        for (int i = 0; i < 6; i++)
        {
            DamageType temp = new DamageType
            {
                damageElement = (DamageElement)i,
                value = valuet
            };
            bool exist = false;
            foreach (DamageType ew in ec.weakness)
            {
                if (temp.damageElement == ew.damageElement)
                {
                    exist = true;
                    ew.value += temp.value;
                }
            }
            if (!exist)
            {
                ec.weakness.Add(temp);
            }
        }
    }
    public override void RemoveStatusEffect(EnemyController ec)
    {
        for (int i = 5; i >= 0; i--)
        {
            ec.weakness[i].value -= valuet;
        }
        base.RemoveStatusEffect(ec);
    }
}
