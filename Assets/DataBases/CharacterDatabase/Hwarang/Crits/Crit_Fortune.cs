using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crit_Fortune", menuName = "Crits/Hwarang/Fortune", order = 111)]
public class Crit_Fortune : Crit
{
    public AdPrankMod pm;
    public override void StatusEffect(int value, EnemyController ec)
    {
        base.StatusEffect(value, ec);
        pm = ScriptableObject.CreateInstance<AdPrankMod>();
        pm.ect = ec;
        ec.dm.killedMods.Add(pm);
    }
    public override void RemoveStatusEffect(EnemyController ec)
    {
        ec.dm.killedMods.Remove(pm);
        base.RemoveStatusEffect(ec);
    }
}

public class AdPrankMod : Modifier
{
    public Crit parent;
    public EnemyController ect;
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        if (da.hitObj == ect.gameObject)
        {
            da.hitObj.GetComponent<EnemyController>().minGoldDrop += 3;
            da.hitObj.GetComponent<EnemyController>().maxGoldDrop += 3;
        }
    }
}
