using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocioVida : Item {

    public Sprite imatge;
    public override void Initialize()
    {
        this.id = 10;
        this.name = "Poció de vida";
        this.description = "Poció que et recuperar 250 punts de vida";
        this.health = 250;
        this.price = 100;
        this.icona = imatge;
    }

    public override void Comprar(Unidad champion)
    {
        champion.GetCombatSystem().ModifyHealth(this.health, false, -1, this.name);
        champion.SetGold(champion.GetGold() - this.price);
    }
}
