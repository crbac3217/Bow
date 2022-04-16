using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TheSoulcaller", menuName = "Modifier/Items/TheSoulcaller", order = 110)]
public class TheSoulcaller : Modifier
{
    public int chanceMax;
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        int chance = Random.Range(0, chanceMax);
        if (chance == 1)
        {
            da.hitObj.GetComponent<EnemyController>().minGoldDrop += 1;
            da.hitObj.GetComponent<EnemyController>().maxGoldDrop += 1;
        }
    }
}
