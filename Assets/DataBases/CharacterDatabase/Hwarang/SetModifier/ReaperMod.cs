using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ReaperMod", menuName = "Modifier/Sets/ReaperMod", order = 110)]
public class ReaperMod : Modifier
{
    public Skill before;
    public Skill after;
    public override void OnModifierAdd(PlayerControl pc)
    {
        base.OnModifierAdd(pc);
        pc.ReplaceSkill(before.skillName, Instantiate(after));
    }
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        if (da.hitObj.GetComponent<EnemyController>().activeCrits.Count > 0)
        {
            List<DamageType> temp = new List<DamageType>();
            foreach (DamageType dt in da.epc.damageTypes)
            {
                DamageType tempd = new DamageType
                {
                    damageElement = dt.damageElement,
                    value = dt.value
                };
                temp.Add(tempd);
            }
            da.hitObj.GetComponent<EnemyController>().CalculateDamage(temp, false, 0);
        }
    }
}
