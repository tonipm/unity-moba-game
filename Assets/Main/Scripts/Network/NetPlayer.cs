using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetPlayer : MonoBehaviour
{

    public GameObject playerDataCanvasPrefab;
    private GameObject playerDataCanvas;
    private Rigidbody rb;
    private PhotonView pv;
    private Vector3 realPosition;
    private Quaternion realRotation;

    // Use this for initialization
    void Start()
    {
        // EL meu personatge
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        object[] data;
        data = this.pv.instantiationData;
        CombatSystem cs = GetComponent<CombatSystem>();
        cs.SetTeam((int)data[1]);

        if (pv.isMine)
        {
            GetComponent<PlayerController>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

            if (camera != null)
            {
                PlayerCamera pc = camera.GetComponent<PlayerCamera>();
                pc.enabled = true;
                pc.player = GetComponent<PlayerController>();
            }

            GameObject netManagerOBJ = GameObject.FindGameObjectWithTag("NetManager");
            NetManager netManagerScript = netManagerOBJ.GetComponent<NetManager>();

            cs.playerHealthBar = netManagerScript.playerHUD.playerHealthBar;
            cs.playerManaBar = netManagerScript.playerHUD.playerManaBar;

            if (cs.mr_skill != null)
            {
                netManagerScript.playerHUD.skill_qImg.sprite = cs.mr_skill.skillSprite;
                cs.mr_skill.coolDownSlider = netManagerScript.playerHUD.skill_q;
            }
            if (cs.q_skill != null)
            {
                netManagerScript.playerHUD.skill_eImg.sprite = cs.q_skill.skillSprite;
                cs.q_skill.coolDownSlider = netManagerScript.playerHUD.skill_e;
            }
            if (cs.e_skill != null)
            {
                netManagerScript.playerHUD.skill_mlImg.sprite = cs.e_skill.skillSprite;
                cs.e_skill.coolDownSlider = netManagerScript.playerHUD.skill_ml;
            }
            if (cs.b_skill != null)
            {
                netManagerScript.playerHUD.skill_mrImg.sprite = cs.b_skill.skillSprite;
                cs.b_skill.coolDownSlider = netManagerScript.playerHUD.skill_mr;
            }
        }
        else
        {
            string nomJugador = (string)data[0];

            // Còpia del personatge a la xarxa
            this.playerDataCanvas = (GameObject)Instantiate(this.playerDataCanvasPrefab,
                                                            this.transform.position + Vector3.up * 2,
                                                            Quaternion.Euler(new Vector3(50, 0, 0)));

            this.playerDataCanvas.name = "PDC" + nomJugador;
            PlayerDataCanvas pdc = this.playerDataCanvas.GetComponent<PlayerDataCanvas>();

            pdc.targetPlayer = this.transform;
            pdc.playerNameText.text = nomJugador;

            this.gameObject.name = nomJugador;
            Unidad unidad = this.gameObject.GetComponent<Unidad>();
            unidad.Initialize();

            pdc.playerHealthBar.maxValue = unidad.GetMaxHealth();
            pdc.playerHealthBar.value = unidad.GetHealth();
            cs.playerHealthBar = pdc.playerHealthBar;

            // Nom del campió dels equips
            if (cs.GetTeam() == 0)
            {
                pdc.playerNameText.color = Color.blue;
            }
            else
            {
                pdc.playerNameText.color = Color.red;
            }
        }
    }

    private void Update()
    {
        if (!this.pv.isMine)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, this.realPosition, Time.deltaTime * 10);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.realRotation, Time.deltaTime * 10);
        }
    }

    void OnDestroy()
    {
        if (this.playerDataCanvas != null)
        {
            Destroy(this.playerDataCanvas);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Enviem i rebem les dades de posició i velocitat dels jugadors en partida
        // Si estem enviant dades o rebent
        if (stream.isWriting)
        {
            stream.SendNext(this.transform.position);
            stream.SendNext(this.transform.rotation);
        }
        else
        {
            this.realPosition = (Vector3)stream.ReceiveNext();
            this.realRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
