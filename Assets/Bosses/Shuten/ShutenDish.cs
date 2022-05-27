using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShutenDish", menuName = "EnemyAttack/Boss/Shuten/ShutenDish", order = 105)]
public class ShutenDish : EnemyAttack
{
    public int maxTime;
    public float reviveDuration;
    public int hitTimes;
    public GameObject ShutenDishCollider;

    public override void SetUp()
    {
        base.SetUp();
        ShutenDishCollider = aiHandler.visuals.transform.Find("ShutenDishCollider").gameObject;
    }
    public override void Activate()
    {
        hitTimes = 0;
        int rand = Random.Range((int)duration, (int)maxTime);
        aiHandler.TriggerAnimation(AttackName, whileMove, rand);
        aiHandler.StartCoroutine(Revive(rand));
    }
    public override void AdditionalTrigger()
    {
        base.AdditionalTrigger();
        hitTimes++;
    }

    public IEnumerator Revive(int rand)
    {
        yield return new WaitForSeconds(rand);
        aiHandler.TriggerAnimation("ShutenRevive", whileMove, reviveDuration);
        yield return new WaitForSeconds(1);
        ShutenDishCollider.SetActive(false);
        foreach (EnemyAttack at in aiHandler.attacks)
        {
            at.damageMult += (0.05f * hitTimes);
            at.dashDist += (0.05f * hitTimes);
        }
        aiHandler.ec.hp += hitTimes * 4;
        aiHandler.ec.UpdateHpUI();
        aiHandler.ec.invincible = false;
        aiHandler.checkInterval = Mathf.Clamp(aiHandler.checkInterval - (0.05f * hitTimes), 0.1f, 1f);
    }
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        ShutenDishCollider.SetActive(true);
        aiHandler.ec.invincible = true;
    }
}
