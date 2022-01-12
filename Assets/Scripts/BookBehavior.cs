using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookBehavior : MonoBehaviour
{
    public PlayerController playerController;
    private GameObject player;
    public Image pickUpIcon;
    public Camera cam;
    private Animator anim;

    public bool bookPressed;

    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("AnimCam").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        pickUpIcon.enabled = false;
        anim.enabled = false;
    }

    private void Update()
    {
        //Clicar no butão
        if (playerController.hasItem == false)
        {
            if (playerController.isInRangeOfBook == true)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, maxDistance: 1000f))
                {
                    if (hit.collider.gameObject.tag == "Book")
                    {
                        playerController.canOpenBook = true;
                    }
                }
            }
            else
                playerController.canOpenBook = false;
        }
        else if (playerController.hasItem == true)
            pickUpIcon.enabled = false;

        if (bookPressed == true)
        {
            pickUpIcon.enabled = false;
            cam.enabled = false;
            anim.enabled = true;
            anim.SetBool("isOpen", true);
            player.SetActive(false);
            bookPressed = false;       
        }
    }

    #region Triggers

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController.isInRangeOfBook = true;
            pickUpIcon.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController.isInRangeOfBook = true;
            pickUpIcon.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController.isInRangeOfBook = false;
            pickUpIcon.enabled = false;
        }
    }

    #endregion
}
