using UnityEngine;
//using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
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
    }

    public static void increaseGold(int amount)
    {
        gold += amount;
    }
}