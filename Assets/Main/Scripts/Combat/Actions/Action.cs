using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour {
    
    protected float time;
    protected float timeOut;
    protected Skill skill;
    protected PhotonView photonView;
    private Unidad jugador;
    
    void Awake()
    {
        this.time = 0;
        this.photonView = GetComponent<PhotonView>();
    }
    
    void Update()
    {
        if (this.photonView.isMine)
        {
            this.time += Time.deltaTime;
            if (this.time > this.timeOut)
            {
                this.photonView.RPC("AutoDestroy", PhotonTargets.All, null);
            }
        }
        this.Tick();
    }

    public void SetSkill(Skill _skill)
    {
        this.skill = _skill;
    }

    public Unidad GetJugador()
    {
        return this.jugador;
    }

    public void SetJugador(Unidad _jugador)
    {
        this.jugador = _jugador;
    }

    [PunRPC]
    public void AutoDestroy()
    {
        if (this.photonView.isMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
    
    protected abstract void Tick();
}
