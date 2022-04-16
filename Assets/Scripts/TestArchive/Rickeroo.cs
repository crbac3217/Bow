using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rickeroo : MonoBehaviour
{
    public GameObject deadEnd;
    public Node startNode, endNode, tempNode;
    public List<Node> unavail = new List<Node>();
    public Path finalPath, tempPath = new Path();
    public bool finalized = false;
    public float jumpStr;
    public List<Path> pathCandidates = new List<Path>();

    public void findpath(bool canJump)
    {
        while (!finalized)
        {
            if (tempNode == null)
            {
                tempNode = startNode;
                tempPath.nodes.Add(tempNode);
            }
            //CreateNewPath(canJump);
        }
    }
    //public void CreateNewPath(bool canJump)
    //{
    //    //set up the minimum so we can find the minimum
    //    Connection mincon = new Connection { };
    //    float minval = 10;
    //    //check all the connecting nodes within the target node
    //    foreach (Connection con in tempNode.connections)
    //    {
    //        //looking for the minimum distance
    //        if (con.distance + Mathf.Abs(endNode.position.x - con.nodeTo.position.x) + Mathf.Abs(endNode.position.y - con.nodeTo.position.y) < minval)
    //        {
    //            bool avail = true;
    //            //if we just came from the node
    //            foreach (Node n in tempPath.nodes)
    //            {
    //                if (n == con.nodeTo)
    //                {
    //                    avail = false;
    //                }
    //            }
    //            //if we are retracing, and know that this will lead to a dead end
    //            foreach (Node n in unavail)
    //            {
    //                if (n == con.nodeTo)
    //                {
    //                    avail = false;
    //                }
    //            }
    //            //if the enemy can't jump
    //            if (con.jump)
    //            {
    //                if (!canJump)
    //                {
    //                    avail = false;
    //                    //check Jumpstr
    //                }
    //                else if (jumpStr < con.distance)
    //                {
    //                    avail = false;
    //                }
    //            }
                
    //            //if it meets the condition
    //            if (avail)
    //            {
    //                mincon = con;
    //                minval = con.distance + Mathf.Abs(con.nodeTo.position.x - endNode.position.x) + Mathf.Abs(con.nodeTo.position.y - endNode.position.y);
    //            }
    //        }
    //    }
    //    //if there is a result (if it is not the end)
    //    if (mincon.nodeTo != null)
    //    {
    //        tempPath.nodes.Add(mincon.nodeTo);
    //        tempPath.connections.Add(mincon);
    //        tempPath.totalDistance += mincon.distance;
    //        //if the result is not the end node
    //        if (mincon.nodeTo != endNode)
    //        {
    //            drawvisuallizer(mincon.nodeTo, tempNode, mincon.jump);
    //            tempNode = mincon.nodeTo;
    //        }
    //        else
    //        //if the result is the end node
    //        {
    //            drawvisuallizer(mincon.nodeTo, tempNode, mincon.jump);
    //            tempPath.hasEnded = true;
    //            tempPath.didConnect = true;
    //            finalPath = tempPath;
    //            Debug.Log("Complete");
    //            finalized = true;
    //        }
    //    }
    //    else
    //    //if there is no result(has reached the end)
    //    {
    //        tempPath.hasEnded = true;
    //        //if there are other possibilities
    //        if (tempPath.nodes.Count > 1)
    //        {
    //            tempPath.didConnect = false;
    //            unavail.Add(tempPath.nodes[tempPath.nodes.Count - 1]);
    //            drawUnavailable(tempPath.nodes[tempPath.nodes.Count - 1]);
    //            Debug.Log("Dead End");
    //        }
    //        else
    //        //if there is no way you can reach the end-node
    //        {
    //            finalized = true;
    //            Path finalCandidate = new Path();
    //            float pathMaxVal = 0;
    //            foreach (Path p in pathCandidates)
    //            {
    //                if (p.totalDistance > pathMaxVal)
    //                {
    //                    pathMaxVal = p.totalDistance;
    //                    finalCandidate = p;
    //                }
    //            }
    //            finalPath = finalCandidate;
    //            Debug.Log("Can't Reach");
    //        }
    //        //instantiate the path and add it to the candidates
    //        Path temp = tempPath.Clone();
    //        pathCandidates.Add(temp);
    //        tempPath = new Path();
    //        tempNode = null;
    //    }
    //}
    public void drawUnavailable(Node firstnode)
    {
        GameObject unav = Instantiate(deadEnd, firstnode.position, Quaternion.identity);
    }
    public void drawvisuallizer(Node firstnode, Node secondnode, bool jump)
    {
        GameObject visualizer = new GameObject("visualizer");
        visualizer.AddComponent<LineRenderer>();
        LineRenderer lr = visualizer.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, firstnode.position);
        lr.SetPosition(1, secondnode.position);
        lr.widthMultiplier = 0.03f;
        if (jump)
        {
            lr.startColor = Color.cyan;
            lr.endColor = Color.cyan;
        }
        else
        {
            lr.startColor = Color.blue;
            lr.endColor = Color.blue;
        }
    }
}
