using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public int gold;   // Player's gold
    public int wave;   // Current wave number
    public int phase;  // Current phase: 0-Hero, 1-Shop1, 2-Shop2, 3-Battle

    public void ReSet()
    {
        phase = 0;
        gold = 0;
        wave = 0;
    }

    // Define events
    public delegate void RoundHandler();
    public static event RoundHandler OnRoundStart;
    public static event RoundHandler OnRoundBattle;
    public void Start()
    {
        ReSet();
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
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            RoundStart(); // Trigger RoundStart phase event
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            RoundBattle(); // Trigger RoundBattle phase event
        }
    }
}