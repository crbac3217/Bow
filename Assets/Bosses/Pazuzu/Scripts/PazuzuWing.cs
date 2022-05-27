using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PazuzuWing", menuName = "EnemyAttack/Boss/Pazuzu/PazuzuWing", order = 105)]
public class PazuzuWing : EnemyAttack
{
    public LayerMask ground;
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        Vector2 rayPos = new Vector2();
        if (aiHandler.visuals.transform.localScale.x > 0)
        {
            rayPos = new Vector2(aiHandler.visuals.transform.position.x + 1, aiHandler.visuals.transform.position.y);
        }
        else
        {
            rayPos = new Vector2(aiHandler.visuals.transform.position.x - 1, aiHandler.visuals.transform.position.y);
        }
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.down, 5, ground);
        if (hit)
        {
            var wind = Instantiate(projectilePrefab, hit.point, Quaternion.identity);
            wind.GetComponent<EnemyParticle>().pc = aiHandler.pc;
        }
    }
}
