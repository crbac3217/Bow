using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsuraMTripleShoot", menuName = "EnemyAttack/Boss/Asura/AsuraMTripleShoot", order = 105)]
public class AsuraMTripleShoot : EnemyAttack
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
        var inst = Instantiate(projectilePrefab, aiHandler.visuals.transform.position, Quaternion.identity);
        Destroy(chargeInst);
        var explodePart = Instantiate(explodeParticle, aiHandler.visuals.transform);
        EnemyProjectile ep = inst.GetComponent<EnemyProjectile>();
        ep.damage = Mathf.RoundToInt(aiHandler.damage * damageMult);
        ep.dir = new Vector2(aiHandler.visuals.transform.localScale.x, 0);
    }
}
