using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArangShoot", menuName = "EnemyAttack/Boss/Arang/ArangShoot", order = 105)]
public class ArangShoot : EnemyAttack
{
    public ArangAI aai;

    public override void SetUp()
    {
        base.SetUp();
        aai = aiHandler.GetComponent<ArangAI>();
    }
    public override void Activate()
    {
        base.Activate();
        foreach (GameObject go in aai.butterflies)
        {
            go.GetComponent<ButterFlyCont>().Shoot();
        }
    }
}
