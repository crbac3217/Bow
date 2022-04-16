using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveRenderer : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public LineRenderer lineRenderer;
    public Vector3[] pointArray = new Vector3[] { };
    public int vertCount = 20;
    void Update()
    {
        RenderCurve();
        //var pointList = new List<Vector3>();

        //for (float ratio = 0; ratio <= 1; ratio += 1 / vertCount)
        //{
        //var tangent1 = Vector3.Lerp(point1.position, point2.position, ratio);
        //var tangent2 = Vector3.Lerp(point2.position, point3.position, ratio);
        //var curve = Vector3.Lerp(tangent1, tangent2, ratio);

        //pointList.Add(curve);
        //}

        //lineRenderer.positionCount = pointList.Count;
        //lineRenderer.SetPositions(pointList.ToArray());
    }
    void RenderCurve()
    {
        lineRenderer.positionCount = vertCount + 1;
        lineRenderer.SetPositions(pointArray);
    }
}
