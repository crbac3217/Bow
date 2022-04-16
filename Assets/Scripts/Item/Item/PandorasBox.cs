using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PandorasBox", menuName = "Modifier/Items/PandorasBox", order = 110)]
public class PandorasBox : Modifier
{
    public override void OnModifierAdd(PlayerControl pc)
    {
        base.OnModifierAdd(pc);
        MixAll(pc);
    }

    void MixAll(PlayerControl pc)
    {
        int totalval = 0;

        foreach (DamageType dtype in pc.damageTypes)
        {
            totalval += dtype.value - 1;
            dtype.value = 1;
        }
        for (int i = 0; i < pc.damageTypes.Count; i++)
        {
            DamageType temp = pc.damageTypes[i];
            int randomIndex = Random.Range(i, pc.damageTypes.Count);
            pc.damageTypes[i] = pc.damageTypes[randomIndex];
            pc.damageTypes[randomIndex] = temp;
        }
        foreach (DamageType dt in pc.damageTypes)
        {
            if (pc.damageTypes[pc.damageTypes.Count-1] != dt)
            {
                int amount = Random.Range(0, totalval);
                dt.value += amount;
                totalval -= amount;
            }
            else
            {
                dt.value += totalval;
            }
        }
    }
}
