using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HelmOfTheBlackDragon", menuName = "Modifier/Items/HelmOfTheBlackDragon", order = 110)]
public class HelmOfTheBlackDragon : Modifier
{
    public Crit fear;
    public int chanceMax;

    public override void OnEnemyModActive(EnemyArg da)
    {
        int chance = Random.Range(1, chanceMax);
        if (chance == 1)
        {
            base.OnEnemyModActive(da);
            da.hitObj.GetComponent<EnemyController>().CritEffect(1, fear);
        }
    }
}