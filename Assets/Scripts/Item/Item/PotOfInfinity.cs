using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PotOfInfinity", menuName = "Modifier/Items/PotOfInfinity", order = 110)]
public class PotOfInfinity : Modifier
{
    public float percentLostHealthToHeal;
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        float amount = (da.epc.stats[2].value - da.epc.currentHp) * percentLostHealthToHeal * 0.01f;
        da.epc.Heal((int)Mathf.Max(amount, 1));
    }
}
