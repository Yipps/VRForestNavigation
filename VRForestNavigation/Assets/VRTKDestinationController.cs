using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRTKDestinationController : MonoBehaviour
{

    private LocationNode targetLocationNode;
    private LocationNode currentLocationNode;

    private LocationNode lastLocation;

    VRTK_DestinationPoint currentDestinationPoint;
    // Start is called before the first frame update
    void Start()
    {
        currentLocationNode = transform.parent.GetComponent<LocationNode>();
        currentDestinationPoint = GetComponent<VRTK_DestinationPoint>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDestinationRotation()
    {
        targetLocationNode = currentDestinationPoint.destinationLocation.parent.GetComponent<LocationNode>();

        Vector3 playerSourceDestination = new Vector3();
        foreach (VRTK_DestinationPoint vrtkDestinations in targetLocationNode.VRTKDestinations)
        {
            if (vrtkDestinations.destinationLocation == currentLocationNode.teleportLocation)
            {
                //Found the VRTKDestination from where you came
                playerSourceDestination = vrtkDestinations.transform.position;
            }
        }

        targetLocationNode.teleportLocation.transform.rotation = Quaternion.LookRotation(targetLocationNode.teleportLocation.position - playerSourceDestination);


    }
}
