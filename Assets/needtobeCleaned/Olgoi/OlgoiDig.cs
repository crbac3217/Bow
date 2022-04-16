using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OlgoiDig", menuName = "EnemyAttack/Boss/Olgoi/OlgoiDig", order = 105)]
public class OlgoiDig : EnemyAttack
{
    public GameObject actualRangePref;
    public EnemyAttackRange actualRange;
    public OlgoiAi oai;
    public override void SetUp()
    {
        base.SetUp();
        var actualRangeInst = Instantiate(actualRangePref, aiHandler.visuals.transform);
        actualRange = actualRangeInst.GetComponent<EnemyAttackRange>();
        oai = aiHandler.GetComponent<OlgoiAi>();
    }
    public override void AttackEtc(PlayerControl pc)
    {
        if (oai.isDug)
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
}
