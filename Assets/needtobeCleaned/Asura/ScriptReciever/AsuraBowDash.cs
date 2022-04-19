using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsuraBowDash", menuName = "EnemyAttack/Boss/Asura/AsuraBowDash", order = 105)]
public class AsuraBowDash : EnemyAttack
{
    private Vector2 dir;
    public GameObject dashParticle;
    public override void Activate()
    {
        base.Activate();
        dir = (aiHandler.pc.transform.position - aiHandler.visuals.transform.position).normalized;
    }
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        var dash = Instantiate(dashParticle, aiHandler.visuals.transform);
        dash.transform.localPosition = new Vector2(-0.5f, 0);
        aiHandler.GetComponent<Rigidbody2D>().velocity = dir * dashDist;
        if (dir.x > 0)
        {
            aiHandler.visuals.transform.localScale = new Vector2(Mathf.Abs(aiHandler.visuals.transform.localScale.x), aiHandler.visuals.transform.localScale.y);
        }
        else
        {
            aiHandler.visuals.transform.localScale = new Vector2(Mathf.Abs(aiHandler.visuals.transform.localScale.x) * -1, aiHandler.visuals.transform.localScale.y);
        }
    }
}
