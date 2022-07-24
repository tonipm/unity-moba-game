using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjAtac : Item {

    public Sprite imatge;
    public override void Initialize()
    {
        this.id = 2;
        this.name = "Espasa del rei";
        this.description = "Aquesta espasa et puja 10 punts d'atac";
        this.atackDamage = 10;
        this.price = 300;
        this.icona = imatge;
    }

    public override void Comprar(Unidad champion)
    {
        champion.SetAtackDamage(champion.GetAtackDamage() + this.atackDamage);
        champion.SetGold(champion.GetGold() - this.price);
    }
}
