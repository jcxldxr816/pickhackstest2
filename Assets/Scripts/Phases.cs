using UnityEngine;

public class Phases : MonoBehaviour
{
    public static Phases[,] Card2DArray = new Phases[5, 4];

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





    public static void AddCardToArray(Phases card, int row, int col)
    {
        if (row >= 0 && row < Card2DArray.GetLength(0) && col >= 0 && col < Card2DArray.GetLength(1))
        {
            if (Card2DArray[row, col] == null)
            {
                Card2DArray[row, col] = card;
                Debug.Log($"Card {card.gameObject.name} added to Card2DArray[{row}, {col}]");
            }
            else
            {
                Debug.LogWarning($"Card2DArray[{row}, {col}] is already occupied.");
            }
        }
        else
        {
            Debug.LogError($"Invalid index: Card2DArray[{row}, {col}] is out of bounds.");
        }
    }
    public static void RemoveCardFromArray(int row, int col)
    {
        if (row >= 0 && row < Card2DArray.GetLength(0) && col >= 0 && col < Card2DArray.GetLength(1))
        {
            if (Card2DArray[row, col] != null)
            {
                Debug.Log($"Removing Card {Card2DArray[row, col].gameObject.name} from Card2DArray[{row}, {col}]");
                Card2DArray[row, col] = null;
            }
            else
            {
                Debug.LogWarning($"Card2DArray[{row}, {col}] is already empty.");
            }
        }
        else
        {
            Debug.LogError($"Invalid index: Card2DArray[{row}, {col}] is out of bounds.");
        }
    }

    public static Phases GetCardFromArray(int row, int col)
    {
        if (row >= 0 && row < Card2DArray.GetLength(0) && col >= 0 && col < Card2DArray.GetLength(1))
        {
            return Card2DArray[row, col];
        }
        else
        {
            Debug.LogError($"Invalid index: Card2DArray[{row}, {col}] is out of bounds.");
            return null;
        }
    }
}
