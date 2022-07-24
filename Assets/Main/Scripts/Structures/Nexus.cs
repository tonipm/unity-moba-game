using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : Unidad
{
    public override void Initialize()
    {
        SetMaxHealth(3000);
        SetMaxMana(0);
        SetHealth(3000);
        SetMana(0);
        SetHealthRegen(2);
        SetManaRegen(0);
        SetAtackDamage(0);
        SetAtackSpeed(0);
        SetAbilityPower(0);
        SetArmour(0);
        SetMagicArmour(0);
        SetMovementSpeed(0);
        SetLevel(0);
        SetGold(0);
        SetExperience(0);
        SetMaxExperience(0);
        SetDeaths(0);
        SetAssassinations(0);
    }
}
