using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    public string BossName;
    public EndArea ea;
    public AudioClip bgm;
    public PlayerControl pc;

    public override void Setup()
    {
        UpdateHpUI();
        base.Setup();
    }
    public override void UpdateHpUI()
    {
        pc.guiManager.BossHPBarUpdate(this);
        if (invincible)
        {
            pc.guiManager.BossHPBarInvincible();
        }
        else
        {
            pc.guiManager.BossHPBarVulnerable();
        }
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
