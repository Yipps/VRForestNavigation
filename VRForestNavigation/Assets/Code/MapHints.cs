using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHints : MonoBehaviour
{
    //public GameObject map;
    public Material NoHints;
    public Material Hints;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            ToggleHintOn();
        }
        if (Input.GetKeyDown("n"))
        {
            ToggleHintOff();
        }
    }

    public void ToggleHintOff()
    {
        foreach (GameObject map in GameObject.FindGameObjectsWithTag("Map"))
        {
            //Do something to ObjectFound, like this:
            map.GetComponent<Renderer>().material = NoHints;

        }
        print("Hint off");
    }

    public void ToggleHintOn()
    {
        print("Hint on");
        Debug.Log("Hint on");
        foreach (GameObject map in GameObject.FindGameObjectsWithTag("Map"))
        {
            //Do something to ObjectFound, like this:
            map.GetComponent<Renderer>().material = Hints;

        }
    }




}
