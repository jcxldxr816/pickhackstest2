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