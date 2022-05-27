using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsuraMReflect", menuName = "EnemyAttack/Boss/Asura/AsuraMReflect", order = 105)]
public class AsuraMReflect : EnemyAttack
{
    public GameObject chargeParticle, explodeParticle, particle;
    private GameObject chargeInst;
    public override void Activate()
    {
        base.Activate();
        aiHandler.ec.invincible = true;
        if (chargeInst)
        {
            Destroy(chargeInst);
        }
        chargeInst = Instantiate(chargeParticle, aiHandler.visuals.transform);
    }
    public override void AttackEtc(PlayerControl pc)
    {
        Destroy(chargeInst);
        var explodePart = Instantiate(explodeParticle, aiHandler.visuals.transform);
        base.AttackEtc(pc);
        int lost = aiHandler.ec.maxHp - aiHandler.ec.hp;
        aiHandler.ec.hp += (int)(lost * 0.4f);
        aiHandler.ec.UpdateHpUI();
        aiHandler.damage += 1;
        aiHandler.ec.invincible = false;
        foreach (DamageType str in aiHandler.ec.strength)
        {
            str.value += 10;
        }
    }
    public override void AdditionalTrigger()
    {
        base.AdditionalTrigger();
        var part = Instantiate(particle, aiHandler.visuals.transform);
    }
}
