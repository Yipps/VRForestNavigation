using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VRTK;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject[] leftHandObjects;
    public GameObject[] rightHandObjects;

    public GameObject Sun;
    private Material skyMat;

    private int currentLeftObjectIndex = 0;
    private int currentRightObjectIndex = 0;

    public int currentTimeInMinutes = 725;

    public int endTimeInMinutes = 1200;

    public Color startColor;
    public Color endColor;

    public float startExposure = .9f;
    public float endExposure = .6f;

    public float startSunIntensity = 1f;
    public float endSunIntensity = 3f;

    private Vector3 startAngle = new Vector3(80, -90, 0);
    private Vector3 endAngle = new Vector3(160, -90, 0);



    private void Awake()
    {
        instance = this;
        skyMat = RenderSettings.skybox;
    }

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

        
    }

    public void UpdateDaylight()
    {
        Sun.transform.rotation = Quaternion.Euler(Vector3.Lerp(startAngle, endAngle, (currentTimeInMinutes - 725f) / (endTimeInMinutes - 725f)));
        float currentExposure = Mathf.Lerp(startExposure, endExposure, (currentTimeInMinutes - 725f) / (endTimeInMinutes - 725f));
        Color currentTint = Color.Lerp(startColor, endColor, (currentTimeInMinutes - 725f) / (endTimeInMinutes - 725f));
        float currentSunIntensity = Mathf.Lerp(startSunIntensity,endSunIntensity, (currentTimeInMinutes - 725f) / (endTimeInMinutes - 725f));

        print((currentTimeInMinutes - 725f) / (endTimeInMinutes - 725f));
        skyMat.SetColor("_TintColor", currentTint);
        skyMat.SetFloat("_Exposure", currentExposure);

        Sun.GetComponent<Light>().intensity = currentSunIntensity;
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
