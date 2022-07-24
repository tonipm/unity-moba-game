using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Unidad {

    public override void Initialize()
    {
        SetMaxHealth(3500);
        SetMaxMana(0);
        SetHealth(3500);
        SetMana(0);
        SetHealthRegen(0);
        SetManaRegen(0);
        SetAtackDamage(60);
        SetAtackSpeed(5);
        SetAbilityPower(60);
        SetArmour(0);
        SetMagicArmour(0);
        SetMovementSpeed(0);
        SetLevel(0);
        SetGold(600);
        SetExperience(500);
        SetMaxExperience(0);
        SetDeaths(0);
        SetAssassinations(0);
        SetInventory(0);
    }
}
