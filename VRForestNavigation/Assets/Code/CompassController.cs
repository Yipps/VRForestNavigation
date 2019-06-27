using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CompassController : MonoBehaviour
{
    public GameObject magneticNorthArrow;
    public GameObject orientationArrow;
    public GameObject up;

    public float magneticNorthSpeed;
    public float orientationArrowSpeed;

    private float orientationMovementAmt;
    // Start is called before the first frame update
    void Start()
    {
        //Get VRTK_ControllerEvent and listen to event?
        VRTK_SDKManager.instance.scriptAliasRightController.GetComponent<VRTK_ControllerEvents>().TouchpadTouchStart += DoStartCompassMovement;
        VRTK_SDKManager.instance.scriptAliasRightController.GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd += DoStopCompassMovement;
        VRTK_SDKManager.instance.scriptAliasRightController.GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += DoSetOrientationMovementAmount;
    }

    private void OnDisable()
    {
        VRTK_SDKManager.instance.scriptAliasRightController.GetComponent<VRTK_ControllerEvents>().TouchpadTouchStart -= DoStartCompassMovement;
        VRTK_SDKManager.instance.scriptAliasRightController.GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd -= DoStopCompassMovement;
        VRTK_SDKManager.instance.scriptAliasRightController.GetComponent<VRTK_ControllerEvents>().TouchpadSenseAxisChanged -= DoSetOrientationMovementAmount;
    }

    // Update is called once per frame
    void Update()
    {
        FaceMagneticNorth();

        if (Input.GetKey("left"))
        {
            print("left");
        }

        if (Input.GetKey("right"))
        {
            print("right");
        }
    }

    private void FaceMagneticNorth()
    {

        Vector3 velocity = Vector3.zero;

        Vector3 newRotation = Vector3.SmoothDamp(magneticNorthArrow.transform.forward, -Vector3.right, ref velocity, magneticNorthSpeed);
        //MagneticNorthArrow.transform.localRotation
        Quaternion lookdir = Quaternion.LookRotation(newRotation, transform.up);

        magneticNorthArrow.transform.rotation = lookdir;
        
        //MagneticNorthArrow.transform.LookAt(newRotation, transform.parent.up);
        //MagneticNorthArrow.transform.forward = newRotation;
        //MagneticNorthArrow.transform.rotation = Quaternion.LookRotation(newRotation, transform.parent.up);
    }

    IEnumerator CompassMovementCoroutine()
    {
        while (true)
        {
            orientationArrow.transform.Rotate(new Vector3(0, orientationArrowSpeed * orientationMovementAmt, 0));
            yield return new WaitForEndOfFrame();
        }
    }

    private void DoStartCompassMovement(object sender, ControllerInteractionEventArgs e)
    {
        print("Touching");
        StartCoroutine(CompassMovementCoroutine());
    }

    private void DoStopCompassMovement(object sender, ControllerInteractionEventArgs e)
    {
        print("Stop touching");
        StopAllCoroutines();
    }

    public void DoSetOrientationMovementAmount(object sender, ControllerInteractionEventArgs e)
    {
        
        float amt = e.touchpadAxis.x;

        if (Mathf.Abs(amt) < .15)
        {
            amt = 0;
        }
        print(amt);

        orientationMovementAmt = amt;

    }




}
