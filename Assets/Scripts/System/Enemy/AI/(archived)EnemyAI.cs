using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "EnemyAI", menuName = "EnemyAI/Default", order = 105)]
public class EnemyAI: ScriptableObject
{
    //public AggressionType aggro;
    //public bool canJump;
    //public float jumpStr, defSpeed, chaseSpeed;
    //public Rigidbody2D rb;
    //public PlayerControl pc;
    //public AiHandler ai;
    //public Path currentPath;
    //public Node curNode, nextNode;
    ////priv
    //public Path tempPath, finalPath;
    //public Node temp, tempNode;
    //public List<Path> candidates = new List<Path>();
    //public List<Node> unavail = new List<Node>();

    //public void SetUpAi()
    //{
        
    //}
    //public Node CurrentNode(float minVal)
    //{
    //    temp = null;
    //    float min = minVal;
    //    foreach (Collider2D col in Physics2D.OverlapCircleAll(ai.transform.position, 2, pc.pm.layerMask))
    //    {
    //        if (Vector2.Distance(col.transform.position, ai.transform.position) < min)
    //        {
    //            min = Vector2.Distance(col.transform.position, ai.transform.position);
    //            temp = col.GetComponent<Node>();
    //        }
    //    }
    //    if (min != minVal)
    //    {
    //        return temp;
    //    }
    //    else
    //    {
    //        return curNode;
    //    }
    //}
    //public Node NextNode()
    //{
    //    //if (aggro == AggressionType.Melee)
    //    return currentPath.nodes[1];
    //}
    //public void CreatePath()
    //{
    //    //if chaser
    //    CurrentNode(5);
    //    currentPath = PathAStar(curNode, pc.pm.nearestNode, canJump, jumpStr);
    //    //if skittish / kiting
    //}
    //public Path PathAStar(Node startNode, Node endNode, bool canJump, float jumpStr)
    //{
    //    candidates.Clear();
    //    unavail.Clear();
    //    tempPath = new Path();
    //    tempNode = null;
    //    temp = null;
    //    finalPath = new Path();
    //    bool finalized = false;
    //    while (!finalized)
    //    {
    //        if (tempNode == null)
    //        {
    //            tempNode = startNode;
    //            tempPath.nodes.Add(tempNode);
    //        }
    //        Connection mincon = new Connection { };
    //        float minval = 10;
    //        //check all the connecting nodes within the target node
    //        foreach (Connection con in tempNode.connections)
    //        {
    //            //looking for the minimum distance
    //            if (con.distance + Mathf.Abs(endNode.position.x - con.nodeTo.position.x) + Mathf.Abs(endNode.position.y - con.nodeTo.position.y) < minval)
    //            {
    //                bool avail = true;
    //                //if we just came from the node
    //                foreach (Node n in tempPath.nodes)
    //                {
    //                    if (n == con.nodeTo)
    //                    {
    //                        avail = false;
    //                    }
    //                }
    //                //if we are retracing, and know that this will lead to a dead end
    //                foreach (Node n in unavail)
    //                {
    //                    if (n == con.nodeTo)
    //                    {
    //                        avail = false;
    //                    }
    //                }
    //                //if the enemy can't jump
    //                if (con.jump)
    //                {
    //                    if (!canJump)
    //                    {
    //                        avail = false;
    //                        //check Jumpstr
    //                    }
    //                    else if (jumpStr < con.distance)
    //                    {
    //                        avail = false;
    //                    }
    //                }

    //                //if it meets the condition
    //                if (avail)
    //                {
    //                    mincon = con;
    //                    minval = con.distance + Mathf.Abs(con.nodeTo.position.x - endNode.position.x) + Mathf.Abs(con.nodeTo.position.y - endNode.position.y);
    //                }
    //            }
    //        }
    //        //if there is a result (if it is not the end)
    //        if (mincon.nodeTo != null)
    //        {
    //            tempPath.nodes.Add(mincon.nodeTo);
    //            tempPath.connections.Add(mincon);
    //            tempPath.totalDistance += mincon.distance;
    //            //if the result is not the end node
    //            if (mincon.nodeTo != endNode)
    //            {
    //                tempNode = mincon.nodeTo;
    //            }
    //            else
    //            //if the result is the end node
    //            {
    //                tempPath.hasEnded = true;
    //                tempPath.didConnect = true;
    //                finalPath = tempPath;
    //                Debug.Log("Complete");
    //                finalized = true;
    //            }
    //        }
    //        else
    //        //if there is no result(has reached the end)
    //        {
    //            tempPath.hasEnded = true;
    //            //if there are other possibilities
    //            if (tempPath.nodes.Count > 1)
    //            {
    //                tempPath.didConnect = false;
    //                unavail.Add(tempPath.nodes[tempPath.nodes.Count - 1]);
    //                Debug.Log("Dead End");
    //            }
    //            else
    //            //if there is no way you can reach the end-node
    //            {
    //                finalized = true;
    //                Path finalCandidate = new Path();
    //                float pathMaxVal = 0;
    //                foreach (Path p in candidates)
    //                {
    //                    if (p.totalDistance > pathMaxVal)
    //                    {
    //                        pathMaxVal = p.totalDistance;
    //                        finalCandidate = p;
    //                    }
    //                }
    //                finalPath = finalCandidate;
    //                Debug.Log("Can't Reach");
    //            }
    //            //instantiate the path and add it to the candidates
    //            Path temp = tempPath.Clone();
    //            candidates.Add(temp);
    //            tempPath = null;
    //            tempNode = null;
    //        }
    //    }
    //    return finalPath;
    //}
    //public void Interact()
    //{
    //    if (nextNode == null)
    //    {
    //        CreatePath();
    //        nextNode = NextNode();
    //    }
    //    else
    //    {
    //        rb.velocity = Vector2.MoveTowards(curNode.position, nextNode.position, chaseSpeed) * Time.deltaTime;
    //    }
    //    if (Vector2.Distance(ai.transform.position, curNode.position) > Vector2.Distance(ai.transform.position, nextNode.position))
    //    {
    //        CreatePath();
    //        NextNode();
    //    }
    //}
}


//if (aggro == AggressionType.Skittish)
//{

//}
//else if (aggro == AggressionType.Reactive)
//{

//}
//else if (aggro == AggressionType.Melee)
//{

//}
//else if (aggro == AggressionType.Shooting)
//{

//}
//else if (aggro == AggressionType.Kiting)
//{

//}
