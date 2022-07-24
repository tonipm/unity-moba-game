using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocioMana : Item {

    public Sprite imatge;
    public override void Initialize()
    {
        this.id = 9;
        this.name = "Poció de mana";
        this.description = "Poció que et recupera 250 punts de mana";
        this.mana = 250;
        this.price = 100;
        this.icona = imatge;
    }

    public override void Comprar(Unidad champion)
    {
        champion.GetCombatSystem().ModifyMana(this.mana);
        champion.SetGold(champion.GetGold() - this.price);
    }
}
