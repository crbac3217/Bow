using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArrowOfDestruction", menuName = "Modifier/Items/ArrowOfDestruction", order = 110)]
public class ArrowOfDestruction : Modifier
{
    public override void OnModifierAdd(PlayerControl pc)
    {
        base.OnModifierAdd(pc);
        pc.stats[2].value = 1;
        pc.currentHp = 1;
    }
}
