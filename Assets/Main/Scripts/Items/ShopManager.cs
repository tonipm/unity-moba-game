using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Window {

    public Image imatgeDescripcio;
    public Text nom, descripcio, preu;
    public Button comprar;
    public Image slot1, slot2, slot3, slot4;
    public GameObject zonaDescripcio;
    private ItemMono itemSeleccionat;
    private PhotonView photonView;
    
    NetManager netManager;

    private void Start()
    {
        zonaDescripcio.SetActive(false);
    }
    public void ShowDescription(ItemMono item)
    {
        zonaDescripcio.SetActive(true);
        netManager = GameObject.FindGameObjectWithTag("NetManager").GetComponent<NetManager>();
        imatgeDescripcio.sprite = item.icona;
        nom.text = item.name;
        descripcio.text = item.description;
        preu.text = item.price.ToString();
        itemSeleccionat = item;
    }

    public void ComprarItem()
    {
        this.photonView = GetComponent<PhotonView>();
        this.photonView.RPC("ComprarItemNet", PhotonTargets.All, PhotonNetwork.player.NickName, itemSeleccionat.id);
    }

    [PunRPC]
    public void ComprarItemNet(string playerName, int itemId)
    {
        switch(itemId)
        {
            case 1:
                ObjArmdura objArmadura = new ObjArmdura();
                objArmadura.imatge = itemSeleccionat.icona;
                objArmadura.Initialize();
                ComprarItemByType(playerName, objArmadura);
                break;
            case 2:
                ObjAtac objAtac = new ObjAtac();
                objAtac.imatge = itemSeleccionat.icona;
                objAtac.Initialize();
                ComprarItemByType(playerName, objAtac);
                break;
            case 3:
                ObjMana objMana = new ObjMana();
                objMana.imatge = itemSeleccionat.icona;
                objMana.Initialize();
                ComprarItemByType(playerName, objMana);
                break;
            case 4:
                ObjPodAbi objPodAbi = new ObjPodAbi();
                objPodAbi.imatge = itemSeleccionat.icona;
                objPodAbi.Initialize();
                ComprarItemByType(playerName, objPodAbi);
                break;
            case 5:
                ObjResMag objResMagic = new ObjResMag();
                objResMagic.imatge = itemSeleccionat.icona;
                objResMagic.Initialize();
                ComprarItemByType(playerName, objResMagic);
                break;
            case 6:
                ObjVelAtac objVelAtac = new ObjVelAtac();
                objVelAtac.imatge = itemSeleccionat.icona;
                objVelAtac.Initialize();
                ComprarItemByType(playerName, objVelAtac);
                break;
            case 7:
                ObjVelMov objVelMov = new ObjVelMov();
                objVelMov.imatge = itemSeleccionat.icona;
                objVelMov.Initialize();
                ComprarItemByType(playerName, objVelMov);
                break;
            case 8:
                ObjVida objVida = new ObjVida();
                objVida.imatge = itemSeleccionat.icona;
                objVida.Initialize();
                ComprarItemByType(playerName, objVida);
                break;
            case 9:
                PocioMana pocioMana = new PocioMana();
                pocioMana.imatge = itemSeleccionat.icona;
                pocioMana.Initialize();
                ComprarItemByType(playerName, pocioMana);
                break;
            case 10:
                PocioVida pocioVida = new PocioVida();
                pocioVida.imatge = itemSeleccionat.icona;
                pocioVida.Initialize();
                ComprarItemByType(playerName, pocioVida);
                break;
        }
    }

    private void ComprarItemByType(string playerName, Item item)
    {
        Unidad jugador = GameObject.Find(playerName).GetComponent<Unidad>();
        if (jugador != null)
        {
            if (item.Id == 9 && item.Id == 10)
            {
                if (jugador.GetGold() >= item.Price)
                {
                    item.Comprar(jugador);
                }
            }
            else
            {
                if (jugador.GetInventory() > 0)
                {
                    if (jugador.GetGold() >= item.Price)
                    {
                        item.Comprar(jugador);
                        if (item.Id != 9 && item.Id != 10)
                        {
                            jugador.SetInventory(jugador.GetInventory() - 1); // Sumem un objecte al inventari

                            // Emplenem espais a l'inventari del jugador que l'ha comprat
                            if (playerName == PhotonNetwork.playerName)
                            {
                                if (slot1.sprite == null)
                                {
                                    slot1.sprite = item.Icona;
                                }
                                else if (slot2.sprite == null)
                                {
                                    slot2.sprite = item.Icona;
                                }
                                else if (slot3.sprite == null)
                                {
                                    slot3.sprite = item.Icona;
                                }
                                else if (slot4.sprite == null)
                                {
                                    slot4.sprite = item.Icona;
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("No tens or suficient");
                    }
                }
                else
                {
                    Debug.Log("No tens espai suficient a l'inventari");
                }
            }
        }
    }
}
