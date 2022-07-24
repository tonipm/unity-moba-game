using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPodAbi : Item {

    public Sprite imatge;
    public override void Initialize()
    {
        this.id = 4;
        this.name = "Llibre perdut";
        this.description = "Aquest llibre et puja 10 punts en poder l'abilitat.";
        this.abilityPower = 10;
        this.price = 350;
        this.icona = imatge;
    }

    public override void Comprar(Unidad champion)
    {
        champion.SetAbilityPower(champion.GetAbilityPower() + this.abilityPower);
        champion.SetGold(champion.GetGold() - this.price);
    }
}
