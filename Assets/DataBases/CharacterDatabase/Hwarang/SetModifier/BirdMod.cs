using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Beik", menuName = "Modifier/Sets/Beik", order = 110)]
public class BirdMod : Modifier
{
    public int counter = 0;
    public Skill before;
    public Skill after;
    public GameObject Additional;

    public override void OnModifierAdd(PlayerControl pc)
    {
        base.OnModifierAdd(pc);
        pc.ReplaceSkill(before.skillName, Instantiate(after));
    }
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        counter++;
        if (counter == 2)
        {
            FireAdditional(da.epc, da.hitObj);
            counter = 0;
        }
    }
    private void FireAdditional(PlayerControl pc, GameObject hit)
    {
        var proj = Instantiate(Additional, pc.transform.position, Quaternion.identity);
        foreach (DamageType dt in pc.damageTypes)
        {
            DamageType temp = new DamageType 
            {
                damageElement = dt.damageElement,
                value = dt.value
            };
            proj.GetComponent<TargettedProjectile>().damages.Add(temp);
        }
        proj.GetComponent<TargettedProjectile>().target = hit;
    }
}
