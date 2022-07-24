using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samuzai : Unidad
{
    public override void Initialize()
    {
        SetMaxHealth(1000);
        SetMaxMana(400);
        SetHealth(1000);
        SetMana(400);
        SetHealthRegen(2);
        SetManaRegen(2);
        SetAtackDamage(40);
        SetAtackSpeed(20);
        SetAbilityPower(20);
        SetArmour(12);
        SetMagicArmour(18);
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
