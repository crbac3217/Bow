using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutenDishController : EnemyController
{
    public AiHandler ai;
    public EnemyController ec;

    public override void Setup()
    {

    }
    public override void OnDamageTaken()
    {
        ai.AdditionalAttackTriggered("ShutenDish");
    }
    public override void UpdateHpUI()
    {
    }
}
