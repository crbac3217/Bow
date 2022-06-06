using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAi : BossAi
{
    public override void SetUp()
    {
        base.SetUp();
        foreach (EnemyAttack ea in base.attacks)
        {
            if (ea.name == "GolemSpawn")
            {
                StartCoroutine(ea.CoolDown());
            }
        }
    }
}
