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
        aiHandler.StartCoroutine(CoolDown());
        base.SetUp();
        aai = aiHandler.GetComponent<AsuraAi>();
    }
    public override void Activate()
    {
        Debug.Log(aai.currentFormIndex + "is the curIndex, but" + aiHandler.anim.GetInteger("FormIndex"));
        aiHandler.anim.SetTrigger("Change");
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
        
    }
    public override void AttackEtc(PlayerControl pc)
    {
        aai.FormChange();
        aiHandler.anim.SetTrigger("ChangeEnd");
        aai.StartCoroutine(Exp());
    }
    public IEnumerator Exp()
    {
        yield return new WaitForSeconds(0.5f);
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
