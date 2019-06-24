using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassController : MonoBehaviour
{
    public GameObject MagneticNorthArrow;
    public GameObject OrientationArrow;
    public GameObject up;

    public float MagneticNorthSpeed;
    public float OrientationArrowSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FaceMagneticNorth();

        if (Input.GetKey("left"))
        {
            print("left");
            RotateOrientationArrow(-1);
        }

        if (Input.GetKey("right"))
        {
            print("right");
            RotateOrientationArrow(1);
        }
    }

    private void FaceMagneticNorth()
    {

        Vector3 velocity = Vector3.zero;

        Vector3 newRotation = Vector3.SmoothDamp(MagneticNorthArrow.transform.forward, -Vector3.right, ref velocity, MagneticNorthSpeed);
        //MagneticNorthArrow.transform.localRotation
        Quaternion lookdir = Quaternion.LookRotation(newRotation, transform.up);

        MagneticNorthArrow.transform.rotation = lookdir;
        
        //MagneticNorthArrow.transform.LookAt(newRotation, transform.parent.up);
        //MagneticNorthArrow.transform.forward = newRotation;
        //MagneticNorthArrow.transform.rotation = Quaternion.LookRotation(newRotation, transform.parent.up);
    }

    private void RotateOrientationArrow(float rotateAmount)
    {
        OrientationArrow.transform.Rotate(new Vector3(0, OrientationArrowSpeed * rotateAmount, 0));
    }
}
