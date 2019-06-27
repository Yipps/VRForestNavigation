using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldNotebook : MonoBehaviour
{
    Animator anim;
    private int currentPage = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            currentPage++;
            print("CurrPage: " + currentPage);
            anim.SetInteger("CurrentPage", currentPage);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            currentPage--;
            print("CurrPage: "+ currentPage);
            anim.SetInteger("CurrentPage", currentPage);
        }
    }
}
