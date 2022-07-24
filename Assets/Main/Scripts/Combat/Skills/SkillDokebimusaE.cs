using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDokebimusaE : Skill
{

    private int damage;
    private string prefabActionName;

    // Use this for initialization
    void Start()
    {
        this.prefabActionName = "SkillsActions/SkillDokebimusaQ";
        this.damage = 15;
        this.manaCost = 250;
        this.coolDownTime = 12;
        this.time = 10;
    }

    public override bool Execute(int damageChamp)
    {
        if (this.canExecute)
        {
            GameObject ball = PhotonNetwork.Instantiate(this.prefabActionName, this.firePoint.position, this.firePoint.rotation, 0);
            this.playerPV.RPC("SetBuffNet", PhotonTargets.All, PhotonNetwork.player.NickName, 999999, 4f);

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
            int statAnterior1 = jugador.GetArmour();
            int statAnterior2 = jugador.GetMagicArmour();
            jugador.SetArmour(jugador.GetArmour() + amount);
            jugador.SetMagicArmour(jugador.GetMagicArmour() + amount);
            StartCoroutine(EliminarBuff(duracio, playerName, statAnterior1, statAnterior2));
        }
    }

    private IEnumerator EliminarBuff(float time, string playerName, int statAnterior1, int statAnterior2)
    {
        yield return new WaitForSeconds(time);
        Unidad jugador = GameObject.Find(playerName).GetComponent<Unidad>();
        jugador.SetArmour(statAnterior1);
        jugador.SetMagicArmour(statAnterior2);
    }

    public override void Return(GameObject target)
    {

    }
}