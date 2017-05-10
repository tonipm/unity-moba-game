using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocioVida : Item {
    public Sprite imatge;
    // Use this for initialization
    void Start () {
        this.name = "Poció de vida";
        this.description = "Poció per recuperar la vida";
        this.maxHealth = 250;
        this.price = 100;
        this.icona = imatge;
	}
}
