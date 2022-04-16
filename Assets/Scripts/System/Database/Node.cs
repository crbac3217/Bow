using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour
{
    public Vector2 position;
    public Platform platform;
    public bool isEnd;
    public bool isRight;
    public bool isLeft;
    private Node leftNode, rightNode;
    public float offset;
    private Dictionary<Platform, Node> minConPerPlat = new Dictionary<Platform, Node>();
    public List<Node> connectedNodes, nodeCandidates = new List<Node>();
    public List<Connection> connections = new List<Connection>();
    public LevelManager lm;
    public void SetUpConnection(float distance)
    {
        //List<Node> nodeCandidates = new List<Node>();
        foreach (Collider2D col in Physics2D.OverlapCircleAll(position, distance))
        {
            if (col.gameObject.layer == gameObject.layer)
            {
                if (col.gameObject != this.gameObject)
                {
                    nodeCandidates.Add(col.gameObject.GetComponent<Node>());
                    if (col.gameObject.GetComponent<Node>().platform != platform)
                    {
                        if (!minConPerPlat.ContainsKey(col.gameObject.GetComponent<Node>().platform))
                        {
                            minConPerPlat.Add(col.gameObject.GetComponent<Node>().platform, null);
                        }
                    }
                }
            }
        }
        foreach (Node n in nodeCandidates)
        {
            if (n.platform != platform)
            {
                CheckCandidate(n);
            }
            else
            {
                CheckSamePlatform(n);
            }
        }
        if (rightNode != null)
        {
            CreateConnection(rightNode, Mathf.Abs(position.y - rightNode.position.y) > 0.1f);
        }
        if (leftNode != null)
        {
            CreateConnection(leftNode, Mathf.Abs(position.y - leftNode.position.y) > 0.1f);
        }
        foreach (Platform pf in minConPerPlat.Keys)
        {
            if (minConPerPlat[pf] != null)
            {
                CreateConnection(minConPerPlat[pf], true);
            }
        }
    }
    public void CheckCandidate(Node n)
    {
        //divide into quadrants
        //if target node is higher
        if (n.position.y  >=  position.y)
        {
            //if target node is on the right top and is a left-End node
            if (n.position.x + offset > position.x && n.isLeft)
            {
                CheckMinimum(n);
            }
            //if target node is on the left top and is a right-End node
            else if (n.position.x < position.x + offset && n.isRight)
            {
                CheckMinimum(n);
            }
        }
        else
        //if target node is lower
        {
            //if base node is an end node
            if (isEnd)
            {
                //if target node is on the right bottom and base node is a right-End node
                if (n.position.x + offset > position.x && isRight)
                {
                    CheckMinimum(n);
                }
                //if target node is on the left bottom and base node is a left-End node
                else if (n.position.x < position.x + offset && isLeft)
                {
                    CheckMinimum(n);
                }
            }
        }
    }
    //    {
    //        //if the target node is on the same platform
    //        if (n.platform == platform)
    //        {
    //            //if it is within a walkable y-range
    //            if (Mathf.Abs(position.y - n.position.y) < 0.2f)
    //            {
    //                //check which one is point 1
    //                if (position.x > n.position.x)
    //                {
    //                    CreateConnection(n, false);
    //                }
    //                else
    //                {
    //                    CreateConnection(n, false);
    //                }
    //            }
    //            else
    //            {
    //                //otherwise you have to jump it
    //                if (position.x > n.position.x)
    //                {
    //                    CreateConnection(n, true);
    //                }
    //                else
    //                {
    //                    CreateConnection(n, true);
    //                }
    //            }
    //        }
    //    }
    //}

    private void CheckMinimum(Node n)
    {
        if (minConPerPlat[n.platform] == null)
        {
            minConPerPlat[n.platform] = n;
        }
        else
        {
            if (Vector2.Distance(n.position, transform.position) < Vector2.Distance(minConPerPlat[n.platform].position, transform.position))
            {
                minConPerPlat[n.platform] = n;
            }
        }
    }
    public void CheckSamePlatform(Node n)
    {
        //if the target is to the right of the base node
        if (n.position.x - position.x > 0)
        {
            if (rightNode == null)
            {
                rightNode = n;
            }
            else if ((n.position.x - position.x) < (rightNode.position.x - position.x))
            {
                rightNode = n;
            }
        }
        else
        //if the target is to the right of the base node
        {
            if (leftNode == null)
            {
                leftNode = n;
            }
            else if ((n.position.x - position.x) > (leftNode.position.x - position.x))
            {
                leftNode = n;
            }
        }
    }
    public void CreateConnection(Node n, bool jump)
    {
        if (!n.connectedNodes.Contains(this))
        {
            Connection temp = new Connection
            {
                nodeTo = n,
                jump = jump,
                distance = Vector2.Distance(position, n.position)
            };
            connectedNodes.Add(n);
            connections.Add(temp);
            Connection temp2 = new Connection
            {
                nodeTo = this,
                jump = jump,
                distance = Vector2.Distance(position, n.position)
            };
            n.connectedNodes.Add(this);
            n.connections.Add(temp2);
            CreateVisuallizer(this, n, jump);
        }
    }
    public void CreateVisuallizer(Node firstNode, Node secondNode, bool jump)
    {
        GameObject visualizer = new GameObject("Visualizer");
        visualizer.AddComponent<LineRenderer>();
        LineRenderer lr = visualizer.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, firstNode.position);
        lr.SetPosition(1, secondNode.position);
        lr.widthMultiplier = 0.01f;
        if (jump)
        {
            lr.startColor = Color.red;
            lr.endColor = Color.red;
        }
        else
        {
            lr.startColor = Color.green;
            lr.endColor = Color.green;
        }
    }

    //if the base node is 
    ////if a node is an endnode
    //if (isEnd)
    //{
    //    //if the node is a rightend node
    //    if (isRight)
    //    {
    //        //if the target node is on the right side of the base node
    //        if (n.position.x > position.x)
    //        {
    //            //if the target node is on the right top corner of the base node
    //            if (n.position.y > position.y)
    //            {
    //                //only target nodes that are on the left end of a platform can connect
    //                if (n.isEnd && !n.isRight)
    //                {
    //                    CreateConnection(n, true);
    //                }
    //            }
    //            else
    //            // if the target node is on the right bottom side of the base node, any kind of node can make a connection
    //            {
    //                CreateConnection(n, true);
    //            }
    //        }
    //        else
    //        // if the node is on the left end of the base node
    //        {
    //            //if the node is on the left top of the base node (right end)
    //            if (n.position.y > position.y)
    //            {
    //                if (n.isEnd && n.isRight)
    //                {
    //                    CreateConnection(n, true);
    //                }
    //            }
    //        }

    //    }
    //    else
    //    //if the node is a left-end Node
    //    {
    //        if (n.position.x < position.x)
    //        {
    //            //if the node is on the left top of the base node
    //            if (n.position.y > position.y)
    //            {
    //                if (n.isEnd && n.isRight)
    //                {
    //                    CreateConnection(n, true);
    //                }
    //            }
    //            else
    //            //if the node is on the left bottom of the base node
    //            {
    //                CreateConnection(n, true);
    //            }
    //        }
    //        //if the node is on the right end of the base node
    //        else
    //        {
    //            //if the node is on the right top of the base node
    //            if (n.position.y > position.y)
    //            {
    //                if (n.isEnd && !n.isRight)
    //                {
    //                    CreateConnection(n, true);
    //                }
    //            }
    //        }
    //    }
}
