using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocationNodeEdge
{
    public LocationNode startNode;
    public LocationNode endNode;
    public int weight;

    public LocationNodeEdge(LocationNode start, LocationNode end, int weight)
    {
        this.startNode = start;
        this.endNode = end;
        this.weight = weight;
    }
}
