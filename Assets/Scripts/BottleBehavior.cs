using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleBehavior : MonoBehaviour
{

    #region Variáveis

    private PlayerController playerController;

    public string elementName = "";

    public Image pickUpIcon;

    public bool pickup; 

    #endregion

    #region Start e Update

    private void Start()
    {
        pickUpIcon.enabled = false;

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();  
    }

    private void Update()
    {
        GameObject[] bottles = GameObject.FindGameObjectsWithTag("Bottle");
        for (int i = 0; i < bottles.Length; i++)
        {
            Physics.IgnoreCollision(bottles[i].GetComponent<CapsuleCollider>(), GetComponent<SphereCollider>());
            Physics.IgnoreCollision(bottles[i].GetComponent<SphereCollider>(), GetComponent<SphereCollider>());
        }

        if (playerController.hasItem == false)
        {
            if (playerController.isInRangeOfBottle == true)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, maxDistance: 1000f))
                {
                    if (hit.collider.gameObject.tag == "Bottle")
                    {
                        playerController.canPickup = true;

                        playerController.bottleBehavior = hit.collider.gameObject.GetComponent<BottleBehavior>();
                        playerController.ObjectIwantToPickUp = hit.collider.gameObject;                    
                    }
                }
            }
            else
                playerController.canPickup = false;
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
            playerController.isInRangeOfBottle = true;
            pickUpIcon.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController.isInRangeOfBottle = true;
            pickUpIcon.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController.isInRangeOfBottle = false;
            pickUpIcon.enabled = false;
        }
    }

    #endregion
}
