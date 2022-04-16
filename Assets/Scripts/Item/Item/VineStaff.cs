using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VineStaff", menuName = "Modifier/Items/VineStaff", order = 110)]
public class VineStaff : Modifier
{
    public int count, healAmount;

    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        count++;
        if (count >= 8)
        {
            da.epc.Heal(healAmount);
            count = 0;
        }
    }
}
