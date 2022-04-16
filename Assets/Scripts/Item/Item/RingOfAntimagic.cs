using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "RingOfAntimagic", menuName = "Modifier/Items/RingOfAntimagic", order = 110)]
public class RingOfAntimagic : Modifier
{
    public int counter, perHowmanyCounter, maxRate;

    public override void OnModifierAdd(PlayerControl pc)
    {
        base.OnModifierAdd(pc);
        pc.psm.SkillUsageGeneral += RingOfAntimagicResetCounter;
    }

    public override void OnModifierActive(PlayerControl pc)
    {
        base.OnModifierActive(pc);
        counter++;
    }
    public override AttackArgs AttackArgMod(AttackArgs aa)
    {
        float amount = (float)counter / (float) perHowmanyCounter;
        aa.damageMult += Mathf.Min(amount, maxRate);
        Debug.Log(aa.damageMult);
        return aa;
    }
    public void RingOfAntimagicResetCounter(object sender, SkillArgs e)
    {
        counter = 0;
    }
}
