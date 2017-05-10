using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    protected string name;
    protected string description;
    protected int price;
    protected Sprite icona;
    //Stats
    protected int abilityPower;
    protected int armour;
    protected int magicArmour;
    protected int atackDamage;
    protected int atackSpeed;
    protected int healthRegen;
    protected int manaRegen;
    protected int maxHealth;
    protected int maxMana;
    protected int movementSpeed;

    public string Name
    {
        get
        {
            return name;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }
    }

    public Sprite Icona
    {
        get
        {
            return icona;
        }
    }

    public int AbilityPower
    {
        get
        {
            return abilityPower;
        }

    }

    public int Armour
    {
        get
        {
            return armour;
        }
    }

    public int MagicArmour
    {
        get
        {
            return magicArmour;
        }
    }

    public int AtackDamage
    {
        get
        {
            return atackDamage;
        }
    }

    public int AtackSpeed
    {
        get
        {
            return atackSpeed;
        }

    }

    public int HealthRegen
    {
        get
        {
            return healthRegen;
        }
    }

    public int ManaRegen
    {
        get
        {
            return manaRegen;
        }
    }

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    public int MaxMana
    {
        get
        {
            return maxMana;
        }
    }

    public int MovementSpeed
    {
        get
        {
            return movementSpeed;
        }
    }
}
