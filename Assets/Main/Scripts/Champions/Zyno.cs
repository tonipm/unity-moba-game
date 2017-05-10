using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zyno : Champion {

	// Use this for initialization
	void Start () {
        this.health = 800;
        this.mana = 500;
        this.healthRegen = 2;
        this.manaRegen = 2;
        this.atackDamage = 25;
        this.atackSpeed = 10;
        this.abilityPower = 50;
        this.armour = 100;
        this.magicArmour = 150;
        this.movementSpeed = 10;
        this.level = 1;
        this.gold = 500;
        this.experience = 0;
        this.maxExperience = 500;
    }
}
