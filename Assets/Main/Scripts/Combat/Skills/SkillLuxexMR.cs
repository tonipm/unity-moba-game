using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLuxexMR : Skill
{
    private int damage;
    private string prefabActionName;

    // Use this for initialization
    void Start()
    {
        this.prefabActionName = "SkillsActions/SkillLuxexMR";
        this.damage = -22;
        this.manaCost = 0;
        this.coolDownTime = 1;
        this.time = this.coolDownTime;
    }

    public override bool Execute(int damageChamp)
    {
        if (this.canExecute)
        {
            GameObject go = PhotonNetwork.Instantiate(this.prefabActionName, this.firePoint.position, this.firePoint.rotation, 0);
            go.GetComponent<ActionLuxexMR>().SetSkill(this);
            go.GetComponent<Collider>().enabled = true;

            Unidad jugador = this.gameObject.GetComponent<Unidad>();
            Action action = go.GetComponent<ActionLuxexMR>();
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
