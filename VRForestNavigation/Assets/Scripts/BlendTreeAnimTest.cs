using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeAnimTest : MonoBehaviour
{

    public int currentPage;
    public int maxPages = 7;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            NextPage();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            PreviousPage();
        }
    }

    void NextPage()
    {
        currentPage++;
        currentPage = Mathf.Clamp(currentPage, 0, maxPages);
        //animator.SetFloat("Blend", (float)(currentPage / maxPages));
        animator.SetInteger("currentPage", currentPage);
        Debug.Log("Blend");
    }

    void PreviousPage()
    {
        currentPage--;
        currentPage = Mathf.Clamp(currentPage, 0, maxPages);
        //animator.SetFloat("Blend", (float)(currentPage / maxPages));
        animator.SetInteger("currentPage", currentPage);
    }
}
