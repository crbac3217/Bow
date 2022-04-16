using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutenDishController : EnemyController
{
    public AiHandler ai;
    public EnemyController ec;

    public override void Setup()
    {
        base.Setup();
    }
    public override void OnDamageTaken()
    {
        base.OnDamageTaken();
        ai.AdditionalAttackTriggered("ShutenDish");
    }
    public override void UpdateHpUI()
    {
    }
}
