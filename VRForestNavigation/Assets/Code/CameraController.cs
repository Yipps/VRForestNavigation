using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 
 public class CameraController : MonoBehaviour
{
    public Camera[] cameras;
    private int currentCameraIndex;

    public GameObject mapPin;
    public Text mapLabel;

    // Use this for initialization
    void Start()
    {
        currentCameraIndex = 0;

        //Turn all cameras off, except the first default one
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        //If any cameras were added to the controller, enable the first one
        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
            Debug.Log("Camera with name: " + cameras[0].GetComponent<Camera>().name + ", is now enabled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the c button is pressed, switch to the next camera
        //Set the camera at the current index to inactive, and set the next one in the array to active
        //When we reach the end of the camera array, move back to the beginning or the array.
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentCameraIndex++;
            Debug.Log("C button has been pressed. Switching to the next camera");
            if (currentCameraIndex < cameras.Length)
            {
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                cameras[currentCameraIndex].gameObject.SetActive(true);
                Debug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
            }
            else
            {
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                currentCameraIndex = 0;
                cameras[currentCameraIndex].gameObject.SetActive(true);
                Debug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
            }
        }

        /*
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentCameraIndex--;
            Debug.Log("X button has been pressed. Switching to the previous camera");
            if (currentCameraIndex < 0)
            {
                currentCameraIndex = 0;
            }
            else
            {
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                currentCameraIndex = 0;
                cameras[currentCameraIndex].gameObject.SetActive(true);
                Debug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
            }
        } */

        if (Input.GetKeyDown(KeyCode.M))
        {
            cameras[currentCameraIndex].gameObject.SetActive(false);
            currentCameraIndex = 0;
            cameras[currentCameraIndex].gameObject.SetActive(true);
            Debug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
            /*if (cameras[0].enabled == true)
            {
                foreach (GameObject level in GameObject.FindGameObjectsWithTag("Level"))
                {
                    GameObject pin;
                    Text label;
               
                    pin = Instantiate(mapPin, new Vector3(level.transform.position.x, level.transform.position.y + 10, level.transform.position.z), transform.rotation);
                    //label = Instantiate(mapPin, new Vector3(level.transform.position.x, level.transform.position.y + 10, level.transform.position.z), transform.rotation);
                    //label.text = level.name;
                    //pin.transform.position.y += 10;
                }
                print("Hint off");
            }*/
        }



    }
}