using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HwarangDefaultProjectory", menuName = "Projectory/Hwarang/Default", order = 110)]
public class HwarangDefaultProjectory : Projectory
{
    public int vertCount;
    public LayerMask canHit;
    public Vector2 position1, velocity;
    public float yLimit;
    public override void InvokeProjectory(ProjArgs Pa)
    {
        float speed = (Pa.held / Pa.joyIndi + (Pa.indicator * Pa.indiMult)) * Pa.speedMult;
        float hSpeed = speed * Mathf.Cos(Pa.angle);
        float vSpeed = speed * Mathf.Sin(Pa.angle);
        velocity = new Vector2(hSpeed, vSpeed);
        position1 = Pa.firePos;
        canHit = Pa.pmask;
        yLimit = Pa.ylim;
        Pa.projLinei.positionCount = vertCount;
        Pa.projLinei.SetPositions(CalculateLineArray());
    }
    private Vector3[] CalculateLineArray()
    {
        Vector3[] lineArray = new Vector3[vertCount + 1];

        var lowestTimeValue = MaxTimeX() / vertCount;

        for (int i = 0; i < lineArray.Length; i++)
        {
            var t = lowestTimeValue * i;
            lineArray[i] = CalculateLinePoint(t);
        }

        return lineArray;
    }

    private Vector2 HitPosition()
    {
        var lowestTimeValue = MaxTimeY() / vertCount;

        for (int i = 0; i < vertCount + 1; i++)
        {
            var t = lowestTimeValue * i;
            var tt = lowestTimeValue * (i + 1);

            var hit = Physics2D.Linecast(CalculateLinePoint(t), CalculateLinePoint(tt), canHit);

            if (hit)
                return hit.point;
        }

        return CalculateLinePoint(MaxTimeY());
    }
    private Vector3 CalculateLinePoint(float t)
    {
        float x = velocity.x * t;
        float y = (velocity.y * t) - (gravity * Mathf.Pow(t, 2) / 2);
        return new Vector3(x + position1.x, y + position1.y);
    }

    private float MaxTimeY()
    {
        var v = velocity.y;
        var vv = v * v;

        var t = (v + Mathf.Sqrt(vv + 2 * gravity * (position1.y - yLimit))) / gravity;
        return t;
    }
    private float MaxTimeX()
    {
        var x = velocity.x;
        if (x == 0)
        {
            velocity.x = 000.1f;
            x = velocity.x;
        }

        var t = (HitPosition().x - position1.x) / x;
        return t;
    }
}

//if (Pa.isgreater)
//{
//    float speed = (Pa.held / Pa.joyIndi + (Pa.indicator * Pa.indiMult)) * Pa.speedMult;
//    float hSpeed = speed * Mathf.Cos(Pa.angle);
//    float vSpeed = speed * Mathf.Sin(Pa.angle);
//    float xDist = 2 * hSpeed * vSpeed / gravity;
//    float yDist = vSpeed * vSpeed * 1.75f / (2 * gravity); 
//    float disp = 5;
//    float timeTheta = (-vSpeed - Mathf.Sqrt(vSpeed * vSpeed - (4 * -gravity / 2 * disp))) / -gravity;
//    Pa.acurve.point3.transform.position = new Vector2(Pa.acurve.point1.transform.position.x + (hSpeed * timeTheta), Pa.acurve.point1.transform.position.y - disp);
//    Pa.acurve.point2.transform.position = new Vector2((Pa.acurve.point1.transform.position.x + Pa.acurve.point1.transform.position.x + xDist) / 2, Pa.acurve.point1.transform.position.y + yDist);
//}
//else
//{
//    float speed = (Pa.held / Pa.joyIndi + (Pa.indicator * Pa.indiMult)) * Pa.speedMult;
//    float hSpeed = speed * Mathf.Cos(Pa.angle);
//    float vSpeed = speed * Mathf.Sin(Pa.angle);
//    float xDist = 2 * hSpeed * vSpeed / gravity;
//    float yDist = vSpeed * vSpeed * 1.75f / (2 * gravity);
//    float disp = -5;
//    float timeTheta = (-vSpeed - Mathf.Sqrt(vSpeed * vSpeed - (4 * -gravity / 2 * disp))) / -gravity;
//    Pa.acurve.point3.transform.position = new Vector2(Pa.acurve.point1.transform.position.x + (hSpeed * timeTheta), Pa.acurve.point1.transform.position.y - disp);
//    Pa.acurve.point2.transform.position = new Vector2((Pa.acurve.point1.transform.position.x + Pa.acurve.point3.transform.position.x) / 2, (Pa.acurve.point1.transform.position.y + Pa.acurve.point3.transform.position.y) / 2);
//}
