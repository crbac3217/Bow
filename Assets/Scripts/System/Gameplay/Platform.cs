using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Color color;
    public List<Node> nodes;

    public void SetNode()
    {
        foreach (Node node in nodes)
        {
            node.platform = this;
            node.offset = -0.35f;
            node.position = node.transform.position;
        }
    }
    public void SetUpConnection(float distance)
    {
        foreach (Node node in nodes)
        {
            node.SetUpConnection(distance);
        }
    }
}
