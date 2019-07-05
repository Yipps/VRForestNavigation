using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FieldNotebook : MonoBehaviour
{
    Animator anim;
    public GameObject[] pageMeshes;
    private GameObject brokenPage;

    private AudioSource audioSource;
    private int currentPage = 0;
    public int numOfPages = 6;

    private bool hasChangedPage = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        anim.keepAnimatorControllerStateOnDisable = true;
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

        if (e.touchpadAxis.x > 0.2f)
        {
            pageChangeIndex = -1;
            ChangePage(pageChangeIndex);
        }
        else if(e.touchpadAxis.x < -0.2f)
        {
            pageChangeIndex = 1;
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
            //Going Backwards
            if (pageChange == -1)
            {
                if (brokenPage != null)
                    brokenPage.SetActive(true);


                if (currentPage == 4)
                {
                    brokenPage = pageMeshes[0];
                    brokenPage.SetActive(false);
                }
                //else if (currentPage == 5)
                //{
                //    brokenPage = pageMeshes[2];
                //    brokenPage.SetActive(false);
                //}
                else if (currentPage == 6)
                {
                    brokenPage = pageMeshes[2];
                    brokenPage.SetActive(false);
                }
            //Going forward
            }else if(pageChange == 1)
            {
                if (brokenPage != null && currentPage != 6)
                    brokenPage.SetActive(true);

                //Does not look correct because a page disappears before the page turns to hide it
                if (currentPage == 5 )
                {
                    StartCoroutine(DelayPageHide());
                }

                if(currentPage == 3)
                {
                    brokenPage = pageMeshes[0];
                    brokenPage.SetActive(false);
                }
            }

            if (currentPage == 4 && pageChange == -1)
            {
                //messedUpPage1.SetActive(false);
                print("hide the lies");
            }
            else
            {
                //messedUpPage1.SetActive(true);
            }

            

            print("input: " + pageChange);
            if(currentPage + pageChange >= 0 && currentPage + pageChange <= 6)
            {
                audioSource.Play();
            }
            hasChangedPage = true;
            currentPage = (int)Mathf.Clamp(currentPage + pageChange, 0, numOfPages);
            print(currentPage);
            anim.SetInteger("CurrentPage", currentPage);

            print(currentPage);
        }

    }

    IEnumerator DelayPageHide()
    {
        yield return new WaitForSeconds(0.5f);
        brokenPage = pageMeshes[3];
        brokenPage.SetActive(false);
    }
}
