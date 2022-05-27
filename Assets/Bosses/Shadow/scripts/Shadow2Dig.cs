using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shadow2Dig", menuName = "EnemyAttack/Boss/Shadow/Shadow2Dig", order = 105)]
public class Shadow2Dig : EnemyAttack
{
    public GameObject actualRangePref;
    public EnemyAttackRange actualRange;
    public ShadowAi sai;
    public override void SetUp()
    {
        base.SetUp();
        var actualRangeInst = Instantiate(actualRangePref, aiHandler.visuals.transform);
        actualRange = actualRangeInst.GetComponent<EnemyAttackRange>();
        sai = aiHandler.GetComponent<ShadowAi>();
    }
    public override void Activate()
    {
        base.Activate();
        sai.onGround = true;
    }
    public override void AttackEtc(PlayerControl pc)
    {
        if (sai.buried)
        {
            aiHandler.gameObject.transform.position = new Vector2(pc.transform.position.x, aiHandler.transform.position.y);
        }
        else
        {
            if (actualRange.avail)
            {
                float amount = aiHandler.damage * damageMult;
                aiHandler.pc.ph.OnPlayerHit(aiHandler.visuals.transform.position, (int)amount);
            }
        }
        base.AttackEtc(pc);
    }
    public override void AdditionalTrigger()
    {
        base.AdditionalTrigger();
        sai.onGround = false;
    }
}
