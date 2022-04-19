using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsuraBChargeShot", menuName = "EnemyAttack/Boss/Asura/AsuraBChargeShot", order = 105)]
public class AsuraBChargeShot : EnemyAttack
{
    public GameObject chargeParticle, explodeParticle;
    private GameObject chargeParticleInst;
    public override void Activate()
    {
        base.Activate();
        chargeParticleInst = Instantiate(chargeParticle, aiHandler.visuals.transform);
    }
    public override void ShootProjectile(PlayerControl pc)
    {
        Destroy(chargeParticleInst);
        var explodePart = Instantiate(explodeParticle, aiHandler.visuals.transform);
        var inst = Instantiate(projectilePrefab, aiHandler.visuals.transform.position, Quaternion.identity);
        EnemyProjectile ep = inst.GetComponent<EnemyProjectile>();
        ep.dir = new Vector2(aiHandler.visuals.transform.localScale.x, -0.05f);
        ep.damage = Mathf.RoundToInt(aiHandler.damage * damageMult);
    }
}
