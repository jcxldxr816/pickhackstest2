using UnityEngine;
using System.Collections.Generic;
using TMPro;
//using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public List<GameObject> allCards;
    
    public static int gold;   // Player's gold
    public static int round;   // Current round number
    //0 = SleepStart, 1 = SleepEnd/PlanStart, 2 = PlanInProgress, 3= BattleStart, 4 = BattleEnd, -1 = Main Menu
    public static int state;
    public static int playerHealth;
    public static int opponentHealth;
    public TextMeshPro yourHP;
    public TextMeshPro opponentHP;
    public ShopManager shopManager;
    public TextMeshPro goldText;

     private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; //Instantiate GameManager obj
            DontDestroyOnLoad(gameObject); //Keep it around
        }
        else
        {
            Destroy(gameObject); //Don't allow clones
        }
    }

    public void ReSet()
    {
        state = 0;
        gold = 0;
        round = 0;
        playerHealth = 10;
        opponentHealth = 10;
    }

    // Define events
    public delegate void RoundHandler();
    public static event RoundHandler OnRoundSleep;
    public static event RoundHandler OnRoundSleepEnd;
    public static event RoundHandler OnRoundPlan;
    public static event RoundHandler OnRoundPlanEnd;
    public static event RoundHandler OnRoundBattle;
    public static event RoundHandler OnRoundBattleEnd;
    public void Start()
    {
        ReSet();
        GameObject obj = GameObject.Find("YourHPText");
        yourHP = obj.GetComponent<TextMeshPro>();
        obj = GameObject.Find("OpponentHPText");
        opponentHP = obj.GetComponent<TextMeshPro>();
        obj = GameObject.Find("GoldText");
        goldText = obj.GetComponent<TextMeshPro>();
    }
    public void TakeDamage(int incomingDamage, bool playerTurn)
    {
        if (incomingDamage < 0)
        {
            incomingDamage = -(incomingDamage);
        }
        if (playerTurn == true) //If opponent's turn, deal damage to player
        {
        playerHealth -= incomingDamage;
        if (playerHealth <= 0)
        {
            gameOver();
        }
        else
        {
            opponentHealth -= incomingDamage;
            if(opponentHealth <=0)
            {
                defeatOpponent();
            }
        }
        }

    }
    public void gameOver()
    {
        Debug.Log("Game Over!!");
    }
    public void defeatOpponent()
    {
        Debug.Log("Opponent Killed!");
    }
    public void SleepStage()
    {
        Debug.Log("Entering Sleep Phase");
        state = 0; // Set state to Sleep
        opponentTurn();
        OnRoundSleep?.Invoke(); // Trigger OnRoundSleep event
    }
    public void PlanStage()
    {
        Debug.Log("Round Start, Entering Plan Phase");
        // Set state to Plan
        //Do stuff 
        OnRoundPlanEnd?.Invoke();
    }
    public void BattleStage()
    {
        Debug.Log("Round Start, Entering Battle Phase");
        state = 3; // Set state to Battle
        OnRoundBattle?.Invoke(); // Trigger OnRoundBattle event
        //Do battle stuff
    }
    public void BattleEnded()
    {
        OnRoundBattleEnd?.Invoke();
    }

    private void Update()
    {
        // For testing: Trigger phase events using keyboard input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("one pressed");
            
            SleepStage(); // Trigger RoundStart phase event
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BattleStage(); // Trigger RoundBattle phase event
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            opponentTurn();
        }

        yourHP.text = "Your HP\n" + playerHealth;
        opponentHP.text = "Opponent HP\n" + opponentHealth;
        goldText.text = "Gold\n" + gold;
    }

    public static void increaseGold(int amount)
    {
        gold += amount;
    }

    public void decreaseGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
        }
        else
        {
            gold = 0;
        }
    }

    public void increasePlayerHealth(int amount)
    {
        playerHealth += amount;
    }
    
    //TODO: make increase/decrease health functions if they don't exist alr...

    public void opponentTurn()
    {
        List<GameObject> selectedCards = new List<GameObject>();
        
        for (int i = 0; i < 5; i++) //pick 5 random cards (duplicates allowed)
        {
            int randomIndex = Random.Range(0, allCards.Count);
            GameObject chosenCard = allCards[randomIndex];
            GameObject newCard = Instantiate(chosenCard); //TODO need to specify parent?
            newCard.SetActive(false); //making sure selected card will not be visible
            selectedCards.Add(newCard);
        }

        foreach (var card in selectedCards)
        {
            if (card.GetComponent<Phases>().cardType == 0) //support
            {
                int lane = Random.Range(0, 5);
                if (Phases.GetCardFromArray(3, lane) == null)
                {
                    Phases.AddCardToArray(card.GetComponent<Phases>(), 3, lane);
                    card.GetComponent<Phases>().transform.position = card.GetComponent<Phases>().currentPlaceholder.transform.position; //TODO issue?
                    card.SetActive(true);
                }
            }
            else if (card.GetComponent<Phases>().cardType == 1) //offense
            {
                int lane = Random.Range(0, 5);
                if (Phases.GetCardFromArray(2, lane) == null)
                {
                    Phases.AddCardToArray(card.GetComponent<Phases>(), 2, lane);
                    card.GetComponent<Phases>().transform.position = card.GetComponent<Phases>().currentPlaceholder.transform.position;
                    card.SetActive(true);
                }
            }
        }
        shopManager.RefreshShop(1);
    }
    
}