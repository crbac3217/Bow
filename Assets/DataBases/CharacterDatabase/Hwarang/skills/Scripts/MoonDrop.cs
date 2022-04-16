using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoonDrop", menuName = "Skill/Hwarang/MoonDrop", order = 110)]
public class MoonDrop : Skill
{
    public GameObject moonPref;
    public float addmult;

    public override void ActiveRelease(PlayerControl pc)
    {
        base.ActiveRelease(pc);
        var instance = Instantiate(moonPref, new Vector2(pc.transform.position.x, pc.transform.position.y + 4), Quaternion.identity);
        MoonInstance moon = instance.GetComponent<MoonInstance>();
        foreach (DamageType dtype in pc.damageTypes)
        {
            float mult = dtype.value + (dtype.value * addmult);
            DamageType temp = new DamageType
            {
                damageElement = dtype.damageElement,
                value = (int)mult
            };
            moon.damages.Add(temp);
        }
        moon.campar = pc.campar;
        moon.isright = pc.pm.isRight;
    }
}

