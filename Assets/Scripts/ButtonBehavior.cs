using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    public Orders orders;
    public OrderTable orderTable;
    public PlayerController playerController;

    public Image pickUpIcon;

    public bool buttonPressed;

    private void Start()
    {
        pickUpIcon.enabled = false;
    }

    private void Update()
    {
        //Clicar no butão
        if (playerController.hasItem == false)
        {
            if (playerController.isInRangeOfButton == true)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, maxDistance: 1000f))
                {
                    if (hit.collider.gameObject.tag == "Button")
                    {
                        playerController.canPressButton = true;
                    }
                }
            }
            else
                playerController.canPressButton = false;
        }
        else if (playerController.hasItem == true)
            pickUpIcon.enabled = false;

        //Ver combinação da mesa     

        if (buttonPressed == true && orderTable.hasItem == true)
        {
            for (int i = 0; i < orders.ReactionSymbols.Length; i++)
            {            
                if(orderTable.bottleBehavior.elementName.Equals(orders.ReactionSymbols[i]))
                {
                    orders.orderComplete = true;
                }
            }

            if (orders.orderComplete == false)
                orders.orderFailed = true;

            Destroy(orderTable.itemInTable);
            orderTable.itemInTable = null;
            orderTable.hasItem = false;

            buttonPressed = false;
        }       
    }

    #region Triggers

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController.isInRangeOfButton = true;
            pickUpIcon.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController.isInRangeOfButton = true;
            pickUpIcon.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController.isInRangeOfButton = false;
            pickUpIcon.enabled = false;
        }
    }

    #endregion
}
