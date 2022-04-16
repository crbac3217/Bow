using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crit_Fear", menuName = "Crits/Hwarang/Fear", order = 110)]
public class Crit_Fear : Crit
{
    private Dictionary<DamageElement, int> prev = new Dictionary<DamageElement, int>();
    public override void StatusEffect(int value, EnemyController ec)
    {
        base.StatusEffect(value, ec);
        float percentage = 0.667f;
        foreach (DamageType dtype in ec.strength)
        {
            int prevVal = dtype.value;
            int newVal = Mathf.RoundToInt(Mathf.Clamp(prevVal * percentage, 1, prevVal - 1));
            dtype.value = newVal;
            prev.Add(dtype.damageElement, prevVal - newVal);
        }
        ec.aiHandler.Snared(2);
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
