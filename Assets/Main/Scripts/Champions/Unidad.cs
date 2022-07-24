using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unidad : MonoBehaviour {

    public int level;
    public int abilityPower;
    public int armour;
    public int magicArmour;
    public int atackDamage;
    public int atackSpeed;
    public int experience;
    public int gold;
    public int health;
    public int healthRegen;
    public int mana;
    public int manaRegen;
    public int maxExperience;
    public int maxHealth;
    public int maxMana;
    public int movementSpeed;
    public int deaths;
    public int assassinations;
    public int minions;
    public int inventory;
    public Sprite image;

    public abstract void Initialize();

    public CombatSystem GetCombatSystem()
    {
        return this.gameObject.GetComponent<CombatSystem>();
    }

    public Sprite GetImage()
    {
        return image;
    }
    public int GetInventory()
    {
        return inventory;
    }

    public void SetInventory(int inventory)
    {
        this.inventory = inventory;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public int GetAbilityPower()
    {
        return abilityPower;
    }

    public void SetAbilityPower(int abilityPower)
    {
        this.abilityPower = abilityPower;
    }

    public int GetArmour()
    {
        return armour;
    }

    public void SetArmour(int armour)
    {
        this.armour = armour;
    }

    public int GetMagicArmour()
    {
        return magicArmour;
    }

    public void SetMagicArmour(int magicArmour)
    {
        this.magicArmour = magicArmour;
    }

    public int GetAtackDamage()
    {
        return atackDamage;
    }

    public void SetAtackDamage(int atackDamage)
    {
        this.atackDamage = atackDamage;
    }

    public int GetAtackSpeed()
    {
        return atackSpeed;
    }

    public void SetAtackSpeed(int atackSpeed)
    {
        this.atackSpeed = atackSpeed;
    }

    public int GetExperience()
    {
        return experience;
    }

    public void SetExperience(int experience)
    {
        this.experience = experience;
    }

    public int GetGold()
    {
        return gold;
    }

    public void SetGold(int gold)
    {
        this.gold = gold;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public int GetHealthRegen()
    {
        return healthRegen;
    }

    public void SetHealthRegen(int healthRegen)
    {
        this.healthRegen = healthRegen;
    }

    public int GetMana()
    {
        return mana;
    }

    public void SetMana(int mana)
    {
        this.mana = mana;
    }

    public int GetManaRegen()
    {
        return manaRegen;
    }

    public void SetManaRegen(int manaRegen)
    {
        this.manaRegen = manaRegen;
    }

    public int GetMaxExperience()
    {
        return maxExperience;
    }

    public void SetMaxExperience(int maxExperience)
    {
        this.maxExperience = maxExperience;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public int GetMaxMana()
    {
        return maxMana;
    }

    public void SetMaxMana(int maxMana)
    {
        this.maxMana = maxMana;
    }

    public int GetMovementSpeed()
    {
        return movementSpeed;
    }

    public void SetMovementSpeed(int movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }

    public int GetDeaths()
    {
        return deaths;
    }

    public void SetDeaths(int deaths)
    {
        this.deaths = deaths;
    }

    public int GetAssassinations()
    {
        return assassinations;
    }

    public void SetAssassinations(int assassinations)
    {
        this.assassinations = assassinations;
    }

    public int GetMinions()
    {
        return minions;
    }

    public void SetMinions(int minions)
    {
        this.minions = minions;
    }
}
