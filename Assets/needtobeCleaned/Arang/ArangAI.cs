using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArangAI : BossAi
{
    public List<GameObject> butterflies = new List<GameObject>();
    public float minX, minY, maxX, maxY;

    public override void SetUp()
    {
        base.SetUp();
        if (ea)
        {
            minX = ea.transform.position.x + 0.5f;
            minY = ea.transform.position.y + 1;
            maxX = ea.end.transform.position.x - 0.5f;
            maxY = ea.transform.position.y + 3;
        }
    }
    public override void Dead()
    {
        base.Dead();
        foreach (GameObject bf in butterflies)
        {
            bf.GetComponent<ButterflyController>().Dead(false);
        }
        butterflies.Clear();
    }
}
