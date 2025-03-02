using UnityEngine;
using System.Collections.Generic;
//using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public List<GameObject> allCards;
    
    public static int gold;   // Player's gold
    public static int round;   // Current round number
    public static int stage;  // Current phase: 1-Sleep, 2-Plan, 3-Battle
    public static int playerHealth;
    public static int opponentHealth;
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
        stage = 0;
        gold = 0;
        round = 0;
        playerHealth = 10;
        opponentHealth = 10;
    }

    // Define events
    public delegate void RoundHandler();
    public static event RoundHandler OnRoundSleep;
    public static event RoundHandler OnRoundPlan;
    public static event RoundHandler OnRoundPlanEnd;
    public static event RoundHandler OnRoundBattle;
    public static event RoundHandler OnRoundBattleEnd;
    public void Start()
    {
        ReSet();
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
        stage = 1; // Set stage to Sleep
        OnRoundSleep?.Invoke(); // Trigger OnRoundSleep event
    }
    public void PlanStage()
    {
        Debug.Log("Round Start, Entering Plan Phase");
        stage = 2; // Set stage to Plan
        OnRoundPlan?.Invoke();
        //Do stuff 
        OnRoundPlanEnd?.Invoke();
    }
    public void BattleStage()
    {
        Debug.Log("Round Start, Entering Battle Phase");
        stage = 3; // Set stage to Battle
        OnRoundBattle?.Invoke(); // Trigger OnRoundBattle event
        //Do battle stuff
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
    }

    public static void increaseGold(int amount)
    {
        gold += amount;
    }
    
    //TODO: decrease gold
    
    //TODO: make increase/decrease health functions if they don't exist alr...

    public void opponentTurn()
    {
        List<GameObject> selectedCards = new List<GameObject>();
        
        for (int i = 0; i < 5; i++) //pick 5 random cards (duplicates allowed)
        {
            int randomIndex = Random.Range(0, allCards.Count);
            GameObject chosenCard = allCards[randomIndex];
            Instantiate(chosenCard);
            chosenCard.SetActive(false); //making sure selected card will not be visible
            selectedCards.Add(chosenCard);
        }

        foreach (var card in selectedCards)
        {
            if (card.GetComponent<Phases>().cardType == 0) //support
            {
                int lane = Random.Range(0, 5);
                if (Phases.GetCardFromArray(3, lane) == null)
                {
                    Phases.AddCardToArray(card.GetComponent<Phases>(), 3, lane);
                    card.GetComponent<Phases>().transform.position = card.GetComponent<Phases>().currentPlaceholder.transform.position; //issue
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
        //TODO call onSleepEnd
    }
    
}