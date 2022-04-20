using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Shadow1Shoot", menuName = "EnemyAttack/Boss/Shadow/Shadow1Shoot", order = 105)]
public class Shadow1Shoot : EnemyAttack
{
    private int count;

    public override void Activate()
    {
        base.Activate();
        count = 0;
    }
    public override void ShootProjectile(PlayerControl pc)
    {
        var inst = Instantiate(projectilePrefab, aiHandler.visuals.transform.position, Quaternion.identity);
        EnemyProjectile ep = inst.GetComponent<EnemyProjectile>();
        ep.damage = Mathf.RoundToInt(aiHandler.damage * damageMult);
        count++;
        if (aiHandler.visuals.transform.localScale.x > 0)
        {
            ep.dir = (Vector2)(Quaternion.Euler(0, 0, 90f - count * 15f) * Vector2.right);
        }
        else
        {
            ep.dir = (Vector2)(Quaternion.Euler(0, 0, 270f + count * 15f) * Vector2.left);
        }
        
    }
}
