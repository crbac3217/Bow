using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsuraMFrontShoot", menuName = "EnemyAttack/Boss/Asura/AsuraMFrontShoot", order = 105)]
public class AsuraMFrontShoot : EnemyAttack
{
    public GameObject chargeParticle, explodeParticle;
    private GameObject chargeInst;

    public override void Activate()
    {
        base.Activate();
        chargeInst = Instantiate(chargeParticle, aiHandler.visuals.transform);
    }
    public override void ShootProjectile(PlayerControl pc)
    {
        Destroy(chargeInst);
        var explodePart = Instantiate(explodeParticle, aiHandler.visuals.transform);
        var inst = Instantiate(projectilePrefab, aiHandler.visuals.transform.position, Quaternion.identity);
        EnemyProjectile ep = inst.GetComponent<EnemyProjectile>();
        ep.damage = Mathf.RoundToInt(aiHandler.damage * damageMult);
        ep.dir = new Vector2(aiHandler.visuals.transform.localScale.x, 0);
        ep.pc = aiHandler.pc;
    }
}
