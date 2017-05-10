using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocioMana : Item {
    public Sprite imatge;
    // Use this for initialization
    void Start()
    {
        this.name = "Poció de mana";
        this.description = "Poció per recuperar el mana";
        this.maxHealth = 250;
        this.price = 100;
        this.icona = imatge;
    }
}
