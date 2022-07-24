using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dokebimusa : Unidad {

    public override void Initialize()
    {
        SetMaxHealth(1200);
        SetMaxMana(300);
        SetHealth(1200);
        SetMana(300);
        SetHealthRegen(3);
        SetManaRegen(2);
        SetAtackDamage(30);
        SetAtackSpeed(10);
        SetAbilityPower(20);
        SetArmour(15);
        SetMagicArmour(19);
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
