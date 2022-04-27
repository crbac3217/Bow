using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsuraBDoubleShot", menuName = "EnemyAttack/Boss/Asura/AsuraBDoubleShot", order = 105)]
public class AsuraBDoubleShot : EnemyAttack
{
    public GameObject chargeParticle, explodeParticle;
    private GameObject chargeInst;
    private int count;
    public override void Activate()
    {
        base.Activate();
        count = 0;
        if (chargeInst)
        {
            Destroy(chargeInst);
        }
        chargeInst = Instantiate(chargeParticle, aiHandler.visuals.transform);
    }
    public override void ShootProjectile(PlayerControl pc)
    {
        if (chargeInst)
        {
            Destroy(chargeInst);
        }
        var explodePart = Instantiate(explodeParticle, aiHandler.visuals.transform);
        var inst = Instantiate(projectilePrefab, aiHandler.visuals.transform.position, Quaternion.identity);
        EnemyProjectile ep = inst.GetComponent<EnemyProjectile>();
        ep.damage = Mathf.RoundToInt(aiHandler.damage * damageMult);
        count++;
        if (count == 1)
        {
            ep.dir = new Vector2(aiHandler.visuals.transform.localScale.x, 1);
        }
        else
        {
            ep.dir = new Vector2(aiHandler.visuals.transform.localScale.x, -0.05f);
        }
    }
}
