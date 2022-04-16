using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OlgoiTailwhip", menuName = "EnemyAttack/Boss/Olgoi/OlgoiTailwhip", order = 105)]
public class OlgoiTailwhip : EnemyAttack
{
    public GameObject actualRangePref;
    public EnemyAttackRange actualRange;
    public OlgoiAi oai;
    private int stacks;
    public float meleeMult;
    public override void SetUp()
    {
        base.SetUp();
        var actualRangeInst = Instantiate(actualRangePref, aiHandler.visuals.transform);
        actualRange = actualRangeInst.GetComponent<EnemyAttackRange>();
        oai = aiHandler.GetComponent<OlgoiAi>();
    }
    public override void Activate()
    {
        base.Activate();
        stacks = 0;
    }
    public override void AttackEtc(PlayerControl pc)
    {
        if (actualRange.avail)
        {
            float amount = aiHandler.damage * meleeMult;
            aiHandler.pc.ph.OnPlayerHit(aiHandler.visuals.transform.position, (int)amount);
        }
        Vector2 spawnPos = new Vector2(aiHandler.visuals.transform.position.x, aiHandler.visuals.transform.position.y + 1);
        for (int i = 0; i < 12; i++)
        {
            var inst = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            EnemyProjectile ep = inst.GetComponent<EnemyProjectile>();
            ep.dir = (Vector2)(Quaternion.Euler(0, 0, (i * 360 / 12) + (stacks * 10)) * Vector2.right);
            ep.damage = Mathf.RoundToInt(aiHandler.damage * damageMult);
            if (ep.dir.y < -0.9f)
            {
                Destroy(inst);
            }
        }
        stacks++;
    }
}
