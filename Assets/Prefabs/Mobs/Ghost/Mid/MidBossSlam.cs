using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MidbossSlam", menuName = "EnemyAttack/Mobs/Ghost/MidBossSlam", order = 105)]
public class MidBossSlam : EnemyAttack
{
    public GameObject groundSlamParticle;
    public GameObject actualRangePref;
    private GameObject actualRangeInst;
    public EnemyAttackRange actualRange;
    public override void SetUp()
    {
        base.SetUp();
        actualRangeInst = Instantiate(actualRangePref, aiHandler.visuals.transform);
        actualRangeInst.transform.localScale = aiHandler.visuals.transform.localScale;
        actualRange = actualRangeInst.GetComponent<EnemyAttackRange>();
    }
    public override void AttackEtc(PlayerControl pc)
    {
        if (aiHandler.curNode)
        {
            if (pc.pm.nearestNode.platform == aiHandler.curNode.platform)
            {
                var part = Instantiate(groundSlamParticle, aiHandler.transform);
                pc.campar.StartCoroutine(pc.campar.CamShake(Vector2.up * 0.05f, 0.5f));
                if (pc.pj.isGrounded)
                {
                    float amount = aiHandler.damage * damageMult;
                    pc.ph.OnPlayerHit(aiHandler.visuals.transform.position, (int)amount);
                }
            }
        }
        if (actualRange.avail)
        {
            float amount = aiHandler.damage * damageMult;
            pc.ph.OnPlayerHit(aiHandler.visuals.transform.position, (int)amount);
        }
    }
}
