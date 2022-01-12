using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    #region Variáveis

    public float speed;

    public Transform myHands;
    
    public GameObject ObjectIwantToPickUp;
    public GameObject[] Bottles;

    public CharacterController player;
    public BottleBehavior bottleBehavior;
    public BoxBehavior boxBehavior;
    public ExtinguisherBehavior extinguisherBehavior;
    public ButtonBehavior buttonBehavior;
    public BookBehavior bookBehavior;
    private Animator anim;

    public bool canPickup;
    public bool hasItem;
    public bool isInRangeOfBottle;
    public bool isInRangeOfBox;
    public bool isInRangeOfButton;
    public bool isInRangeOfBook;
    public bool canOpenBox;
    public bool canPressButton;
    public bool canOpenBook;

    public TextMeshProUGUI elementName;

    private AudioSource audioSourcePlayer;
    public AudioSource audioSourceBottles;
    public AudioClip[] bottleSounds;
    public AudioClip[] steps;
    public AudioClip extinguisher;

    public float stepCooldown;
    public float stepRate;

    #endregion  

    #region Start e Update

    private void Start()
    {
        player = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audioSourcePlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Move();
        Look();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isInRangeOfBottle == true)
                if (hasItem == false)
                    if (canPickup == true)
                        Pickup();
            
            if (isInRangeOfBox == true)
                if (hasItem == false)
                    if (canOpenBox == true)
                        OpenBox();

            if (canPressButton == true)
                if (hasItem == false)
                    PressButton();

            if(isInRangeOfBook == true)
                if (canOpenBook == true)
                    if (hasItem == false)
                        OpenBook();
        }
  
        if (Input.GetKeyDown(KeyCode.Mouse1))
            if(hasItem == true)
                Release();

        if (hasItem == true)
        {
            ObjectIwantToPickUp.transform.position = myHands.position;

            if(ObjectIwantToPickUp.tag == "Bottle")
                elementName.text = bottleBehavior.elementName.ToString();

            anim.SetBool("hasItem", true);
        }

        if(hasItem == false)
        {
            anim.SetBool("hasItem", false);
        }
            
    }

    #endregion

    #region Funções

    #region Movimento

    private void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), -1, Input.GetAxis("Vertical"));
        player.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;        
        }      

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("isMoving", true);
            
            //Som de passos
            stepCooldown -= Time.deltaTime;
            if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && stepCooldown <= 0f)
            {
                int randomNr = Random.Range(0, steps.Length);
                audioSourcePlayer.clip = steps[randomNr];
                audioSourcePlayer.Play();
                stepCooldown = stepRate;
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && hasItem == true)
        {
            anim.SetBool("Both", true);
        }
        else
        {
            anim.SetBool("Both", false);
        }     
    }

    private void Look()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance: 100f))
        {
            var target = hit.point;
            target.y = Camera.main.transform.position.y;

            Vector3 targetPos = new Vector3(target.x, this.transform.position.y, target.z);
            this.transform.LookAt(targetPos);
        }
    }

    #endregion

    #region Interação de Objetos

    private void Pickup()
    {
        ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;
        ObjectIwantToPickUp.GetComponent<CapsuleCollider>().enabled = false;
        ObjectIwantToPickUp.layer = 2; 
        ObjectIwantToPickUp.transform.position = myHands.transform.position;

        canPickup = false;
        hasItem = true;
    }

    private void Release()
    {
        ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false;
        ObjectIwantToPickUp.GetComponent<CapsuleCollider>().enabled = true;
        ObjectIwantToPickUp.layer = 0;

        if(ObjectIwantToPickUp.tag == "Bottle")
        {
            int randomNr = Random.Range(0, bottleSounds.Length);
            audioSourceBottles.clip = bottleSounds[randomNr];
            audioSourceBottles.Play();
        }

        if (ObjectIwantToPickUp.tag == "Extinguisher")
        {
            audioSourceBottles.clip = extinguisher;
            audioSourceBottles.Play();
        }

        ObjectIwantToPickUp = null;
        bottleBehavior = null;

        canPickup = true;
        hasItem = false;
    }

    private void OpenBox()
    {
        //Caixas de Químicos
        if(boxBehavior.boxName == "CBox")
            Instantiate(Bottles[0].gameObject, boxBehavior.bottleSpawn.position, Quaternion.identity);
        if (boxBehavior.boxName == "ClBox")
            Instantiate(Bottles[1].gameObject, boxBehavior.bottleSpawn.position, Quaternion.identity);
        if (boxBehavior.boxName == "FeBox")
            Instantiate(Bottles[2].gameObject, boxBehavior.bottleSpawn.position, Quaternion.identity);
        if (boxBehavior.boxName == "HBox")
            Instantiate(Bottles[3].gameObject, boxBehavior.bottleSpawn.position, Quaternion.identity);
        if (boxBehavior.boxName == "MgBox")
            Instantiate(Bottles[4].gameObject, boxBehavior.bottleSpawn.position, Quaternion.identity);
        if (boxBehavior.boxName == "HgBox")
            Instantiate(Bottles[5].gameObject, boxBehavior.bottleSpawn.position, Quaternion.identity);
        if (boxBehavior.boxName == "NBox")
            Instantiate(Bottles[6].gameObject, boxBehavior.bottleSpawn.position, Quaternion.identity);
        if (boxBehavior.boxName == "OBox")
            Instantiate(Bottles[7].gameObject, boxBehavior.bottleSpawn.position, Quaternion.identity);
        if (boxBehavior.boxName == "AgBox")
            Instantiate(Bottles[8].gameObject, boxBehavior.bottleSpawn.position, Quaternion.identity);
        if (boxBehavior.boxName == "NaBox")
            Instantiate(Bottles[9].gameObject, boxBehavior.bottleSpawn.position, Quaternion.identity);

        canOpenBox = false;
    }

    private void PressButton()
    {
        buttonBehavior.buttonPressed = true;
    }

    private void OpenBook()
    {
        bookBehavior.bookPressed = true;
    }

    #endregion

    #endregion
}
