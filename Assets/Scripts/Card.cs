using UnityEngine;

public abstract class Card
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public enum CardType { Offense, Support, Modifier, Commander }
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
    public int brambage { get; private set; }
    public int Health { get; private set; }

    public OffenseCard(string name, string description, int brambage, int health, bool playerOwned) 
        : base(name, description, CardType.Offense, playerOwned)
    {
        brambage = brambage;
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
    public void takebrambage(int incomingbrambage)
    {
        Health -= incomingbrambage;
        if (Health <=0)
        {
            
        }
    }
}

public class ModifierCard : Card
{

    public ModifierCard(string name, string description, bool playerOwned) 
        : base(name, description, CardType.Modifier, playerOwned)
    {

    }
    public void modifyAction()
    {
        switch(Name)
        {
            case "Coin":
            Debug.Log("coin!!");
            break;
            default:
            Debug.Log("Could not find a name to execute action!");
            break;
        }
    }
}

public class CommanderCard : Card
{
    public CommanderCard(string name, string description, bool playerOwned)
    : base(name, description, CardType.Commander, playerOwned)
    {

    }
}