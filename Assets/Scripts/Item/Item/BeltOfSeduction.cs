using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeltOfSeduction", menuName = "Modifier/Items/BeltOfSeduction", order = 110)]
public class BeltOfSeduction : Modifier
{
    public int chanceMax;
    public float healMultiplier;
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        int trig = UnityEngine.Random.Range(1, chanceMax);
        if (trig == 1)
        {
            int amount = (int)Mathf.Max(da.damageAmount * healMultiplier, 1);
            da.epc.Heal(amount);
        }
    }
}
