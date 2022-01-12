using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeReactions : MonoBehaviour
{
    public BottleBehavior bottleBehavior;
    private PlayerController playerController;
    private ExtinguisherBehavior extinguisherBehavior;

    public Transform bottleSpawn;

    public GameObject[] ReactionBottles;
    public GameObject[] UsedBottles;
    public GameObject fire;

    public string[] ReactionNames;

    public string reactionName;

    public bool reactionComplete;
    public bool isOnFire;

    private int reactionCount;
    private int charPos;
    private int char2Pos;

    private void Start()
    {
        fire = GameObject.FindGameObjectWithTag("Fire");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        extinguisherBehavior = GameObject.FindGameObjectWithTag("Extinguisher").GetComponent<ExtinguisherBehavior>();

        fire.SetActive(false);

        reactionName = "";
        reactionComplete = false;
        reactionCount = 0;
    }

    private void Update()
    {
        //Se acertou a reação
        if (reactionComplete == false)
        {
            for (int i = 0; i < ReactionNames.Length; i++)
            {
                if (reactionName != "")
                {
                    if (reactionName.Equals(ReactionNames[i]))
                    {
                        Instantiate(ReactionBottles[i].gameObject, bottleSpawn.position, Quaternion.identity);
                        reactionComplete = true;
                    }
                }
            }
        }

        //Se falhou a reação
        if (reactionCount == 5 && reactionComplete == false)
        {
            isOnFire = true;
            reactionComplete = true;
            playerController.isInRangeOfBottle = false;
        }

        //Reação completa
        if (reactionComplete == true)
        {
            reactionComplete = false;

            reactionCount = 0;
            reactionName = "";

            for (int i = 0; i < UsedBottles.Length; i++)
                Destroy(UsedBottles[i].gameObject);      
        }

        //Fogo
        if (isOnFire == false)
            fire.SetActive(false);
        else if(isOnFire == true)
            fire.SetActive(true);
            
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bottle")
        {
            bottleBehavior = other.gameObject.GetComponent<BottleBehavior>();

            if (bottleBehavior.elementName == "C")
            {
                UsedBottles[reactionCount] = other.gameObject;
                reactionName += bottleBehavior.elementName;
                reactionCount++;
            }
            if (bottleBehavior.elementName == "O")
            {
                UsedBottles[reactionCount] = other.gameObject;
                reactionName += bottleBehavior.elementName;
                reactionCount++;
            }
            if (bottleBehavior.elementName == "H")
            {
                UsedBottles[reactionCount] = other.gameObject;
                reactionName += bottleBehavior.elementName;
                reactionCount++;
            }
            if (bottleBehavior.elementName == "Cl")
            {
                UsedBottles[reactionCount] = other.gameObject;
                reactionName += bottleBehavior.elementName;
                reactionCount++;
            }
            if (bottleBehavior.elementName == "Na")
            {
                UsedBottles[reactionCount] = other.gameObject;
                reactionName += bottleBehavior.elementName;
                reactionCount++;
            }
            if (bottleBehavior.elementName == "Hg")
            {
                UsedBottles[reactionCount] = other.gameObject;
                reactionName += bottleBehavior.elementName;
                reactionCount++;
            }
            if (bottleBehavior.elementName == "Mg")
            {
                UsedBottles[reactionCount] = other.gameObject;
                reactionName += bottleBehavior.elementName;
                reactionCount++;
            }
            if (bottleBehavior.elementName == "Ag")
            {
                UsedBottles[reactionCount] = other.gameObject;
                reactionName += bottleBehavior.elementName;
                reactionCount++;
            }
            if (bottleBehavior.elementName == "N")
            {
                UsedBottles[reactionCount] = other.gameObject;
                reactionName += bottleBehavior.elementName;
                reactionCount++;
            }
            if (bottleBehavior.elementName == "Fe")
            {
                UsedBottles[reactionCount] = other.gameObject;
                reactionName += bottleBehavior.elementName;
                reactionCount++;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        
        //Se tirou uma garrafa
        if (other.gameObject.tag == "Bottle")     
            if (reactionName != "")
            {             
                for (int i = 0; i < reactionName.Length; i++)
                {
                    if (reactionName[i] == other.gameObject.GetComponent<BottleBehavior>().elementName[0])
                    {
                        charPos = i;

                        if (other.gameObject.GetComponent<BottleBehavior>().elementName.Length == 2)
                            char2Pos = i++;
                    }       
                }

                if (other.gameObject.GetComponent<BottleBehavior>().elementName.Length == 1)
                    reactionName = reactionName.Remove(charPos, 1);
                else
                {
                    reactionName = reactionName.Remove(charPos, 1);
                    reactionName = reactionName.Remove(char2Pos, 1);
                }

                bool hasDeletedUsedBottle = false;

                for (int i = 0; i < UsedBottles.Length; i++)
                {
                    if(hasDeletedUsedBottle == false)
                        if(UsedBottles[i].gameObject.GetComponent<BottleBehavior>().elementName == other.gameObject.GetComponent<BottleBehavior>().elementName)
                        {
                            UsedBottles[i] = null;
                            hasDeletedUsedBottle = true;

                            reactionCount--;
                        }
                }

                hasDeletedUsedBottle = false;

                for (int i = 0; i < UsedBottles.Length; i++)
                {
                    if (i != UsedBottles.Length -1)
                    {
                        UsedBottles[i] = UsedBottles[i + 1];
                        UsedBottles[i + 1] = null;
                    }
                }

                reactionCount--;
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOnFire == true)
            if (other.gameObject.tag == "Player" && playerController.ObjectIwantToPickUp.tag == "Extinguisher")
            {          
                extinguisherBehavior.audioSource.enabled = true;
                extinguisherBehavior.smoke.Play();
                isOnFire = false;
                reactionComplete = false;         
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerController.hasItem == true)
        {
            if (other.gameObject.tag == "Player" && playerController.ObjectIwantToPickUp.tag == "Extinguisher")
            {
                extinguisherBehavior.audioSource.enabled = false;
                extinguisherBehavior.smoke.Stop();
            }
        }
    }
}
