using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTable : MonoBehaviour
{
    public bool hasItem = false;
    public GameObject itemInTable;
    public BottleBehavior bottleBehavior;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bottle")
        {
            itemInTable = other.gameObject;
            bottleBehavior = other.gameObject.GetComponent<BottleBehavior>();
            hasItem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bottle")
        {
            hasItem = false;
            bottleBehavior = null;
            itemInTable = null;
        }
    }
}
