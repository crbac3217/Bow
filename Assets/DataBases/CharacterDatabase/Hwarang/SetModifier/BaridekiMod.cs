using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Barideki", menuName = "Modifier/Sets/Barideki", order = 110)]
public class BaridekiMod : Modifier
{
    public int amount;
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        da.epc.Heal(amount);
    }
}
