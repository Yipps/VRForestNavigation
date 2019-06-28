using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FieldNotebook : MonoBehaviour
{
    Animator anim;
    private int currentPage = 0;
    public int numOfPages = 8;

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
        int pageChangeIndex = 1;

        if(e.touchpadAxis.x < 0)
        {
            pageChangeIndex = -1;
        }

        ChangePage(pageChangeIndex);
    }

    private void ChangePage(float pageChange)
    {
        currentPage = (int) Mathf.Clamp(currentPage + pageChange, 0, numOfPages);
        anim.SetInteger("CurrentPage", currentPage);
    }
}
