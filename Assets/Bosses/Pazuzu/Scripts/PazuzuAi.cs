using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PazuzuAi : BossAi
{
    public override void Dead()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
        AddSnareStack();
        visuals.GetComponent<Animator>().SetTrigger("Death");
        StopCoroutine(CheckPlayer());
    }
}
