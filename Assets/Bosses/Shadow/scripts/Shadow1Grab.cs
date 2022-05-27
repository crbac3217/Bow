using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shadow1Grab", menuName = "EnemyAttack/Boss/Shadow/Shadow1Grab", order = 105)]
public class Shadow1Grab : EnemyAttack
{
    public GameObject grab;
    public LayerMask ground;
    private GameObject grabInst;
    private ShadowAi sai;

    public override void SetUp()
    {
        base.SetUp();
        sai = aiHandler.GetComponent<ShadowAi>();
    }
    public override void AttackEtc(PlayerControl pc)
    {
        Vector2 markPos = new Vector2(pc.transform.position.x, sai.groundLevel);
        base.AttackEtc(pc);
        grabInst = Instantiate(grab, markPos, Quaternion.identity);
        grabInst.transform.localScale = aiHandler.visuals.transform.localScale;
        grabInst.GetComponent<EnemyParticle>().damage = Mathf.RoundToInt(aiHandler.damage * damageMult);
        grabInst.GetComponent<EnemyParticle>().pc = pc;
    }
    public override void AdditionalTrigger()
    {
        base.AdditionalTrigger();
        if (grabInst)
        {
            grabInst.GetComponent<Animator>().SetTrigger("End");
        }
    }
}
