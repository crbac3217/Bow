using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArangSpawn", menuName = "EnemyAttack/Boss/Arang/ArangSpawn", order = 105)]
public class ArangSpawn : EnemyAttack
{
    public ArangAI aai;
    public GameObject particle;
    public override void SetUp()
    {
        base.SetUp();
        aai = aiHandler.GetComponent<ArangAI>();
    }
    public override void AttackEtc(PlayerControl pc)
    {
        base.AttackEtc(pc);
        SpawnMob(new Vector2(aiHandler.nextNode.position.x, aiHandler.nextNode.position.y + 1));
    }
    public void SpawnMob(Vector2 pos)
    {
        GameObject temp = Instantiate(projectilePrefab, pos, Quaternion.identity);
        GameObject part = Instantiate(particle, pos, Quaternion.identity);
        EnemyController ec = temp.GetComponent<EnemyController>();
        temp.GetComponent<ButterflyController>().aai = aai;
        aai.butterflies.Add(temp);
        SetDifficulty(ec);
    }
    private void SetDifficulty(EnemyController ec)
    {
        ec.dm = aiHandler.ec.dm;
        ec.damageCrits = aiHandler.ec.damageCrits;
        ec.lm = aiHandler.ec.lm;
        ec.lvlm = aiHandler.ec.lvlm;
        ButterFlyCont bf = ec.GetComponent<ButterFlyCont>();
        bf.minX = aai.minX;
        bf.maxX = aai.maxX;
        bf.minY = aai.minY;
        bf.maxY = aai.maxY;
        bf.pc = aiHandler.pc;
    }
}
