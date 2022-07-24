using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjVida : Item {

    public Sprite imatge;
    public override void Initialize()
    {
        this.id = 8;
        this.name = "Poma";
        this.description = "Aquesta et puja la vida màxima en 500 i la regeneració en 10.";
        this.maxHealth = 500;
        this.healthRegen = 10;
        this.price = 400;
        this.icona = imatge;
    }

    public override void Comprar(Unidad champion)
    {
        champion.SetMaxHealth(champion.GetMaxHealth() + this.maxHealth);
        champion.SetHealthRegen(champion.GetHealthRegen() + this.healthRegen);
        champion.SetGold(champion.GetGold() - this.price);
        champion.GetCombatSystem().ModifyHealth(0, false, -1, this.name);
    }
}
