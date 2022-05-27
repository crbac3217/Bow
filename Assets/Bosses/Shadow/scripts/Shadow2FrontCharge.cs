using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shadow2FrontCharge", menuName = "EnemyAttack/Boss/Shadow/Shadow2FrontCharge", order = 105)]
public class Shadow2FrontCharge : EnemyAttack
{
    public GameObject ball;
    private GameObject ballInst;
    public override void Activate()
    {
        int rand = Random.Range((int)duration, (int)duration + 2);
        aiHandler.TriggerAnimation(AttackName, whileMove, rand);
        aiHandler.StartCoroutine(StopCharging(rand));
        ballInst = Instantiate(ball, new Vector2(aiHandler.visuals.transform.position.x, aiHandler.visuals.transform.position.y), Quaternion.identity);
        ballInst.transform.localScale = aiHandler.visuals.transform.localScale;
        ballInst.transform.SetParent(aiHandler.visuals.transform);
    }
    public override void AttackEtc(PlayerControl pc)
    {
        if (ballInst)
        {
            Vector2 spawnPos = ballInst.GetComponent<ShadowFrontChargeParticle>().pos.position;
            var inst = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            ShadowHomingProj ep = inst.GetComponent<ShadowHomingProj>();
            ep.dir = new Vector2(Random.Range(-1f, 1f), Random.Range(0.3f, 1f)).normalized;
            ep.pc = pc;
            ep.damage = Mathf.RoundToInt(aiHandler.damage * damageMult);
        }
    }
    public IEnumerator StopCharging(int rand)
    {
        yield return new WaitForSeconds(rand);
        aiHandler.TriggerAnimationNoDisrupt("Shadow2FrontChargeTrigger", false, 1);
        End();
    }
    public void End()
    {
        ballInst.GetComponent<EnemyParticle>().disappear = true;
    }
}
