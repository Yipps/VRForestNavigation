using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VRTK;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] leftHandObjects;
    public GameObject[] rightHandObjects;

    private int currentLeftObjectIndex = 0;
    private int currentRightObjectIndex = 0;

    private int currentTimeMin = 800;



    


    private void Start()
    {
        for(int x = 0; x < leftHandObjects.Length; x++)
        {
            if (x == currentLeftObjectIndex)
            {
                leftHandObjects[x].SetActive(true);
            }
            else
            {
                leftHandObjects[x].SetActive(false);
            }
        }

        for (int x = 0; x < rightHandObjects.Length; x++)
        {
            if (x == currentRightObjectIndex)
            {
                rightHandObjects[x].SetActive(true);
            }
            else
            {
                rightHandObjects[x].SetActive(false);
            }
        }

        TimeSpan ts = TimeSpan.FromMinutes(currentTimeMin);
        print(ts);
    }

    private void OnEnable()
    {
        VRTK_SDKManager.instance.scriptAliasLeftController.GetComponent<VRTK_ControllerEvents>().ButtonOnePressed += DoNextHandObject;
    }

    private void OnDisable()
    {
        VRTK_SDKManager.instance.scriptAliasLeftController.GetComponent<VRTK_ControllerEvents>().ButtonOnePressed -= DoNextHandObject;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            NextHandObject(0);
        } 

    }

    void ChangeHandObject(int handIndex, int objectIndex)
    {
        if(handIndex == 0)
        {
            leftHandObjects[currentLeftObjectIndex].SetActive(false);
            leftHandObjects[objectIndex].SetActive(true);
            currentLeftObjectIndex = objectIndex;
        }
        else
        {
            rightHandObjects[currentRightObjectIndex].SetActive(false);
            rightHandObjects[objectIndex].SetActive(true);
            currentRightObjectIndex = objectIndex;
        }
    }

    void NextHandObject(int handIndex)
    {
        if (handIndex == 0)
        {
            currentRightObjectIndex++;
            currentRightObjectIndex %= leftHandObjects.Length;
            ChangeHandObject(0, currentRightObjectIndex);
        }
        else
        {
            currentLeftObjectIndex++;
            handIndex %= rightHandObjects.Length;
            ChangeHandObject(1, currentRightObjectIndex);
        }
    }

    private void DoNextHandObject(object sender, ControllerInteractionEventArgs e)
    {
        NextHandObject(0);
    }







}
