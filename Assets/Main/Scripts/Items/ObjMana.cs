using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMana : Item {

    public Sprite imatge;
    public override void Initialize()
    {
        this.id = 3;
        this.name = "Hope";
        this.description = "Aquesta joia et puja el mana màxim en 500 punts i 10 punts de regeneració de mana ";
        this.maxMana = 500;
        this.manaRegen = 10;
        this.price = 400;
        this.icona = imatge;
    }

    public override void Comprar(Unidad champion)
    {
        champion.SetMaxMana(champion.GetMaxMana() + this.maxMana);
        champion.SetManaRegen(champion.GetManaRegen() + this.manaRegen);
        champion.SetGold(champion.GetGold() - this.price);
        champion.GetCombatSystem().ModifyMana(0);
    }
}
