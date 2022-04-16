using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinotaurSlam", menuName = "EnemyAttack/Boss/Minotaur/MinotaurSlam", order = 105)]
public class MinotaurSlam : EnemyAttack
{
    public GameObject groundSlamParticle;
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        var part = Instantiate(groundSlamParticle, aiHandler.transform);
        aiHandler.GetComponent<BossAi>().campar.StartCoroutine(aiHandler.GetComponent<BossAi>().campar.CamShake(Vector2.up * 0.05f, 0.5f));
        if (pc.pj.isGrounded)
        {
            float amount = aiHandler.damage * damageMult;
            pc.ph.OnPlayerHit(aiHandler.visuals.transform.position, (int)amount);
        }
    }
}
