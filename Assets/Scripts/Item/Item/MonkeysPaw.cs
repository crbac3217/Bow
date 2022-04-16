using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonkeysPaw", menuName = "Modifier/Items/MonkeysPaw", order = 110)]
public class MonkeysPaw : Modifier
{
    private int savedAmount;

    public override void OnModifierAdd(PlayerControl pc)
    {
        base.OnModifierAdd(pc);
        DecRandom(pc);
        IncRandom(pc);
        DecRandom(pc);
        IncRandom(pc);
        DecRandom(pc);
        IncRandom(pc);
        Destroy(this);
    }
    void DecRandom(PlayerControl pc)
    {
        int WhichStat = Random.Range(0, pc.stats.Length);
        if (pc.stats[WhichStat].value > 1)
        {
            savedAmount = Random.Range(1, pc.stats[WhichStat].value);
        }
        else
        {
            savedAmount = 0;
        }
        pc.stats[WhichStat].value -= savedAmount;
    }
    void IncRandom(PlayerControl pc)
    {
        int WhichStat = Random.Range(0, pc.stats.Length);
        pc.stats[WhichStat].value += savedAmount + 1;
    }
    
}