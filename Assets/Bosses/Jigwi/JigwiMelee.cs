using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JigwiMelee", menuName = "EnemyAttack/Boss/Jigwi/JigwiMelee", order = 105)]
public class JigwiMelee : EnemyAttack
{
    public GameObject JigwiMeleeParticle;
    public Transform JigwiMeleePoint;

    public override void SetUp()
    {
        JigwiMeleePoint = aiHandler.visuals.transform.Find("JigwiMeleePoint");
        base.SetUp();
    }
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        var particle = Instantiate(JigwiMeleeParticle, JigwiMeleePoint.transform);
        EnemyParticle ep = particle.GetComponent<EnemyParticle>();
        ep.pc = pc;
        float amount = aiHandler.damage * damageMult;
        ep.damage = (int)amount;
    }
}
