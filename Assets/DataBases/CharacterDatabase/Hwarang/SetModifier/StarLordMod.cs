using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StarLordMod", menuName = "Modifier/Sets/StarLordMod", order = 110)]
public class StarLordMod : Modifier
{
    public Skill before, before1;
    public Skill after, after1;
    public override void OnModifierAdd(PlayerControl pc)
    {
        base.OnModifierAdd(pc);
        pc.ReplaceSkill(before.skillName, Instantiate(after));
        pc.ReplaceSkill(before1.skillName, Instantiate(after1));
    }
}
