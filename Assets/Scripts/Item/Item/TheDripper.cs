using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TheDripper", menuName = "Modifier/Items/TheDripper", order = 110)]
public class TheDripper : Modifier
{
    int count;
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        count++;
        if (count >= 9)
        {
            da.hitObj.GetComponent<EnemyController>().minGoldDrop += 8;
            da.hitObj.GetComponent<EnemyController>().maxGoldDrop += 8;
            count = 0;
        }
    }
}
