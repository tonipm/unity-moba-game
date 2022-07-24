using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zyno : Unidad {
    
    public override void Initialize()
    {
        SetMaxHealth(600);
        SetMaxMana(500);
        SetHealth(600);
        SetMana(500);
        SetHealthRegen(2);
        SetManaRegen(2);
        SetAtackDamage(25);
        SetAtackSpeed(10);
        SetAbilityPower(50);
        SetArmour(10);
        SetMagicArmour(15);
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
