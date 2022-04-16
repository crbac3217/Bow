using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezingShield", menuName = "Modifier/Items/FreezingShield", order = 110)]
public class FreezingShield : Modifier
{
    public GameObject aura, temp;

    public override void OnModifierAdd(PlayerControl pc)
    {
        base.OnModifierAdd(pc);
        temp = Instantiate(aura, pc.transform.position, Quaternion.identity);
        temp.transform.SetParent(pc.gameObject.transform);
    }

    public override void OnModifierRemove(PlayerControl pc)
    {
        Destroy(temp);
    }
}
