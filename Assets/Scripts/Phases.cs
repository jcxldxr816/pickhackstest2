using UnityEngine;

public class Phases : MonoBehaviour
{
    public static Phases[,] Card2DArray = new Phases[4, 5];

    public static GameObject selectedCard = null; // The currently selected card
    public Placeholder currentPlaceholder = null; // The placeholder object occupied by this card
    public bool activated = false;
    public bool inField = false;

    public string cardName = "";
    public int state = 0; // 0: in shop, 1: off battle, 2: in battle
    public int HP = 0;
    public int Damage = 0;
    public int cardType;//defence 0,attack1
    public int cost;

    private void OnEnable()
    {
        // Subscribe to events from GameManager
        GameManager.OnRoundPlan += OnRoundPlan;
        GameManager.OnRoundBattle += OnRoundBattle;
    }

    private void OnDisable()
    {
        // Unsubscribe from events to prevent memory leaks or invalid calls
        GameManager.OnRoundPlan -= OnRoundPlan;
        GameManager.OnRoundBattle -= OnRoundBattle;
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

    // Respond to GameManager's OnRoundPlan event
    private void OnRoundPlan()
    {
        if (!inField)
        {
            Debug.Log($"Card {gameObject.name} is still in shop during OnRoundPlan.");
            // Perform any logic, such as resetting state or preparing for refresh
            state = 1; // Still in the shop
        }
        else
        {
            Debug.Log($"Card {gameObject.name} is in field during OnRoundPlan.");
            // Perform any logic, such as making it unsellable or preparing for battle
        }
    }


    private void OnRoundBattle()
    {
        state = 2; // Set state to "in battle"

        int currentRow = 1; //Start on player's offense card row
        int oppositeOffenseRow = 2;
        int oppositeSupportRow = 3;
        bool playerTurn = false; //false for player's turn, true for opponent's turn
        CalculateDamage(currentRow, oppositeOffenseRow, oppositeSupportRow, playerTurn);
        currentRow = 2;
        oppositeOffenseRow = 1;
        oppositeSupportRow = 0;
        playerTurn = true;
        CalculateDamage(currentRow, oppositeOffenseRow, oppositeSupportRow, playerTurn);
        GameManager.Instance.BattleEnded();
    }
    public void CalculateDamage(int currentRow, int oppositeOffenseRow, int oppositeSupportRow, bool playerTurn)
        {
        for (int col = 0; col <=4; col++)
        {
            if (Card2DArray[currentRow,col] != null)
            {
            int damageDealt = Card2DArray[currentRow,col].Damage;
            
            if (Card2DArray[oppositeOffenseRow,col] != null) //If opponent has offense card in this lane
            {
                Card2DArray[oppositeOffenseRow,col].HP -= damageDealt; //Damage opponent's offense card
                if (Card2DArray[oppositeOffenseRow,col].HP <= 0) //If opponent's offense card dies
                {
                    int supportOverflowDamage = Card2DArray[oppositeOffenseRow,col].HP;
                    RemoveCardFromArray(oppositeOffenseRow, col); //Destroy opponent's offense card
                    if (Card2DArray[oppositeSupportRow,col] != null) //If opponent has support card
                    {
                    Card2DArray[oppositeSupportRow,col].HP += supportOverflowDamage; //Overflow damage to suport card
                    if (Card2DArray[oppositeSupportRow,col].HP <= 0) //If opponent's support card dies
                    {
                        int opponentOverflowDamage = Card2DArray[oppositeSupportRow,col].HP;
                        RemoveCardFromArray(oppositeSupportRow, col); //Destroy opponent's support card
                        GameManager.Instance.TakeDamage(opponentOverflowDamage, playerTurn); //Overflow damage to player
                    }
                    }
                    else
                    {
                        GameManager.Instance.TakeDamage(supportOverflowDamage, playerTurn);
                    }
                }
            }
            else
            {
                GameManager.Instance.TakeDamage(damageDealt, playerTurn); //Attack directly if no card in lane
            }
            }
        }
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

    public void supportEffect()
    {
        if (cardType == 0)
        {
            switch (cardName)
            {
                case "GoldBoost":
                    GameManager.increaseGold(5);
                    break;
                case "DamageBoost":
                    GetCardFromArray(currentPlaceholder.row +1, currentPlaceholder.line).Damage += 5;
                    break;
                case "HealthBoost":
                    GetCardFromArray(currentPlaceholder.row + 1, currentPlaceholder.line).HP += 3;
                    break;
            }
        }
        
    }
}
