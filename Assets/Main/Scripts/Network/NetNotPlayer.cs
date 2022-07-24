using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetNotPlayer : MonoBehaviour
{
    public GameObject playerDataCanvasPrefab;
    private GameObject playerDataCanvas;
    private Rigidbody rb;
    private PhotonView pv;
    private Vector3 realPosition;
    private Quaternion realRotation;
    public PlayerDataCanvas pdc;

    // Use this for initialization
    void Start()
    {
        // EL meu personatge
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        CombatSystem cs = GetComponent<CombatSystem>();


        // Còpia del personatge a la xarxa
        Unidad unidad = this.gameObject.GetComponent<Unidad>();
        unidad.Initialize();

        pdc.playerHealthBar.maxValue = unidad.GetMaxHealth();
        pdc.playerHealthBar.value = unidad.GetHealth();
        cs.playerHealthBar = pdc.playerHealthBar;

        // Nom de la unitat
        if (cs.GetTeam() == 0)
        {
            pdc.playerNameText.color = Color.blue;
        }
        else if (cs.GetTeam() == 1)
        {
            pdc.playerNameText.color = Color.red;
        }
        else
        {
            pdc.playerNameText.color = Color.black;
        }
    }

    void OnDestroy()
    {
        if (this.playerDataCanvas != null)
        {
            Destroy(this.playerDataCanvas);
        }
    }
}
