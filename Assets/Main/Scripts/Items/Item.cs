using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item {
    
    protected int id;
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
    protected int health;
    protected int mana;
    protected int movementSpeed;

    public abstract void Initialize();

    public abstract void Comprar(Unidad champion);

    public int Id
    {
        get
        {
            return id;
        }
    }

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
    public int Health
    {
        get
        {
            return health;
        }
    }
    public int Mana
    {
        get
        {
            return mana;
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
