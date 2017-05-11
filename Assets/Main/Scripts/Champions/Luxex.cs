using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luxex : Champion {
    // Use this for initialization
    void Start()
    {
        this.health = 700;
        this.mana = 700;
        this.healthRegen = 4;
        this.manaRegen = 3;
        this.atackDamage = 20;
        this.atackSpeed = 10;
        this.abilityPower = 60;
        this.armour = 100;
        this.magicArmour = 170;
        this.movementSpeed = 10;
        this.level = 1;
        this.gold = 500;
        this.experience = 0;
        this.maxExperience = 500;
    }
}
