using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Champion : MonoBehaviour {
    protected int level;
    protected int abilityPower;
    protected int armour;
    protected int magicArmour;
    protected int atackDamage;
    protected int atackSpeed;
    protected int currentArmor;
    protected int experience;
    protected int gold;
    protected int health;
    protected int healthRegen;
    protected int mana;
    protected int manaRegen;
    protected int maxExperience;
    protected int maxHealth;
    protected int maxMana;
    protected int movementSpeed;

    public int MagicArmour
    {
        get
        {
            return magicArmour;
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

    public int CurrentArmor
    {
        get
        {
            return currentArmor;
        }

        
    }

    public int Experience
    {
        get
        {
            return experience;
        }

       
    }

    public int Gold
    {
        get
        {
            return gold;
        }

       
    }

    public int Health
    {
        get
        {
            return health;
        }

     
    }

    public int HealthRegen
    {
        get
        {
            return healthRegen;
        }

    }

    public int Mana
    {
        get
        {
            return mana;
        }
  
    }

    public int ManaRegen
    {
        get
        {
            return manaRegen;
        }

       
    }

    public int MaxExperience
    {
        get
        {
            return maxExperience;
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

    public int Level
    {
        get
        {
            return level;
        }

    }
}
