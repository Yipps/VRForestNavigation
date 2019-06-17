using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRTK;

[CustomEditor(typeof(TeleportNavigationNetwork))]
public class TeleportNavigationNetworkEditor : Editor
{
    TeleportNavigationNetwork navNet;
    LocationNode[] nodes;
    GUIStyle locationPointStyle;


    private void OnEnable()
    {
        navNet = (TeleportNavigationNetwork)target;
        nodes = navNet.GetComponentsInChildren<LocationNode>();

        locationPointStyle = new GUIStyle();
        locationPointStyle.fontSize = 20;
        locationPointStyle.alignment = TextAnchor.UpperCenter;
        locationPointStyle.normal.textColor = Color.yellow;
    }

    private void OnSceneGUI()
    {
        foreach(LocationNode node in nodes)
        {
            Handles.Label(node.transform.position + Vector3.up * 2, node.name, locationPointStyle);

            foreach(VRTK_DestinationPoint VRTKDestination in node.VRTKDestinations)
            {
                if (VRTKDestination.destinationLocation != null)
                    Handles.DrawLine(VRTKDestination.transform.position + Vector3.up, VRTKDestination.destinationLocation.transform.position);
            }
        }
    }
}
