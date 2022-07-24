using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetJugador : MonoBehaviour {
    
    private PhotonView pv;

    // Use this for initialization
    void Start()
    {
        // EL meu personatge
        pv = GetComponent<PhotonView>();
        object[] data;
        data = this.pv.instantiationData;

        if (pv.isMine)
        {
            GameObject netManagerOBJ = GameObject.FindGameObjectWithTag("NetManager");
            NetManager netManagerScript = netManagerOBJ.GetComponent<NetManager>();
        }
        else
        {
            string nomJugador = (string)data[0];
            this.gameObject.name = "J-" + nomJugador;
        }
    }

    [PunRPC]
    private void RemovePlayerNet(string name)
    {
        GameObject jugadorGO = GameObject.Find(name);
        Destroy(jugadorGO);
    }
}
