using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBillboard : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    Vector3 lookAtPosition;

    void Start(){
        
        if(player == null){
        	player =  GameObject.FindWithTag("MainCamera");
        }
        
    }

    // Update is called once per frame
    void Update(){
    	lookAtPosition = player.transform.position;
    	lookAtPosition.y = this.transform.position.y;
        transform.LookAt(lookAtPosition);
    }
}
