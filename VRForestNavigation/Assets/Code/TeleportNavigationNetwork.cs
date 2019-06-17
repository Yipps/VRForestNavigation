using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportNavigationNetwork : MonoBehaviour
{
    public GraphEdge[] NodeEdges;


    public class GraphEdge
    {
        public LocationNode start;
        public LocationNode end;
        public int weight;

        public GraphEdge(LocationNode start, LocationNode end, int weight)
        {
            this.start = start;
            this.end = end;
            this.weight = weight;
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
