using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFireBall : Skill
{
    private int damage;
    private string prefabActionName;

    // Use this for initialization
    void Start()
    {
        this.prefabActionName = "SkillsActions/SkillFireBall";
        this.damage = -20;
        this.manaCost = 10;
        this.coolDownTime = 2;
        this.time = this.coolDownTime;
    }

    public override bool Execute()
    {
        if (this.canExecute)
        {
            GameObject ball = PhotonNetwork.Instantiate(this.prefabActionName, this.firePoint.position, this.firePoint.rotation, 0);
            ball.GetComponent<ActionFireBall>().SetSkill(this);
            ball.GetComponent<Collider>().enabled = true;

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
        targetPV.RPC("ModifyHealth", PhotonTargets.All, this.damage);
    }
}
