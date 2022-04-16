using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GemOfFate", menuName = "Modifier/Items/GemOfFate", order = 110)]
public class GemOfFate : Modifier
{
    public int maxWinChance;
    public override void OnModifierAdd(PlayerControl pc)
    {
        base.OnModifierAdd(pc);
        int chance = Random.Range(0, maxWinChance);
        ItemManager im = pc.itemManager;
        if (chance == 1)
        {
            pc.stats[2].value += 10;
            pc.currentHp += 10;
        }
        else
        {
            pc.stats[2].value += 1;
        }
    }
}
