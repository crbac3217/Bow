using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "RingOfProtection", menuName = "Modifier/Items/RingOfProtection", order = 110)]
public class RingOfProtection : Modifier
{
    public float time;
    public override void OnModifierActive(PlayerControl pc)
    {
        base.OnModifierActive(pc);
        pc.psm.ActivateCoroutine(InvincibleForTime(pc)); 
    }
    public IEnumerator InvincibleForTime(PlayerControl pc)
    {
        pc.ph.SetInvincible();
        pc.ph.SetDodge();
        yield return new WaitForSeconds(time);
        pc.ph.UnsetDodge();
        pc.ph.SetVulnerable();
    }
}
