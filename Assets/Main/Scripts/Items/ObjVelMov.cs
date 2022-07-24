using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjVelMov : Item
{
    public Sprite imatge;
    public override void Initialize()
    {
        this.id = 7;
        this.name = "Botas velocitat de moviment";
        this.description = "Aquestes botes et pujen 3 punts la velocitat de moviment";
        this.movementSpeed = 3;
        this.price = 300;
        this.icona = imatge;
    }

    public override void Comprar(Unidad champion)
    {
        champion.SetMovementSpeed(champion.GetMovementSpeed() + this.movementSpeed);
        champion.SetGold(champion.GetGold() - this.price);
    }
}
