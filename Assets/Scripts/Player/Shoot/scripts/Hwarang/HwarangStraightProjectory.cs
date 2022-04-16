using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HwarangStraightProjectory", menuName = "Projectory/Hwarang/Straight", order = 110)]
public class HwarangStraightProjectory : Projectory
{
    public int vertCount;
    public LayerMask canHit;
    public float range;
    public override void InvokeProjectory(ProjArgs Pa)
    {
        Pa.projLinei.positionCount = vertCount;
        Pa.projLinei.SetPosition(0, Pa.firePos);
        Pa.projLinei.SetPosition(vertCount - 1, EndPos(Pa.firePos, Pa.pdir));
    }
    private Vector2 EndPos(Vector2 start, Vector2 direction)
    {
        Vector2 defaultEnd = start + direction.normalized * range;
        var hit = Physics2D.Linecast(start, defaultEnd, canHit);
        if (hit)
        {
            return hit.point;
        }
        return defaultEnd;
    }
}
