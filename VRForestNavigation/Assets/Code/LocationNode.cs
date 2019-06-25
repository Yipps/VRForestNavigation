using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LocationNode : MonoBehaviour
{ 
    public Transform teleportLocation;

    public List<LocationNodeEdge> edges = new List<LocationNodeEdge>();

    public List<VRTK_DestinationPoint> VRTKDestinations = new List<VRTK_DestinationPoint>();

    private void Start()
    {
        UpdateVRTKDestinations(this);
    }

    private void UpdateVRTKDestinations(LocationNode targetNode)
    {
        for (int index = 0; index < targetNode.edges.Count; index++)
        {
            //Update the VRTKDestination based on the edge connection end point
            targetNode.VRTKDestinations[index].destinationLocation = targetNode.edges[index].endNode.teleportLocation;
            targetNode.VRTKDestinations[index].name = "ExitPoint_" + targetNode.edges[index].endNode.name;
        }
    }
}
