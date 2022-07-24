using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjArmdura : Item {

    public Sprite imatge;

    public override void Initialize()
    {
        this.id = 1;
        this.name = "L'armadura del no-mort";
        this.description = "Aquest objecte et puja l'armadura 4 punts";
        this.armour = 4;
        this.price = 400;
        this.icona = imatge;
    }

    public override void Comprar(Unidad champion)
    {
        champion.SetArmour(champion.GetArmour() + this.armour);
        champion.SetGold(champion.GetGold() - this.price);
    }
}
