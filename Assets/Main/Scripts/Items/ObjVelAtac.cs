using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjVelAtac : Item {

    public Sprite imatge;
    public override void Initialize()
    {
        this.id = 6;
        this.name = "La destral maligna";
        this.description = "Aquesta destral et puja la velocitat d'atac en 25 punts.";
        this.atackSpeed = 25;
        this.price = 250;
        this.icona = imatge;
    }

    public override void Comprar(Unidad champion)
    {
        champion.SetAtackSpeed(champion.GetAtackSpeed() + this.atackSpeed);
        champion.SetGold(champion.GetGold() - this.price);
    }
}
