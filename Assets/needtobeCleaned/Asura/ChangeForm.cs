using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsuraChangeForm", menuName = "EnemyAttack/Boss/Asura/ChangeForm", order = 105)]
public class ChangeForm : EnemyAttack
{
    public AsuraAi aai;

    public override void SetUp()
    {
        base.SetUp();
        aai = aiHandler.GetComponent<AsuraAi>();
    }
    public override void Activate()
    {
        aiHandler.StartCoroutine(aiHandler.StopMoving(duration));
        aiHandler.StartCoroutine(aai.FormChange());
    }
}
