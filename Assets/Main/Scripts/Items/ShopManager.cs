using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    public Image imatgeDescripcio;

    public void ShowDescription(Item item)
    {
        imatgeDescripcio.sprite = item.Icona;

    }
}
