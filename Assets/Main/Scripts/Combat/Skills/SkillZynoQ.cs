using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillZynoQ : Skill {

    private int damage;
    private string prefabActionName;

    // Use this for initialization
    void Start()
    {
        this.prefabActionName = "SkillsActions/SkillZynoQ";
        this.damage = 10;
        this.manaCost = 20;
        this.coolDownTime = 6;
        this.time = 10;
    }

    public override bool Execute(int damageChamp)
    {
        if (this.canExecute)
        {
            GameObject ball = PhotonNetwork.Instantiate(this.prefabActionName, this.firePoint.position, this.firePoint.rotation, 0);
            this.playerPV.RPC("SetBuffNet", PhotonTargets.All, PhotonNetwork.player.NickName, 15, 10f);

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
            int statAnterior = jugador.GetAbilityPower();
            jugador.SetAbilityPower(jugador.GetAbilityPower() + amount);
            StartCoroutine(EliminarBuff(duracio, playerName, statAnterior));
        }
    }

    private IEnumerator EliminarBuff(float time, string playerName, int statAnterior)
    {
        yield return new WaitForSeconds(time);
        Unidad jugador = GameObject.Find(playerName).GetComponent<Unidad>();
        jugador.SetAbilityPower(statAnterior);
    }

    public override void Return(GameObject target)
    {

    }
}
