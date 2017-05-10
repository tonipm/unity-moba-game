using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offoff : Champion {

    // Use this for initialization
    void Start()
    {
        this.health = 1200;
        this.mana = 300;
        this.healthRegen = 3;
        this.manaRegen = 2;
        this.atackDamage = 30;
        this.atackSpeed = 10;
        this.abilityPower = 20;
        this.armour = 300;
        this.magicArmour = 270;
        this.movementSpeed = 10;
        this.level = 1;
        this.gold = 500;
        this.experience = 0;
        this.maxExperience = 500;
    }
}
