using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : Unidad {

    public override void Initialize()
    {
        SetMaxHealth(2000);
        SetMaxMana(0);
        SetHealth(2000);
        SetMana(0);
        SetHealthRegen(0);
        SetManaRegen(0);
        SetAtackDamage(45);
        SetAtackSpeed(5);
		SetAbilityPower(45);
        SetArmour(0);
        SetMagicArmour(0);
        SetMovementSpeed(0);
        SetLevel(0);
        SetGold(600);
        SetExperience(500);
        SetMaxExperience(0);
        SetDeaths(0);
        SetAssassinations(0);
    }
}
