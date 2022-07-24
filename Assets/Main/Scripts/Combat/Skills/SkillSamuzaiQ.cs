using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSamuzaiQ : Skill { 

    private int damage;
    private string prefabActionName;

    // Use this for initialization
    void Start()
    {
        this.prefabActionName = "SkillsActions/SkillSamuzaiQ";
        this.damage = 20;
        this.manaCost = 250;
        this.coolDownTime = 12;
        this.time = 10;
    }

    public override bool Execute(int damageChamp)
    {
        if (this.canExecute)
        {
            GameObject go = PhotonNetwork.Instantiate(this.prefabActionName, this.firePoint.position, this.firePoint.rotation, 0);
            this.playerPV.RPC("SetBuffNet", PhotonTargets.All, PhotonNetwork.player.NickName, 3, 15, 10f);

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
    private void SetBuffNet(string playerName, int amount1, int amount2, float duracio)
    {
        Unidad jugador = GameObject.Find(playerName).GetComponent<Unidad>();
        if (jugador != null)
        {
            int statAnterior1 = jugador.GetMovementSpeed();
            int statAnterior2 = jugador.GetAtackSpeed();
            jugador.SetMovementSpeed(jugador.GetMovementSpeed() + amount1);
            jugador.SetAtackSpeed(jugador.GetAtackSpeed() + amount2);
            StartCoroutine(EliminarBuff(duracio, playerName, statAnterior1, statAnterior2));
        }
    }

    private IEnumerator EliminarBuff(float time, string playerName, int statAnterior1, int statAnterior2)
    {
        yield return new WaitForSeconds(time);
        Unidad jugador = GameObject.Find(playerName).GetComponent<Unidad>();
        jugador.SetMovementSpeed(statAnterior1);
        jugador.SetAtackSpeed(statAnterior2);
        NetManager netManager = GameObject.FindGameObjectWithTag("NetManager").GetComponent<NetManager>();
    }

    public override void Return(GameObject target)
    {

    }
}
