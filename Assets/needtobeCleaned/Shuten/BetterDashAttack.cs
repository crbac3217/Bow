using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FollowDashAttack", menuName = "EnemyAttack/FollowDashAttack", order = 105)]
public class BetterDashAttack : DashAttack
{
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        if (!hasDashed)
        {
            aiHandler.GetComponent<Rigidbody2D>().velocity = (dir  + (pc.GetComponent<Rigidbody2D>().velocity * 0.2f)) * dashDist;
            if (dir.x > 0)
            {
                aiHandler.visuals.transform.localScale = new Vector2(Mathf.Abs(aiHandler.visuals.transform.localScale.x), aiHandler.visuals.transform.localScale.y);
            }
            else
            {
                aiHandler.visuals.transform.localScale = new Vector2(Mathf.Abs(aiHandler.visuals.transform.localScale.x) * -1, aiHandler.visuals.transform.localScale.y);
            }
            hasDashed = true;
        }
        if (actualRange.avail)
        {
            float amount = aiHandler.damage * damageMult;
            pc.ph.OnPlayerHit(aiHandler.visuals.transform.position, (int)amount);
        }
    }
}
