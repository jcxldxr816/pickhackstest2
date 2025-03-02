using UnityEngine;
//using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public int gold;   // Player's gold
    public int wave;   // Current wave number
    public int phase;  // Current phase: 0-Hero, 1-Shop1, 2-Shop2, 3-Battle
    public int playerHealth;
    public int opponentHealth;
     private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;  // Assign the instance
            DontDestroyOnLoad(gameObject); // Make it persistent
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManagers
        }
    }

    public void ReSet()
    {
        phase = 0;
        gold = 0;
        wave = 0;
        playerHealth = 10;
        opponentHealth = 10;
    }

    // Define events
    public delegate void RoundHandler();
    public static event RoundHandler OnRoundStart;
    public static event RoundHandler OnRoundBattle;
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
    public void RoundStart()
    {
        Debug.Log("Round Start, Entering Shop1 Phase");
        phase = 1; // Set phase to Shop1
        OnRoundStart?.Invoke(); // Trigger OnRoundStart event
    }

    public void RoundBattle()
    {
        Debug.Log("Round Start, Entering Battle Phase");
        phase = 3; // Set phase to Battle
        OnRoundBattle?.Invoke(); // Trigger OnRoundBattle event
    }

    private void Update()
    {
        // For testing: Trigger phase events using keyboard input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("one pressed");
            
            RoundStart(); // Trigger RoundStart phase event
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RoundBattle(); // Trigger RoundBattle phase event
        }
    }
}