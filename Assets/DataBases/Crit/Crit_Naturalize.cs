using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crit_Naturalize", menuName = "Crits/Hwarang/Naturalize", order = 111)]
public class Crit_Naturalize : Crit
{
    public Naturalize naturalizeMod;
    public float multiplier;
    public override void StatusEffect(int value, EnemyController ec)
    {
        base.StatusEffect(value, ec);
        naturalizeMod = ScriptableObject.CreateInstance<Naturalize>();
        naturalizeMod.val = value;
        naturalizeMod.ect = ec;
        naturalizeMod.multiplier = this.multiplier;
        ec.dm.damagedMods.Add(naturalizeMod);
    }
    public override void RemoveStatusEffect(EnemyController ec)
    {
        ec.dm.damagedMods.Remove(naturalizeMod);
        base.RemoveStatusEffect(ec);
    }
}

public class Naturalize : Modifier
{
    public Crit parent;
    public float val, multiplier;
    public EnemyController ect;
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        if (da.hitObj == ect.gameObject)
        {
            int amount = Mathf.Max((int)(val * multiplier), 1);
            da.epc.Heal(amount);
        }
    }
}
