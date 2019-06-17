using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using VRTK;

[CustomEditor(typeof(LocationNode))]
public class LocationNodeEditor : Editor
{
    LocationNode targetNode;

    SerializedProperty connections;

    GameObject ExitPointPrefab;

    private void OnEnable()
    {
        targetNode = (LocationNode)target;
        connections = serializedObject.FindProperty("edges");
        
        TeleportNavigationNetwork currentNetwork = FindObjectOfType<TeleportNavigationNetwork>();

        if(currentNetwork == null)
        {
            GameObject newSystem = new GameObject("TeleportNetwork");
            newSystem.AddComponent<TeleportNavigationNetwork>();
            targetNode.transform.parent = newSystem.transform;
        }
        else 
        {
            if (PrefabUtility.GetPrefabInstanceStatus(targetNode.gameObject) != PrefabInstanceStatus.NotAPrefab)
            {
                targetNode.transform.parent = currentNetwork.transform;
            }
            
            
        }


        ExitPointPrefab = AssetDatabase.LoadAssetAtPath("Assets/Level/Prefabs/P_ExitPoint.prefab", typeof(GameObject)) as GameObject;

    }

    private void OnSceneGUI()
    {
        GUIStyle VRTKStyle = new GUIStyle();
        VRTKStyle.fontSize = 20;
        VRTKStyle.alignment = TextAnchor.MiddleCenter;
        VRTKStyle.normal.textColor = new Color(.71f, .68f, .94f);
        VRTKStyle.fontStyle = FontStyle.Bold;

        GUIStyle locationPointStyle = new GUIStyle();
        locationPointStyle.fontSize = 30;
        locationPointStyle.alignment = TextAnchor.UpperCenter;
        locationPointStyle.normal.textColor = Color.yellow;

        GUIStyle teleportLocationSytle = new GUIStyle();
        teleportLocationSytle.fontSize = 20;
        teleportLocationSytle.normal.textColor = Color.white;
        teleportLocationSytle.alignment = TextAnchor.UpperCenter;
        teleportLocationSytle.fontStyle = FontStyle.Bold;

        Handles.DrawWireCube(targetNode.transform.position + Vector3.up, new Vector3(2, 2, 1.5f));
        Handles.DrawAAConvexPolygon(new Vector3[] {targetNode.teleportLocation.position + Vector3.left * 0.2f, targetNode.teleportLocation.position + Vector3.right * 0.2f, targetNode.teleportLocation.position + Vector3.forward * 0.5f});

        Handles.Label(targetNode.transform.position + Vector3.up * 3f, targetNode.name, locationPointStyle);

        foreach (VRTK_DestinationPoint VRTKDestination in targetNode.VRTKDestinations)
        {
            Handles.Label(VRTKDestination.transform.position + Vector3.up * 1.5f, "[" + VRTKDestination.name + "]", VRTKStyle);
            VRTKDestination.transform.position = Handles.PositionHandle(VRTKDestination.transform.position, VRTKDestination.transform.rotation); 
        }

        targetNode.teleportLocation.position = Handles.PositionHandle(targetNode.teleportLocation.position, targetNode.teleportLocation.rotation);
        Handles.Label(targetNode.teleportLocation.position + Vector3.up, "Teleport Location", teleportLocationSytle);
        

    }

    public override void OnInspectorGUI()
    {


        RemoveNulls(targetNode);

        GUILayout.Label("Connected Locations");
        GUILayout.BeginVertical("Box");
        for (int x = 0; x < targetNode.edges.Count; x++)
        {

            GUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 80;
            //Create a proprty field for the target node in a new edge
            SerializedProperty edge = connections.GetArrayElementAtIndex(x);
            SerializedProperty endNode = edge.FindPropertyRelative("endNode");
            SerializedProperty weight = edge.FindPropertyRelative("weight");

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(endNode,new GUIContent("Destination"));
            EditorGUILayout.PropertyField(weight, new GUIContent("| Weight"));
            serializedObject.ApplyModifiedProperties();

            //When a destination is set
            if (EditorGUI.EndChangeCheck())
            {
                if (targetNode.edges[x].endNode != null)
                {
                    UpdateVRTKDestinations(targetNode, x);

                    //if (isUndirected)
                    //{
                    //    UpdateOppositeLocationNodes(targetNode.edges[x]);
                    //}
                }     
            }

            if (GUILayout.Button("x", GUILayout.Width(20)))
            {
                DeleteConnection(x);
            }

            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        //if (GUILayout.Button("Add new connected location", GUILayout.Width(200)))
        //{
        //    AddNewConnection();
        //}

        if (GUILayout.Button("Add new empty connection", GUILayout.Width(200)))
        {
            AddEmptyConnection(targetNode);
        }

        
        base.OnInspectorGUI();
    }

    private void DeleteConnection(int index)
    {
        targetNode.edges.RemoveAt(index);
        DestroyImmediate(targetNode.VRTKDestinations[index].gameObject);
        targetNode.VRTKDestinations.RemoveAt(index);
    }

    private void AddEmptyConnection(LocationNode targetNode)
    {

        LocationNodeEdge edge = new LocationNodeEdge(targetNode, null, -1);
        targetNode.edges.Add(edge);
        AddExitPoints(targetNode);
    }

    private void AddNewConnection()
    {
        GameObject newLocation = new GameObject("RENAME_ME");
        LocationNode newNode = newLocation.AddComponent<LocationNode>();

        LocationNodeEdge edge = new LocationNodeEdge(targetNode, newNode, -1);
        LocationNodeEdge edgeReversed = new LocationNodeEdge(newNode, targetNode, -1);


        targetNode.edges.Add(edge);
        newNode.edges.Add(edgeReversed);
    }

    //If a ExitPoint containing the VRTKDestination was deleted, remove the corrosponding edge
    private void RemoveNulls(LocationNode targetNode)
    {
        for (int x = targetNode.VRTKDestinations.Count - 1; x >= 0; x--)
        {
            if (targetNode.VRTKDestinations[x] == null)
            {
                targetNode.VRTKDestinations.RemoveAt(x);
                targetNode.edges.RemoveAt(x);
            }
        }
    }

    private void AddExitPoints(LocationNode targetNode)
    {
        GameObject exitPoint = (GameObject)PrefabUtility.InstantiatePrefab(ExitPointPrefab, targetNode.transform);
        targetNode.VRTKDestinations.Add(exitPoint.GetComponent<VRTK_DestinationPoint>());
        exitPoint.name = "NO_DESTINATION";
        exitPoint.transform.localPosition = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f)); ;
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

    private void UpdateVRTKDestinations(LocationNode targetNode, int index)
    {
        //Update the VRTKDestination based on the edge connection end point
        targetNode.VRTKDestinations[index].destinationLocation = targetNode.edges[index].endNode.teleportLocation;
        targetNode.VRTKDestinations[index].name = "ExitPoint_" + targetNode.edges[index].endNode.name;

    }

    private void UpdateOppositeLocationNodes(LocationNodeEdge targetEdge)
    {
        LocationNode oppositeLocationNode = targetEdge.endNode;
        LocationNode currentNode = targetEdge.startNode;

        bool hasCorrospondingExitPoint = false;

        //Does the opposite location node have a corrosponding exitpoint to this current node
        for(int x = 0; x < oppositeLocationNode.edges.Count; x++)
        {
            if (oppositeLocationNode.edges[x].endNode == currentNode)
            {
                hasCorrospondingExitPoint = true;
                oppositeLocationNode.edges[x].weight = targetEdge.weight;
            }
                
        }

        if (!hasCorrospondingExitPoint)
        {
            LocationNodeEdge corrospondingEdge = new LocationNodeEdge(oppositeLocationNode, currentNode, targetEdge.weight);
            oppositeLocationNode.edges.Add(corrospondingEdge);
            AddExitPoints(oppositeLocationNode);
            UpdateVRTKDestinations(oppositeLocationNode);
        }


    }
}
