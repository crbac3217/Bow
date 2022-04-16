using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigwiAi : BossAi
{
    public override void MoveToNextNode()
    {
        if (nextNode.position.x > transform.position.x)
        {
            visuals.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            visuals.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
        }
    }
}
