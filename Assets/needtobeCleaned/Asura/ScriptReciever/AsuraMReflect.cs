using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsuraMReflect", menuName = "EnemyAttack/Boss/Asura/AsuraMReflect", order = 105)]
public class AsuraMReflect : EnemyAttack
{
    public GameObject chargeParticle, explodeParticle;
    private GameObject chargeInst;
    public override void Activate()
    {
        base.Activate();
        aiHandler.ec.invincible = true;
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
        foreach (DamageType str in aiHandler.ec.strength)
        {
            str.value += 10;
        }
    }
}
