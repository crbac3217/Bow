using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NecklaceOfAffluence", menuName = "Modifier/Items/NecklaceOfAffluence", order = 110)]
public class NecklaceOfAffluence : Modifier
{
    public override void OnModifierAdd(PlayerControl pc)
    {
        base.OnModifierAdd(pc);
        pc.GainGold(Random.Range(3, 20));
    }
}
