using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WendigoDash", menuName = "EnemyAttack/Boss/Wendigo/WendigoDash", order = 105)]
public class WendigoDash : EnemyAttack
{
    public float dashDistance;
    private Vector2 dir;
    private bool hasDashed;
    public GameObject actualRangePref;
    private GameObject actualRangeInst;
    private EnemyAttackRange actualRange;
    public override void SetUp()
    {
        base.SetUp();
        actualRangeInst = Instantiate(actualRangePref, aiHandler.visuals.transform);
        actualRange = actualRangeInst.GetComponent<EnemyAttackRange>();
    }

    public override void Activate()
    {
        base.Activate();
        dir = (aiHandler.pc.transform.position - aiHandler.transform.position).normalized;
        hasDashed = false;
    }
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        if (!hasDashed)
        {
            aiHandler.GetComponent<Rigidbody2D>().velocity = dir * dashDistance;
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
