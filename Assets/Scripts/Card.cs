using UnityEngine;

public abstract class Card
{
    public string Name { get; private set; }
    public enum CardType { Offense, Support, Modifier }
    public CardType Type { get; protected set; }
    public bool isPlayerOwned;

    public Card(string name, CardType type, bool playerOwned)
    {
        Name = name;
        Type = type;
        isPlayerOwned = playerOwned;
    }
}

public class OffenseCard : Card
{
    public int Damage { get; private set; }
    public int Health { get; private set; }

    public OffenseCard(string name, int damage, int health) 
        : base(name, CardType.Offense)
    {
        Damage = damage;
        Health = health;
    }
}

public class SupportCard : Card
{
    public string Behavior { get; private set; }

    public SupportCard(string name, string behavior) 
        : base(name, CardType.Support)
    {
        Behavior = behavior;
    }
}

public class ModifierCard : Card
{
    public string Behavior { get; private set; }

    public ModifierCard(string name, string behavior) 
        : base(name, CardType.Modifier)
    {
        Behavior = behavior;
    }
}
