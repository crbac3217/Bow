using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsuraChangeForm", menuName = "EnemyAttack/Boss/Asura/ChangeForm", order = 105)]
public class ChangeForm : EnemyAttack
{
    public AsuraAi aai;
    public GameObject sChargeParticle, bChargeParticle, mChargeParticle, sExplodeParticle, bExplodeParticle, mExplodeParticle;
    private GameObject chargeInst;

    public override void SetUp()
    {
        base.SetUp();
        aai = aiHandler.GetComponent<AsuraAi>();
    }
    public override void Activate()
    {
        aiHandler.StartCoroutine(aiHandler.StopMoving(duration));
        if (aai.currentFormIndex == 1)
        {
            chargeInst = Instantiate(sChargeParticle, aiHandler.visuals.transform);
        }
        else if (aai.currentFormIndex == 2)
        {
            chargeInst = Instantiate(bChargeParticle, aiHandler.visuals.transform);
        }
        else if (aai.currentFormIndex == 3)
        {
            chargeInst = Instantiate(mChargeParticle, aiHandler.visuals.transform);
        }
        aiHandler.anim.SetTrigger("Change");
        aai.FormChange(); 
    }
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        aiHandler.anim.SetTrigger("ChangeEnd");
        if (aai.currentFormIndex == 1)
        {
            var explodePart = Instantiate(sExplodeParticle, aiHandler.visuals.transform);
        }
        else if (aai.currentFormIndex == 2)
        {
            var explodePart = Instantiate(bExplodeParticle, aiHandler.visuals.transform);
        }
        else if (aai.currentFormIndex == 3)
        {
            var explodePart = Instantiate(mExplodeParticle, aiHandler.visuals.transform);
        }
        Destroy(chargeInst);
    }
}
