using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JigwiAttack1", menuName = "EnemyAttack/Boss/Jigwi/JigwiAttack1", order = 105)]

public class JigwiAttack1 : EnemyAttack
{
    public Transform JigwiAttack1Point;
    public GameObject SmashParticle;
    public override void SetUp()
    {
        base.SetUp();
        JigwiAttack1Point = aiHandler.visuals.transform.Find("JigwiAttack1Point");
    }
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        var attack1 = Instantiate(projectilePrefab, JigwiAttack1Point.position, Quaternion.identity);
        var part = Instantiate(SmashParticle, JigwiAttack1Point.position, Quaternion.identity);
        EnemyLinearProj ep = attack1.GetComponent<EnemyLinearProj>();
        if (JigwiAttack1Point.position.x > aiHandler.transform.position.x)
        {
            ep.isRight = true;
        }
        else
        {
            ep.isRight = false;
        }
        float amount = aiHandler.damage * (float)damageMult;
        ep.damage = (int)amount;
        aiHandler.GetComponent<BossAi>().campar.StartCoroutine(aiHandler.GetComponent<BossAi>().campar.CamShake(Vector2.down * 0.05f, 0.1f));
    }
}
