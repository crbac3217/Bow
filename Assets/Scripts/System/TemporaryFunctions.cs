using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryFunctions : MonoBehaviour
{
    public PlayerControl pc;
    public DamageManager dm;
    public Skill from, to;
    public GameObject EnemyPrefab, spawnPos;
    public List<Platform> platforms;

    public void skillSwap()
    {
        pc.ReplaceSkill(from.skillName, to);
    }
    public void SetCrit(int amount)
    {
        pc.stats[3].value = amount;
    }
    public void spawnGgomul()
    {
        GameObject temp = Instantiate(EnemyPrefab, spawnPos.transform.position, Quaternion.identity);
        temp.GetComponent<EnemyController>().dm = dm;
        temp.GetComponent<EnemyController>().damageCrits = pc.playerType.critList;
    }
    public void SetConnections(float distance)
    {
        foreach (Platform pf in platforms)
        {
            Debug.Log(pf.gameObject.name + "has been created");
            pf.SetUpConnection(distance);
        }
    }
    //public void FindPath()
    //{
    //    if (platforms == null)
    //    {
    //        Debug.Log("punch sean gogogo");
    //    }
    //    if (platforms[0] == null)
    //    {
    //        Debug.Log("never gonna give you up");
    //    }
    //    if (platforms[5] == null)
    //    {
    //        Debug.Log("never gonna let you down");
    //    }
    //    if (node1 == null)
    //    {
    //        Debug.Log("tell me why");
    //    }
    //    if (node2 == null)
    //    {
    //        Debug.Log("tell me why");
    //    }
    //    List<Path> possiblePaths = new List<Path>();
    //    Connection minCon = new Connection { };
    //    float minVal = 10;
    //    foreach (Connection con in node1.connections)
    //    {
    //        if (con.distance + Vector2.Distance(node1.position, node2.position) < minVal)
    //        {
    //            minCon = con;
    //            minVal = con.distance + Vector2.Distance(node1.position, node2.position);
    //        }
    //    }
    //    DrawVisuallizer(minCon.nodeTo, node1, minCon.jump);
    //}
    //public void DrawVisuallizer(Node firstNode, Node secondNode, bool jump)
    //{
    //    GameObject visualizer = new GameObject("Visualizer");
    //    visualizer.AddComponent<LineRenderer>();
    //    LineRenderer lr = visualizer.GetComponent<LineRenderer>();
    //    lr.positionCount = 2;
    //    lr.SetPosition(0, firstNode.position);
    //    lr.SetPosition(1, secondNode.position);
    //    lr.widthMultiplier = 0.03f;
    //    if (jump)
    //    {
    //        lr.startColor = Color.cyan;
    //        lr.endColor = Color.cyan;
    //    }
    //    else
    //    {
    //        lr.startColor = Color.blue;
    //        lr.endColor = Color.blue;
    //    }
    //}
}
