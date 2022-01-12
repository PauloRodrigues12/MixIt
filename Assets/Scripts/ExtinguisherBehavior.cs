using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtinguisherBehavior : MonoBehaviour
{
    #region Variáveis

    private PlayerController playerController;

    private GameObject player;

    public Image pickUpIcon;

    public bool pickup;

    public ParticleSystem smoke;

    public AudioSource audioSource;

    #endregion

    #region Start e Update

    private void Start()
    {
        smoke.Stop();
        pickUpIcon.enabled = false;

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
    }

    private void Update()
    {
        if (playerController.hasItem == false)
        {
            if (playerController.isInRangeOfBottle == true)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, maxDistance: 1000f))
                {
                    if (hit.collider.gameObject.tag == "Extinguisher")
                    {
                        playerController.canPickup = true;

                        playerController.extinguisherBehavior = hit.collider.gameObject.GetComponent<ExtinguisherBehavior>();
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

            if(playerController.hasItem == false)
                playerController.ObjectIwantToPickUp = null;
        }
    }

    #endregion
}
