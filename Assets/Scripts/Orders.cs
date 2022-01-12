using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Orders : MonoBehaviour
{
    public OrderTable orderTable;

    public TextMeshProUGUI screenReactionSymbol;
    public TextMeshProUGUI screenReactionName;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI timeText;

    public string[] ReactionNames;
    public string[] ReactionSymbols;

    public float timePerOrder;
    private float originalTime;
    private float countdown;
    public int rndNr;

    public int money;
    public int penaltyMoney;
    public int rewardMoney;

    public bool orderComplete = false;
    public bool orderFailed = false;

    private AudioSource audioSource;
    public AudioClip fail;
    public AudioClip complete;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        countdown = timePerOrder;
        originalTime = timePerOrder;

        StartCoroutine(Order());
        StartCoroutine(CountDown());
    }

    private void Update()
    {
        moneyText.text = money.ToString();
        timeText.text = countdown.ToString();

        if(orderFailed == true)
        {
            audioSource.clip = fail;
            audioSource.Play();

            orderFailed = false;
            orderComplete = false;
            money -= penaltyMoney;
            orderTable.itemInTable = null;
            timePerOrder = originalTime;
            countdown = originalTime;
            Debug.Log("Fail");

            int randomNr = Random.Range(0, ReactionNames.Length);
            screenReactionName.text = ReactionNames[randomNr];
            screenReactionSymbol.text = ReactionSymbols[randomNr];
        }
        if(orderComplete == true)
        {
            audioSource.clip = complete;
            audioSource.Play();

            orderComplete = false;
            orderFailed = false;
            money += rewardMoney;
            timePerOrder = originalTime;
            countdown = originalTime;
            orderTable.itemInTable = null;
            Debug.Log("Complete");

            int randomNr = Random.Range(0, ReactionNames.Length);
            screenReactionName.text = ReactionNames[randomNr];
            screenReactionSymbol.text = ReactionSymbols[randomNr];
        }
    }

    private IEnumerator Order()
    {
        while(true)
        {
            int randomNr = Random.Range(0, ReactionNames.Length);

            rndNr = randomNr;
            timePerOrder = originalTime + 1;
            countdown = originalTime + 1;

            screenReactionName.text = ReactionNames[randomNr];
            screenReactionSymbol.text = ReactionSymbols[randomNr];

            yield return new WaitForSeconds(timePerOrder);

            if (orderComplete == false)
                money -= penaltyMoney;
        }
    }

    private IEnumerator CountDown()
    {
        while (true)
        {
            countdown -= 1;

            yield return new WaitForSeconds(1);
        }     
    }
}
