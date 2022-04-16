using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChakramOfContinuity", menuName = "Modifier/Items/ChakramOfContinuity", order = 110)]
public class ChakramOfContinuity : Modifier
{
    public override void OnModifierAdd(PlayerControl pc)
    {
        base.OnModifierAdd(pc);
        DamageType temp = new DamageType
        {
            damageElement = DamageElement.None,
            value = Random.Range(1, 5)
        };
        pc.itemManager.DamageAdd(temp);
        Destroy(this);
    }
}
