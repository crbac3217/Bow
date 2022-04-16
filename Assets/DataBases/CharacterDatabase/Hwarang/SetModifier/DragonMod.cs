using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "dragon", menuName = "Modifier/Sets/dragon", order = 110)]
public class DragonMod : Modifier
{
    private DamageElement delem;
    public Skill before1, before2, before3;
    public TripleShot after1;
    public SacredFlame after2;
    public DragonBite after3;

    public override void OnModifierAdd(PlayerControl pc)
    {
        base.OnModifierAdd(pc);
        List<DamageType> sorted = pc.damageTypes.OrderByDescending(o => o.value).ToList();
        if (sorted[0].damageElement != DamageElement.None)
        {
            delem = sorted[0].damageElement;
        }
        else
        {
            delem = sorted[1].damageElement;
        }
        sorted[0].value += 10;

        after1.delem = delem;
        after2.delem = delem;
        after3.delem = delem;
        after1.thumbnail = after1.icons[(int)delem - 1];
        after2.thumbnail = after2.sprites[(int)delem - 1];
        after3.thumbnail = after3.icons[(int)delem - 1];
        pc.ReplaceSkill(before1.skillName, after1);
        pc.ReplaceSkill(before2.skillName, after2);
        pc.ReplaceSkill(before3.skillName, after3);
    }
}
