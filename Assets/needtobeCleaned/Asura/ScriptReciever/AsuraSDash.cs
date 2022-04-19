using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsuraSDash", menuName = "EnemyAttack/Boss/Asura/AsuraSDash", order = 105)]
public class AsuraSDash : EnemyAttack
{
    public GameObject actualRangePref, particle;
    private GameObject actualRangeInst, inst;
    private EnemyAttackRange actualRange;
    public bool hasDashed;
    private bool isright;

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
        if (aiHandler.pc.transform.position.x > aiHandler.transform.position.x)
        {
            isright = true;
        }
        else
        {
            isright = false;
        }
        hasDashed = false;
    }
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        if (!hasDashed)
        {
            if (isright)
            {
                aiHandler.GetComponent<Rigidbody2D>().velocity = Vector2.right * dashDist;
                aiHandler.visuals.transform.localScale = new Vector2(Mathf.Abs(aiHandler.visuals.transform.localScale.x), aiHandler.visuals.transform.localScale.y);
            }
            else
            {
                aiHandler.GetComponent<Rigidbody2D>().velocity = Vector2.left * dashDist;
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
    public override void AdditionalTrigger()
    {
        base.AdditionalTrigger();
        aiHandler.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        var part = Instantiate(particle, aiHandler.visuals.transform.position, Quaternion.identity);
        part.transform.localScale = new Vector2(aiHandler.visuals.transform.localScale.x, 1);
        part.GetComponent<EnemyParticle>().damage = (int)(aiHandler.damage * damageMult);
    }
}
