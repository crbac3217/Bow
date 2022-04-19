using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsuraMCharge", menuName = "EnemyAttack/Boss/Asura/AsuraMCharge", order = 105)]
public class AsuraMCharge : EnemyAttack
{
    public GameObject ball, chargePart;
    public int count;
    private GameObject ballInst;
    public override void Activate()
    {
        int rand = Random.Range((int)duration, (int)duration + 3);
        aiHandler.TriggerAnimation(AttackName, whileMove, rand);
        aiHandler.StartCoroutine(StopCharging(rand));
        ballInst = Instantiate(ball, new Vector2 (aiHandler.visuals.transform.position.x, aiHandler.visuals.transform.position.y + 1), Quaternion.identity);
        ballInst.transform.SetParent(aiHandler.visuals.transform);
        ballInst.GetComponent<EnemyParticle>().appear = true;
        var chargePartInst = Instantiate(chargePart, ballInst.transform);
        count = 0;
    }
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        Vector2 spawnPos = ballInst.transform.position;
        for (int i = 0; i < 24; i++)
        {
            var inst = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            EnemyProjectile ep = inst.GetComponent<EnemyProjectile>();
            ep.dir = (Vector2)(Quaternion.Euler(0, 0, (i * 360 / 24) + (count * 10)) * Vector2.right);
            ep.damage = Mathf.RoundToInt(aiHandler.damage * damageMult);
            if (ep.dir.y < -0.9f)
            {
                Destroy(inst);
            }
        }
        count++;
    }
    public IEnumerator StopCharging(int rand)
    {
        yield return new WaitForSeconds(rand);
        aiHandler.TriggerAdditionalAnimation("AsuraMCharge", false, 1);
        End();
    }
    public void End()
    {
        ballInst.GetComponent<EnemyParticle>().disappear = true;
    }
}
