using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelController : MonoBehaviour
{
    public GameObject mapPin;
    public GameObject label;
    public Text labelText;

    public GameObject worldMapCam;

    // Start is called before the first frame update
    void Start()
    {
        if (mapPin.activeSelf)
        {
            //set map pin and label to invisible
            mapPin.SetActive(false);
            label.SetActive(false);
            
            //change name of pin to location name
            labelText.text = this.name;

        }
    }

    // Update is called once per frame
    void Update()
    {
        //on keypress of m, show labels and pins
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Show/hide pin");
            mapPin.SetActive(!mapPin.activeSelf);

        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Show/hide label");
      
            label.SetActive(!label.activeSelf);
            //label.transform.LookAt(worldMapCam.transform.position, Vector3.up);
        }
        //label.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        
    }
}
