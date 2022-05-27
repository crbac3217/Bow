using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyController : EnemyController
{
    public ArangAI aai;
    private ButterFlyCont bf;
    public override void Setup()
    {
        bf = GetComponent<ButterFlyCont>();
    }
    public override void OnDamageTaken()
    {
        base.OnDamageTaken();
        aai.butterflies.Remove(this.gameObject);
        Dead(true);
    }
    public override void Dead(bool doDrop)
    {
        if (doDrop)
        {
            dm.OnKill(this.gameObject);
        }
        chestDropMax = 0;
        maxGoldDrop = 0;
        minGoldDrop = 0;
        for (int i = activeCrits.Count - 1; i >= 0; i--)
        {
            Crit c = activeCrits[i];
            if (!activeCrits[i].isTick)
            {

                c.RemoveStatusEffect(this);
            }
            else
            {
                c.ticksLeft.Clear();
            }
        }
        bf.disappear = true;
        GetComponent<Animator>().SetTrigger("Death");
    }
}
