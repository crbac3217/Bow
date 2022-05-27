using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinotaurDash", menuName = "EnemyAttack/Boss/Minotaur/MinotaurDash", order = 105)]
public class MinotaurDash : EnemyAttack
{
    public float dashDistance;
    private Vector2 dir;
    public GameObject actualRangePref;
    private GameObject actualRangeInst;
    private EnemyAttackRange actualRange;
    public override void SetUp()
    {
        base.SetUp();
        actualRangeInst = Instantiate(actualRangePref, aiHandler.visuals.transform);
        actualRange = actualRangeInst.GetComponent<EnemyAttackRange>();
    }

    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        dir = (aiHandler.pc.transform.position - aiHandler.transform.position).normalized;
        if (dir.x > 0)
        {
            aiHandler.GetComponent<Rigidbody2D>().velocity = Vector2.right * dashDistance;
            aiHandler.visuals.transform.localScale = new Vector2(Mathf.Abs(aiHandler.visuals.transform.localScale.x), aiHandler.visuals.transform.localScale.y);
        }
        else
        {
            aiHandler.GetComponent<Rigidbody2D>().velocity = Vector2.left * dashDistance;
            aiHandler.visuals.transform.localScale = new Vector2(Mathf.Abs(aiHandler.visuals.transform.localScale.x) * -1, aiHandler.visuals.transform.localScale.y);
        }
        if (actualRange.avail)
        {
            float amount = aiHandler.damage * damageMult;
            pc.ph.OnPlayerHit(aiHandler.visuals.transform.position, (int)amount);
        }
    }
}
