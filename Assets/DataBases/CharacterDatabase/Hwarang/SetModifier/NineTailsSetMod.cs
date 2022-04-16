using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NineTailsSetMod", menuName = "Modifier/Sets/NineTailsSetMod", order = 110)]
public class NineTailsSetMod : Modifier
{
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        int amount = Mathf.Min((int)da.damageAmount / 20, 1);
        da.epc.Heal(amount);
    }
}
