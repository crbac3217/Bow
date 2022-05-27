using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PazuzuSlash", menuName = "EnemyAttack/Boss/Pazuzu/PazuzuSlash", order = 105)]
public class PazuzuSlash : EnemyAttack
{
    public GameObject actualRangePref;
    public EnemyAttackRange actualRange;
    public float meleeMult;
    public override void SetUp()
    {
        base.SetUp();
        var actualRangeInst = Instantiate(actualRangePref, aiHandler.visuals.transform);
        actualRangeInst.transform.localScale = aiHandler.visuals.transform.localScale;
        actualRange = actualRangeInst.GetComponent<EnemyAttackRange>();
    }
    public override void AttackEtc(PlayerControl pc)
    {
        if (actualRange.avail)
        {
            float amount = aiHandler.damage * meleeMult;
            aiHandler.pc.ph.OnPlayerHit(aiHandler.visuals.transform.position, (int)amount);
        }
        Vector2 spawnPos = new Vector2(aiHandler.visuals.transform.position.x, aiHandler.visuals.transform.position.y);
        var inst = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        EnemyProjectile ep = inst.GetComponent<EnemyProjectile>();
        ep.dir = new Vector2(aiHandler.visuals.transform.localScale.x, 0).normalized;
        ep.damage = Mathf.RoundToInt(aiHandler.damage * damageMult);
    }
}
