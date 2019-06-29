using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FieldNotebook : MonoBehaviour
{
    Animator anim;
    public GameObject fuckedUpPage;
    private int currentPage = 0;
    public int numOfPages = 6;

    private bool hasChangedPage = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        VRTK_SDKManager.instance.scriptAliasLeftController.GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += DoChangePage;
    }

    private void OnDisable()
    {
        VRTK_SDKManager.instance.scriptAliasLeftController.GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged -= DoChangePage;
    }

    private void DoChangePage(object sender, ControllerInteractionEventArgs e)
    {
        //print("Raw input = " + e.touchpadAxis.x);
        int pageChangeIndex = 0;

        if (e.touchpadAxis.x > 0.3f)
        {
            pageChangeIndex = 1;
            ChangePage(pageChangeIndex);
        }
        else if(e.touchpadAxis.x < -0.3f)
        {
            pageChangeIndex = -1;
            ChangePage(pageChangeIndex);
        }
        else
        {
            hasChangedPage = false;
        }

        
    }

    private void ChangePage(int pageChange)
    {
        
        if (!hasChangedPage)
        {
            if (currentPage == 4 && pageChange == -1)
            {
                fuckedUpPage.SetActive(false);
                print("hide the lies");
            }
            else
            {
                fuckedUpPage.SetActive(true);
            }

            

            print("input: " + pageChange);

            hasChangedPage = true;
            currentPage = (int)Mathf.Clamp(currentPage + pageChange, 0, numOfPages);
            print(currentPage);
            anim.SetInteger("CurrentPage", currentPage);

            print(currentPage);
        }

    }
}
