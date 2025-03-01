using UnityEngine;

public abstract class Card
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public enum CardType { Offense, Support, Modifier }
    public CardType Type { get; protected set; }
    public bool isPlayerOwned {get; private set;}

    public Card(string name, string description, CardType type, bool playerOwned)
    {
        Name = name;
        Description = description;
        Type = type;
        isPlayerOwned = playerOwned;
    }
}

public class OffenseCard : Card
{
    public int Damage { get; private set; }
    public int Health { get; private set; }

    public OffenseCard(string name, string description, int damage, int health, bool playerOwned) 
        : base(name, description, CardType.Offense, playerOwned)
    {
        Damage = damage;
        Health = health;
    }
}

public class SupportCard : Card
{
    public int Health { get; private set; }
    public SupportCard(string name, string description, int health, bool playerOwned) 
        : base(name, description, CardType.Support, playerOwned)
    {
        Health = health;
    }
}

public class ModifierCard : Card
{

    public ModifierCard(string name, string description, bool playerOwned) 
        : base(name, description, CardType.Modifier, playerOwned)
    {
    }
}
public class Card : MonoBehaviour
{
    public static GameObject selectedCard = null; // The currently selected card
    public Placeholder currentPlaceholder = null; // The placeholder object occupied by this card
    public bool activated = false;
    public bool inField = false;

    public int state = 0; // 0: in shop, 1: off battle, 2: in battle
    public int HP;
    public int cost;

    private void OnEnable()
    {
        // Subscribe to events from GameManager
        GameManager.OnRoundStart += OnRoundStart;
        GameManager.OnRoundBattle += OnRoundBattle;
    }

    private void OnDisable()
    {
        // Unsubscribe from events to prevent memory leaks or invalid calls
        GameManager.OnRoundStart -= OnRoundStart;
        GameManager.OnRoundBattle += OnRoundBattle;
    }

    private void OnMouseDown()
    {
        // If there is no selected card, select this card
        if (selectedCard == null)
        {
            selectedCard = this.gameObject;
            Debug.Log($"Card {gameObject.name} selected.");
        }
        // If this card is already selected, deselect it
        else if (selectedCard == this.gameObject)
        {
            selectedCard = null;
            Debug.Log($"Card {gameObject.name} deselected.");
        }
    }

    private void Update()
    {
        // Update the color of the card based on its state
        if (selectedCard == this.gameObject)
        {
            GetComponent<Renderer>().material.color = Color.green; // Highlight the selected card
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white; // Reset the color
        }
    }

    // Respond to GameManager's OnRoundStart event
    private void OnRoundStart()
    {
        if (!inField)
        {
            Debug.Log($"Card {gameObject.name} is still in shop during OnRoundStart.");
            // Perform any logic, such as resetting state or preparing for refresh
            state = 1; // Still in the shop
        }
        else
        {
            Debug.Log($"Card {gameObject.name} is in field during OnRoundStart.");
            // Perform any logic, such as making it unsellable or preparing for battle
        }
    }


    private void OnRoundBattle()
    {
        state = 2; // Set state to "in battle"
    }
}
