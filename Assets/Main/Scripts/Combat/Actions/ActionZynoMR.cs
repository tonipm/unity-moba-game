using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionZynoMR : Action
{
    private int speed;

    // Use this for initialization
    void Start()
    {
        this.speed = 20;
        this.timeOut = 0.6f;
    }


    void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;
        if (otherGO != this.skill.gameObject)
        {
            CombatSystem otherCS = other.GetComponent<CombatSystem>();
            CombatSystem myCS = this.skill.GetCombatSystem();

            if (otherCS != null && otherGO != null)
            {
                if (otherCS.GetTeam() != myCS.GetTeam())
                {
                    this.skill.Return(other.gameObject);
                    this.photonView.RPC("AutoDestroy", PhotonTargets.All, null);
                }
            }
        }
    }

    protected override void Tick()
    {
        this.transform.position += this.transform.forward * this.speed * Time.deltaTime;
    }
}
