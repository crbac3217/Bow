using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAi : AiHandler
{
    public CameraParent campar;
    public EndArea ea;

    public override void SetUp()
    {
        base.SetUp();
        nodeSearch *= 2;
    }
}
