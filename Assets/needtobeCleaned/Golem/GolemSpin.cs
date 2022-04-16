using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GolemSpin", menuName = "EnemyAttack/Boss/Golem/GolemSpin", order = 105)]
public class GolemSpin : EnemyAttack
{
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
    public override void Activate()
    {
        base.Activate();
        aiHandler.ec.invincible = true;
    }
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        if (actualRange.avail)
        {
            float amount = aiHandler.damage * damageMult;
            pc.ph.OnPlayerHit(aiHandler.visuals.transform.position, (int)amount);
        }
    }
    public override void AdditionalTrigger()
    {
        base.AdditionalTrigger();
        aiHandler.ec.invincible = false;
    }
}
