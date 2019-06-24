using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBillboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = Camera.main.transform.position;
        //Making sure the tree only rotates around the y-axis
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        transform.Rotate(0, 180, 0);
    }
}
