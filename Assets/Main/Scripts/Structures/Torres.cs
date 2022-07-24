using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torres : Unidad {

    public bool isDestroyed = false;

    public override void Initialize()
    {
        SetMaxHealth(400);
        SetMaxMana(0);
        SetHealth(400);
        SetMana(0);
        SetHealthRegen(0);
        SetManaRegen(0);
        SetAtackDamage(80);
        SetAtackSpeed(2);
        SetAbilityPower(0);
        SetArmour(0);
        SetMagicArmour(0);
        SetMovementSpeed(0);
        SetLevel(0);
        SetGold(300);
        SetExperience(500);
        SetMaxExperience(0);
        SetDeaths(0);
        SetAssassinations(0);
    }
    /* Torres vermell:
     *  135.4958, 3.17, 68.40317
     *  108.8704, 3.17, 42.15558
     *  90.43638, 3.17, 13.00199
     *  119.7843, 3.17, 98.6963
     *  86.0896, 3.17, 64.31685
     *  51.18172, 3.17, 28.9625
     * 
     * 
     * Torres blau:
     *  56.89202, 3.17, 138.9425
     *  39.45858, 3.17, 109.1318
     *  12.88105, 3.17, 94.13446
     *  95.17823, 3.17, 123.5501
     *  60.26974, 3.17, 90.90218
     *  26.71557, 3.17, 53.83526
     * */

    private Vector3[] blueTowerPositions =
    {
        new Vector3(63.61f, 0f, 94.27f),
        new Vector3(47.65707f, 0f, 95.44398f),
        new Vector3(33.31f, 0f, 93.59f),
        new Vector3(77.68201f, 0f, 83.9764f),
        new Vector3(46.9f, 0f, 81f),
        new Vector3(18.6f, 0f, 81.8f)
    };

    private Vector3[] redTowerPositions =
    {
        new Vector3(61.3f, 0f, 50.7f),
        new Vector3(47.08815f, 0f, 48.68384f),
        new Vector3(35.7f, 0f, 49.68f),
        new Vector3(77.8f, 0f, 62.9f),
        new Vector3(47.88f, 0, 64.34f),
        new Vector3(21.18f, 0f, 61.44f)
    };

    public Vector3[] GetBlueTowerPositions()
    {
        return blueTowerPositions;
    }

    public Vector3[] GetRedTowerPositions()
    {
        return redTowerPositions;
    }
}
