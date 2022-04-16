using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "SoulHarvester", menuName = "Modifier/Items/SoulHarvester", order = 110)]
public class SoulHarvester : Modifier
{
    public int chanceMax;
    public float healMultiplier;
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        int trig = UnityEngine.Random.Range(0, chanceMax);
        if (trig == 1)
        {
            int amount = (int)Mathf.Max(da.damageAmount * healMultiplier, 1);
            da.epc.Heal(amount);
        }
    }
}