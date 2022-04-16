using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashAttack", menuName = "EnemyAttack/DashAttack", order = 105)]
public class DashAttack : EnemyAttack
{
    public GameObject actualRangePref;
    private GameObject actualRangeInst;
    public EnemyAttackRange actualRange;
    public bool hasDashed;
    public Vector2 dir;
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
        dir = (aiHandler.pc.transform.position - aiHandler.visuals.transform.position).normalized;
        hasDashed = false;
    }
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        if (!hasDashed)
        {
            aiHandler.GetComponent<Rigidbody2D>().velocity = dir * dashDist;
            if (dir.x > 0)
            {
                aiHandler.visuals.transform.localScale = new Vector2(Mathf.Abs(aiHandler.visuals.transform.localScale.x), aiHandler.visuals.transform.localScale.y);
            }
            else
            {
                aiHandler.visuals.transform.localScale = new Vector2(Mathf.Abs(aiHandler.visuals.transform.localScale.x) * -1, aiHandler.visuals.transform.localScale.y);
            }
            hasDashed = true;
        }
        if (actualRange.avail)
        {
            float amount = aiHandler.damage * damageMult;
            pc.ph.OnPlayerHit(aiHandler.visuals.transform.position, (int)amount);
        }
    }
}

