using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public List<GameObject> availableCards;      // Store all available card prefabs
    public Transform shopBar;                    // Parent transform for cards displayed in the shop (shop area)
    public int shopSlots = 5;                    // Number of slots available in the shop for display

    private List<GameObject> currentShopCards = new List<GameObject>(); // Current items generated in the shop

    private void OnEnable()
    {
        // Subscribe to GameManager events
        GameManager.OnRoundStart += OnRoundStart;
        GameManager.OnRoundBattle += OnRoundBattle;
    }

    private void OnDisable()
    {
        // Unsubscribe from GameManager events
        GameManager.OnRoundStart -= OnRoundStart;
        GameManager.OnRoundBattle -= OnRoundBattle;
    }

    public void Start()
    {
        RefreshShop(1); // Initialize the shop
    }

    // Randomly generate cards or troops for the shop
    public void RefreshShop(int phase)
    {
        // Clear existing shop content, skipping those that are already "inField"
        for (int i = currentShopCards.Count - 1; i >= 0; i--)
        {
            GameObject card = currentShopCards[i];
            Phases cardScript = card.GetComponent<Phases>();

            if (cardScript != null && !cardScript.inField) // Check the inField status
            {
                Destroy(card); // Destroy cards that have not been placed
                currentShopCards.RemoveAt(i); // Remove from the list
            }
        }
        currentShopCards.Clear();

        
        for (int i = 0; i < shopSlots; i++)
        {
            // Randomly select a card from the list of available cards
            int randomIndex = Random.Range(0, availableCards.Count);
            GameObject cardPrefab = availableCards[randomIndex];

            // Instantiate the card under the shopBar (as its child)
            GameObject cardInstance = Instantiate(cardPrefab, shopBar);

            // Set the position of the instance (adjust layout as needed)
            cardInstance.transform.localPosition = new Vector3(i * 1.0f-4, 0.5f, 0f);
            cardInstance.transform.rotation = this.transform.rotation;

            // Add to the current shop list
            currentShopCards.Add(cardInstance);
        }
        
        
        
    }

    // Respond to the `OnRoundStart` event
    public void OnRoundStart()
    {
        Debug.Log("Shop Refreshing for Round Start");
        RefreshShop(1); // Refresh shop, showing cards
    }

    // Respond to the `OnRoundShop2` event
    public void OnRoundBattle()
    {
       
    }
}