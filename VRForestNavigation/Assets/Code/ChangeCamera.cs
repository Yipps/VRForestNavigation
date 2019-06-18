using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Camera[] cameras;
    public KeyCode[] cameraKeys = new KeyCode[] { KeyCode.F1, KeyCode.F2, KeyCode.F3, KeyCode.F4, KeyCode.F5, KeyCode.F6 };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cameraKeys.Length; i++)
        {
            if (Input.GetKeyDown(cameraKeys[i]))
            {
                for (int j = 0; j < cameras.Length; j++)
                {
                    cameras[j].enabled = (i == j) ? true : false;
                }
            }
        }

        //if (OVRInput.GetDown(OVRInput.Button.One))
        //{
            //GetNextElement(cameras, 6);
        //}
    }


  /*  public string GetNextElement(Camera[] cameras, int index)
    {
        //if ((index > cameras.Length - 1) || (index < 0))
            //throw new Exception("Invalid index");

        if (index == cameras.Length - 1)
            index = 0;

        else
            cameras[index++].enabled = true;

        //return cameras[index];
    }*/



}
