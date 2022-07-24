using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHeal : Skill {

    private int amount;
    private string prefabActionName;

	// Use this for initialization
	void Start () {
        this.amount = 50;
        this.manaCost = 50;
        this.prefabActionName = "SkillsActions/SkillHeal";
        this.coolDownTime = 10;
        this.time = this.coolDownTime;
    }
        
    public override bool Execute(int damageChamp)
    {
        if (this.canExecute)
        {
            this.playerPV.RPC("ModifyHealth", PhotonTargets.All, this.amount,false, this.gameObject.name);

            PhotonNetwork.Instantiate(this.prefabActionName, this.transform.position, Quaternion.LookRotation(Vector3.up), 0);

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
