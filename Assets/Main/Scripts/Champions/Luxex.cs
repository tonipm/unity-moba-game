using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luxex : Unidad {

    public override void Initialize()
    {
        SetMaxHealth(700);
        SetMaxMana(700);
        SetHealth(700);
        SetMana(700);
        SetHealthRegen(4);
        SetManaRegen(3);
        SetAtackDamage(20);
        SetAtackSpeed(10);
        SetAbilityPower(60);
        SetArmour(10);
        SetMagicArmour(16);
        SetMovementSpeed(10);
        SetLevel(1);
        SetGold(500);
        SetExperience(0);
        SetMaxExperience(500);
        SetDeaths(0);
        SetAssassinations(0);
        SetInventory(4);
    }
}
