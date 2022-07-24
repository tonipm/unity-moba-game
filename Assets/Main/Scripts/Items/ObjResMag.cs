using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjResMag : Item {

    public Sprite imatge;
    public override void Initialize()
    {
        this.id = 5;
        this.name = "Collaret de Dientehueso";
        this.description = "Aquest collaret et puja la resistència màgica 4 punts.";
        this.magicArmour = 4;
        this.price = 400;
        this.icona = imatge;
    }

    public override void Comprar(Unidad champion)
    {
        champion.SetMagicArmour(champion.GetMagicArmour() + this.magicArmour);
        champion.SetGold(champion.GetGold() - this.price);
    }
}
