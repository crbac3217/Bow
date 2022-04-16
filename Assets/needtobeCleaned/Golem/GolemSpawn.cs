using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GolemSpawn", menuName = "EnemyAttack/Boss/Golem/GolemSpawn", order = 105)]
public class GolemSpawn : EnemyAttack
{
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        SpawnMob(new Vector2(aiHandler.nextNode.position.x, aiHandler.nextNode.position.y + 1));
    }
    public void SpawnMob(Vector2 pos)
    {
        GameObject temp = Instantiate(projectilePrefab, pos, Quaternion.identity);
        EnemyController ec = temp.GetComponent<EnemyController>();
        SetDifficulty(ec);
    }
    private void SetDifficulty(EnemyController ec)
    {
        ec.dm = aiHandler.ec.dm;
        ec.damageCrits = aiHandler.ec.damageCrits;
        ec.lm = aiHandler.ec.lm;
        ec.GetComponent<AiHandler>().damage *= 2;
        ec.GetComponent<AiHandler>().pc = aiHandler.pc;
        ec.lvlm = aiHandler.ec.lvlm;
        ec.chestTier = 2;
        ec.maxHp += Mathf.RoundToInt(ec.maxHp * 0.5f * 2);
        ec.minGoldDrop += Mathf.RoundToInt(ec.minGoldDrop * 0.5f * 2);
        ec.maxGoldDrop += Mathf.RoundToInt(ec.maxGoldDrop * 0.5f * 2);
        foreach (DamageType dt in ec.strength)
        {
            float val = dt.value + (dt.value * 2 * 0.5f);
            dt.value = Mathf.RoundToInt(val);
        }
    }
}
