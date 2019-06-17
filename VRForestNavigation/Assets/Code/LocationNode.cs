using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LocationNode : MonoBehaviour
{ 
    public Transform teleportLocation;

    public List<LocationNodeEdge> edges = new List<LocationNodeEdge>();

    public List<VRTK_DestinationPoint> VRTKDestinations = new List<VRTK_DestinationPoint>();
    
}
