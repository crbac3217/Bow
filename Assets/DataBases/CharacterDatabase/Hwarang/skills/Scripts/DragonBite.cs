using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DragonBite", menuName = "Skill/Hwarang/DragonBite", order = 110)]
public class DragonBite : Skill
{
    public DamageElement delem;
    public GameObject[] prefabs;
    public Sprite[] icons;
    public override void ShotEnhancePress(PlayerControl pc)
    {
        enhanceObj = prefabs[(int)delem - 1];
        base.ShotEnhancePress(pc);
    }
}
