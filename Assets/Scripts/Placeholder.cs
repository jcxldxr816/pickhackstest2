using UnityEngine;

public class Placeholder : MonoBehaviour
{
    public bool isOccupied = false; // Indicates whether this placeholder is occupied by a card
    public Phases currentCard = null; // The Phases object currently occupying this Placeholder

    private void OnMouseDown()
    {
        // Ensure a card is selected and this Placeholder is not occupied
        if (Phases.selectedCard != null && this.isOccupied == false)
        {
            // Get the currently selected card
            GameObject card = Phases.selectedCard;
            Phases cardScript = card.GetComponent<Phases>();

            // If the card is already on another Placeholder, clear the original Placeholder's occupancy
            if (cardScript.currentPlaceholder != null)
            {
                Placeholder oldPlaceholder = cardScript.currentPlaceholder;
                oldPlaceholder.isOccupied = false;
                oldPlaceholder.currentCard = null; // Clear the original placeholder's Phases
                Debug.Log($"Placeholder {oldPlaceholder.gameObject.name} is now unoccupied.");
            }

            // Update the card's position to the current Placeholder
            card.transform.position =new Vector3( transform.position.x,0.2f, transform.position.z); // Move the card to the position of this Placeholder
            
            cardScript.currentPlaceholder = this; // Update the Placeholder reference in the card

            isOccupied = true; // Mark the current Placeholder as occupied
            currentCard = cardScript; // Set the current Placeholder's Phases
            currentCard.inField = true; // Mark the card as being on the field
            // Clear the selected card state
            Phases.selectedCard = null;

            Debug.Log($"Phases moved to Placeholder {gameObject.name}.");
        }
        else
        {
            Debug.Log("Cannot place card: Placeholder is occupied or no card is selected.");
        }
    }

    private void OnMouseEnter()
    {
        // Highlight the placeholder: show green if it can accept a card
        if (Phases.selectedCard != null && !isOccupied)
        {
            GetComponent<Renderer>().material.color = Color.green; // Highlight available placeholder
        }
    }

    private void OnMouseExit()
    {
        // Remove highlight
        GetComponent<Renderer>().material.color = Color.white; // Reset the color
    }
}