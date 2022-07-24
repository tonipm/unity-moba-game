using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLuxexE : Skill { 

    private int damage;
    private string prefabActionName;

    // Use this for initialization
    void Start()
    {
        this.prefabActionName = "SkillsActions/SkillLuxexE";
        this.manaCost = 250;
        this.coolDownTime = 20;
        this.time = 10;
    }

    public override bool Execute(int damageChamp)
    {
        if (this.canExecute)
        {
            GameObject go = PhotonNetwork.Instantiate(this.prefabActionName, this.firePoint.position, this.firePoint.rotation, 0);
            Unidad jugador = gameObject.GetComponent<Unidad>();
            PhotonView pv = gameObject.GetComponent<PhotonView>();
            pv.RPC("ModifyHealth", PhotonTargets.All, jugador.GetMaxHealth(), false, 0, this.gameObject.name);

            this.time = 0;
            this.canExecute = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void Return(GameObject target)
    {

    }
}
