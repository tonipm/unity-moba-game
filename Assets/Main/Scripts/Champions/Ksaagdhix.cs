using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ksaagdhix : Champion {

    // Use this for initialization
    void Start()
    {
        this.health = 1000;
        this.mana = 400;
        this.healthRegen = 2;
        this.manaRegen = 2;
        this.atackDamage = 40;
        this.atackSpeed = 20;
        this.abilityPower = 20;
        this.armour = 200;
        this.magicArmour = 180;
        this.movementSpeed = 10;
        this.level = 1;
        this.gold = 500;
        this.experience = 0;
        this.maxExperience = 500;
    }
}
