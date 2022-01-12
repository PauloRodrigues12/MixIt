using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSmoke : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(playerController.hasItem == true)
            if(playerController.ObjectIwantToPickUp.tag == "Extinguisher")
                transform.rotation = Quaternion.Euler(-90f, 90f, player.transform.eulerAngles.y - 90f);
    }
}
