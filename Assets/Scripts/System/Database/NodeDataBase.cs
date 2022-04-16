using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path
{
    public List<Node> nodes = new List<Node>();
    public bool didConnect;
    public float totalDistance;
    //public Path Clone() => new Path
    //{
    //    nodes = this.nodes,
    //    didConnect = this.didConnect,
    //    totalDistance = this.totalDistance
    //};
}

[System.Serializable]
public class Connection
{
    public Node nodeTo;
    public bool jump;
    public float distance;
}