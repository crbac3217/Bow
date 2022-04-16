using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crit_Prank", menuName = "Crits/Hwarang/Prank", order = 111)]
public class Crit_Prank : Crit
{
    public PrankMod pm;
    public override void StatusEffect(int value, EnemyController ec)
    {
        base.StatusEffect(value, ec);
        pm = ScriptableObject.CreateInstance<PrankMod>();
        pm.ect = ec;
        ec.dm.killedMods.Add(pm);
    }
    public override void RemoveStatusEffect(EnemyController ec)
    {
        ec.dm.killedMods.Remove(pm);
        base.RemoveStatusEffect(ec);
    }
}

[CreateAssetMenu(fileName = "Prank", menuName = "Modifier/Crit/Prank", order = 110)]
public class PrankMod : Modifier
{
    public EnemyController ect;
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        if (da.hitObj == ect.gameObject)
        {
            da.hitObj.GetComponent<EnemyController>().minGoldDrop += 1;
            da.hitObj.GetComponent<EnemyController>().maxGoldDrop += 1;
        }
    }
}