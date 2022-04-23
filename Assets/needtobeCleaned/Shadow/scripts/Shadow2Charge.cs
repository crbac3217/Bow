using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shadow2Charge", menuName = "EnemyAttack/Boss/Shadow/Shadow2Charge", order = 105)]
public class Shadow2Charge : EnemyAttack
{
    public GameObject ball;
    private GameObject ballInst;
    private int count;
    public override void Activate()
    {
        int rand = Random.Range((int)duration, (int)duration + 3);
        aiHandler.TriggerAnimation(AttackName, whileMove, rand);
        aiHandler.StartCoroutine(StopCharging(rand));
        ballInst = Instantiate(ball, new Vector2(aiHandler.visuals.transform.position.x, aiHandler.visuals.transform.position.y + 1), Quaternion.identity);
        ballInst.transform.SetParent(aiHandler.visuals.transform);
        count = 0;
        ballInst.GetComponent<ShadowChargeBall>().damage = Mathf.RoundToInt(aiHandler.damage * damageMult);
        ballInst.GetComponent<ShadowChargeBall>().pc = aiHandler.pc;
    }
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        if (ballInst)
        {
            Vector2 spawnPos = ballInst.transform.position;
            var inst = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            EnemyProjectile ep = inst.GetComponent<EnemyProjectile>();
            ep.damage = Mathf.RoundToInt(aiHandler.damage * damageMult);
            ep.pos = aiHandler.pc.transform.position;
            count++;
        }
    }
    public IEnumerator StopCharging(int rand)
    {
        yield return new WaitForSeconds(rand);
        aiHandler.TriggerAnimationNoDisrupt("Shadow2ChargeTrigger", false, 1);
        ballInst.GetComponent<ShadowChargeBall>().count = count;
        End();
    }
    public void End()
    {
        ballInst.GetComponent<Animator>().SetTrigger("End");
    }
}
