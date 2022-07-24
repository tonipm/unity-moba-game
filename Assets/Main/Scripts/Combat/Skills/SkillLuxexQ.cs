using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLuxexQ : Skill { 

    private int damage;
    private string prefabActionName;

    // Use this for initialization
    void Start()
    {
        this.prefabActionName = "SkillsActions/SkillLuxexQ";
        this.damage = 15;
        this.manaCost = 250;
        this.coolDownTime = 12;
        this.time = 10;
    }

    public override bool Execute(int damageChamp)
    {
        if (this.canExecute)
        {
            GameObject go = PhotonNetwork.Instantiate(this.prefabActionName, this.firePoint.position, this.firePoint.rotation, 0);
            this.playerPV.RPC("SetBuffNet", PhotonTargets.All, PhotonNetwork.player.NickName, 2, 10f);

            this.time = 0;
            this.canExecute = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    [PunRPC]
    private void SetBuffNet(string playerName, int amount, float duracio)
    {
        Unidad jugador = GameObject.Find(playerName).GetComponent<Unidad>();
        if (jugador != null)
        {
            int statAnterior = jugador.GetMovementSpeed();
            jugador.SetMovementSpeed(jugador.GetMovementSpeed() + amount);
            StartCoroutine(EliminarBuff(duracio, playerName, statAnterior));
        }
    }

    private IEnumerator EliminarBuff(float time, string playerName, int statAnterior)
    {
        yield return new WaitForSeconds(time);
        Unidad jugador = GameObject.Find(playerName).GetComponent<Unidad>();
        jugador.SetMovementSpeed(statAnterior);
        NetManager netManager = GameObject.FindGameObjectWithTag("NetManager").GetComponent<NetManager>();
    }

    public override void Return(GameObject target)
    {

    }
}
