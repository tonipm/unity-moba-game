using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSamuzaiE : Skill {

    private int damage;
    private int damageBase;
    private string prefabActionName;

    // Use this for initialization
    void Start()
    {
        this.isMele = false;
        this.prefabActionName = "SkillsActions/SkillSamuzaiE";
        this.damage = -150;
        this.damageBase = -150;
        this.manaCost = 250;
        this.coolDownTime = 12;
        this.time = this.coolDownTime;
    }

    public override bool Execute(int damageChamp)
    {
        if (this.canExecute)
        {
            this.damage = this.damageBase - damageChamp;
            GameObject go = PhotonNetwork.Instantiate(this.prefabActionName, this.firePoint.position, this.firePoint.rotation, 0);
            go.GetComponent<Collider>().enabled = true;

            Unidad jugador = this.gameObject.GetComponent<Unidad>();
            Action action = go.GetComponent<ActionSamuzaiE>();
            action.SetSkill(this);
            action.SetJugador(jugador);

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
        PhotonView targetPV = target.GetComponent<PhotonView>();
        targetPV.RPC("ModifyHealth", PhotonTargets.All, this.damage, true, 2, this.gameObject.name);
    }
}
