using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    public string BossName;
    public EndArea ea;
    public PlayerControl pc;

    public override void Setup()
    {
        base.Setup();
    }
    public override void UpdateHpUI()
    {
        pc.guiManager.BossHPBarUpdate(this);
        if (hp/maxHp < 0.45f)
        {
            aiHandler.isAffectedByCC = false;
        }
    }
    public override void Drop()
    {
        base.Drop();
        ea.BossDefeated();
    }
}
