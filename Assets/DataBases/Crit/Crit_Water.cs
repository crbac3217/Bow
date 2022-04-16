using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crit_Water", menuName = "Crits/Hwarang/Water", order = 110)]
public class Crit_Water : Crit
{
    private Dictionary<DamageElement, int> prev = new Dictionary<DamageElement, int>();
    public override void StatusEffect(int value, EnemyController ec)
    {
        base.StatusEffect(value, ec);
        float percentage = 8 / (8 + value);
        foreach (DamageType dtype in ec.strength)
        {
            int prevVal = dtype.value;
            int newVal = Mathf.RoundToInt(Mathf.Clamp(prevVal * percentage, 1, prevVal - 1));
            dtype.value = newVal;
            prev.Add(dtype.damageElement, prevVal - newVal);
        }
    }
    public override void RemoveStatusEffect(EnemyController ec)
    {
        foreach (DamageType dtype in ec.strength)
        {
            dtype.value += prev[dtype.damageElement];
        }
        base.RemoveStatusEffect(ec);
    }
}
