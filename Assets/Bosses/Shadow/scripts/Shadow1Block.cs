using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shadow1Block", menuName = "EnemyAttack/Boss/Shadow/Shadow1Block", order = 105)]
public class Shadow1Block : EnemyAttack
{
    public GameObject block;
    public LayerMask ground;
    private ShadowAi sai;


    public override void SetUp()
    {
        base.SetUp();
        sai = aiHandler.GetComponent<ShadowAi>();
    }
    public override void AttackEtc(PlayerControl pc)
    {
        Vector2 markPos = new Vector2(aiHandler.visuals.transform.position.x + aiHandler.visuals.transform.localScale.x, sai.groundLevel);
        base.AttackEtc(pc);
        GameObject temp = Instantiate(block, markPos, Quaternion.identity);
        temp.transform.localScale = aiHandler.visuals.transform.localScale;
    }
}
