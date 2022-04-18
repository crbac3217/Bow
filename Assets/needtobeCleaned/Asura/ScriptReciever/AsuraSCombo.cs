using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsuraSCombo", menuName = "EnemyAttack/Boss/Asura/AsuraSCombo", order = 105)]
public class AsuraSCombo : BetterDashAttack
{
    private int count;
    private bool mult = false;
    public override void Activate()
    {
        base.Activate();
        count = 0;
    }
    public override void AttackEtc(PlayerControl pc)
    {
        if (count == 2 && !mult)
        {
            damageMult *= 2;
        }
        base.AttackEtc(pc);
        if (mult)
        {
            damageMult /= 2;
        }
    }
    public override void AdditionalTrigger()
    {
        base.AdditionalTrigger();
        hasDashed = false;
        count++;
    }
}
