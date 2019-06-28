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
        StartCoroutine(TeleportCouroutine());
    }

    // Update is called once per frame
    void Update()
    {
        VRTK_HeightAdjustTeleport teleporter = FindObjectOfType<VRTK_HeightAdjustTeleport>();

        teleporter.Teleport(StartingNode.teleportLocation, StartingNode.teleportLocation.position);
    }

    IEnumerator TeleportCouroutine()
    {
        yield return new WaitForSeconds(2f);
        print("try to teleport");
        VRTK_HeightAdjustTeleport teleporter = FindObjectOfType<VRTK_HeightAdjustTeleport>();

        teleporter.Teleport(StartingNode.teleportLocation, StartingNode.teleportLocation.position);
    }
}
