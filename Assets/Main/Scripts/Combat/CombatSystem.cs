using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSystem : MonoBehaviour {

    public bool isNexus;
    public Slider playerHealthBar, playerManaBar;
    public Skill q_skill, e_skill, ml_skill, mr_skill;

    private PhotonView pv;
    private int currentHealth;
    private int maxHealth;
    private int currentMana;
    private int maxMana;
    private float manaTime;
    private float time;
    public int team;

    // Use this for initialization
    void Start () {
        this.maxHealth = 100;
        this.currentHealth = this.maxHealth;
        this.maxMana = 100;
        this.currentMana = this.maxMana;
        this.manaTime = 1;
        this.time = 0;

        this.pv = GetComponent<PhotonView>();

        if (this.q_skill != null)
        {
            this.q_skill.SetPhotonView(this.pv);
            this.q_skill.SetCombatSystem(this);
        }
        if (this.e_skill != null)
        {
            this.e_skill.SetPhotonView(this.pv);
            this.e_skill.SetCombatSystem(this);
        }
        if (this.ml_skill != null)
        {
            this.ml_skill.SetPhotonView(this.pv);
            this.ml_skill.SetCombatSystem(this);
        }
        if (this.mr_skill != null)
        {
            this.mr_skill.SetPhotonView(this.pv);
            this.mr_skill.SetCombatSystem(this);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (this.pv.isMine && !this.isNexus)
        {
            // Regeneracio manà
            this.time += Time.deltaTime;
            if (this.time >= this.manaTime)
            {
                this.time = 0;
                this.ModifyMana(10);
            }
            // Skills Input
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (this.q_skill != null)
                {
                    if (this.q_skill.GetManaCost() <= this.currentMana)
                    {
                        if (this.q_skill.Execute())
                        {
                            this.ModifyMana(-this.q_skill.GetManaCost());
                        }
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (this.e_skill != null)
                {
                    if (this.e_skill.GetManaCost() <= this.currentMana)
                    {
                        if (this.e_skill.Execute())
                        {
                            this.ModifyMana(-this.e_skill.GetManaCost());
                        }
                    }
                }
            }
            else if(Input.GetButtonDown("Fire1"))
            {
                if (this.ml_skill != null)
                {
                    if (this.ml_skill.GetManaCost() <= this.currentMana)
                    {
                        if (this.ml_skill.Execute())
                        {
                            this.ModifyMana(-this.ml_skill.GetManaCost());
                        }
                    }
                }
            }
            else if(Input.GetButtonDown("Fire2"))
            {
                if (this.mr_skill != null)
                {
                    if (this.mr_skill.GetManaCost() <= this.currentMana)
                    {
                        if (this.mr_skill.Execute())
                        {
                            this.ModifyMana(-this.mr_skill.GetManaCost());
                        }
                    }
                }
            }
        }
	}

    public PhotonView GetPhotonView()
    {
        return this.pv;
    }

    public int GetTeam()
    {
        return this.team;
    }


    public void SetTeam(int _team)
    {
        this.team = _team;
    }

    public void ModifyMana(int amount)
    {
        this.currentMana += amount;
        if (this.currentMana < 0)
        {
            this.currentMana = 0;
        }
        if (this.currentMana > this.maxMana)
        {
            this.currentMana = maxMana;
        }

        if (this.playerManaBar != null)
        {
            this.playerManaBar.value = this.currentMana;
        }
    }

    [PunRPC] // PhotonUnityNetwork RPC -> Aquesta funció s'executarà eper a tots els de la xarxa
    public void ModifyHealth(int amount)
    {
        Debug.Log("ModifyHealth");
        this.currentHealth += amount;

        // El deixa com a molt al màxim
        if (this.currentHealth > this.maxHealth)
        {
            this.currentHealth = this.maxHealth;
        }

        // Si mor ..
        if (this.currentHealth <= 0)
        {
            this.currentHealth = 0;

            if (this.pv.isMine)
            {
                // RESPAWN
                NetManager manager = GameObject.FindGameObjectWithTag("NetManager").GetComponent<NetManager>();
                if (this.isNexus)
                {
                    manager.GameOver(this.team);
                }
                else
                {
                    manager.PlayerDeath();
                }
                PhotonNetwork.Destroy(this.gameObject);
            }
        }

        if (this.playerHealthBar != null)
        {
            this.playerHealthBar.value = this.currentHealth;
        }
    }
}
