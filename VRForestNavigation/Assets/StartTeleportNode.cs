using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StartTeleportNode : MonoBehaviour
{
    public LocationNode StartingNode;
    // Start is called before the first frame update
    void Start()
    {
        VRTK_HeightAdjustTeleport teleporter = FindObjectOfType<VRTK_HeightAdjustTeleport>();

        teleporter.Teleport(StartingNode.teleportLocation, StartingNode.teleportLocation.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
