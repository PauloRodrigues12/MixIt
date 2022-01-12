using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxBehavior : MonoBehaviour
{
    #region Variáveis

    public BottleBehavior bottleBehavior;
    private PlayerController playerController;

    public Transform bottleSpawn;

    public Image pickUpIcon;

    public string boxName = "";

    #endregion

    #region Start e Update

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pickUpIcon.enabled = false;
    }

    private void Update()
    {
        if (playerController.hasItem == false)
        {
            if (playerController.isInRangeOfBox == true)
            {             
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, maxDistance: 1000f))
                {
                    if (hit.collider.gameObject.tag == "Crate")
                    {
                        playerController.canOpenBox = true;
                        playerController.boxBehavior = hit.collider.gameObject.GetComponent<BoxBehavior>();
                    }
                }
            }
            else
                playerController.canOpenBox = false;
        }
        else if (playerController.hasItem == true)
            pickUpIcon.enabled = false;
    }

    #endregion

    #region Triggers

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController.isInRangeOfBox = true;
            pickUpIcon.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController.isInRangeOfBox = true;
            pickUpIcon.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController.isInRangeOfBox = false;
            pickUpIcon.enabled = false;
        }
    }

    #endregion
}
