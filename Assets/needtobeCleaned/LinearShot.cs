using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "linearShot", menuName = "EnemyAttack/linearShot", order = 105)]
public class LinearShot : EnemyAttack
{
    public override void ShootProjectile(PlayerControl pc)
    {
        var inst = Instantiate(projectilePrefab, aiHandler.visuals.transform.position, Quaternion.identity);
        EnemyProjectile ep = inst.GetComponent<EnemyProjectile>();
        ep.damage = Mathf.RoundToInt(aiHandler.damage * damageMult);
        ep.dir = new Vector2(aiHandler.visuals.transform.localScale.x, 0);
        ep.pc = aiHandler.pc;
    }
}
