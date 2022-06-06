using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAi : BossAi
{
    public override void SetUp()
    {
        foreach (EnemyAttack ea in base.attacks)
        {
            if (ea.name == "Golem Spawn")
            {
                StartCoroutine(ea.CoolDown());
            }
        }
        base.SetUp();
    }
}
