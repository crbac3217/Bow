using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "dokebi", menuName = "Modifier/Sets/dokebi", order = 110)]
public class DokebiMod : Modifier
{
    public Skill before;
    public Skill after;
    public override void OnModifierAdd(PlayerControl pc)
    {
        base.OnModifierAdd(pc);
        pc.ReplaceSkill(before.skillName, Instantiate(after));
    }
    public override AttackArgs AttackArgMod(AttackArgs aa)
    {
        aa.damageMult += Mathf.Min(aa.apc.gold * 0.01f, 0.5f);
        return base.AttackArgMod(aa);
    }
}
