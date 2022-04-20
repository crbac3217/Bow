using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shadow2Slash", menuName = "EnemyAttack/Boss/Shadow/Shadow2Slash", order = 105)]
public class Shadow2Slash : EnemyAttack
{
    public GameObject slash, actualRangePref;
    private GameObject slashInst, actualRangeInst;
    private EnemyAttackRange actualRange;
    private bool spawned;

    public override void Activate()
    {
        base.Activate();
        spawned = false;
    }

    public override void SetUp()
    {
        base.SetUp();
        actualRangeInst = Instantiate(actualRangePref, aiHandler.visuals.transform);
        actualRangeInst.transform.localScale = aiHandler.visuals.transform.localScale;
        actualRange = actualRangeInst.GetComponent<EnemyAttackRange>();
    }
    public override void AttackEtc(PlayerControl pc)
    {
        float amount = aiHandler.damage * damageMult;
        if (actualRange.avail)
        {
            pc.ph.OnPlayerHit(aiHandler.visuals.transform.position, (int)amount);
        }
        if (!spawned)
        {
            Vector2 spawnPos = new Vector2(pc.transform.position.x, aiHandler.transform.position.y + 0.5f);
            slashInst = Instantiate(slash, spawnPos, Quaternion.identity);
            slashInst.transform.localScale = aiHandler.visuals.transform.localScale;
            slashInst.GetComponent<EnemyParticle>().damage = (int)amount;
            slashInst.GetComponent<EnemyParticle>().pc = pc;
            spawned = true;
        }
    }
}
