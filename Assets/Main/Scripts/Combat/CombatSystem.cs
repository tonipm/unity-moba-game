using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSystem : MonoBehaviour
{
    public bool isNexus;
    public bool isTorre;
    public bool isChampion;
    public bool isMonster;
    public Slider playerHealthBar, playerManaBar;
    public Skill mr_skill, q_skill, e_skill, b_skill;
    public int team;
    public Unidad unidad;
    public Animation animations;
    public GameObject atacTorre;

    private Unidad jugadorEnemic;
    private PhotonView pv;
    private Text goldText;
    private bool[] torresBlau;
    private bool[] torresVermell;
    private GameObject[] torresTagBlau;
    private GameObject[] pdcTorresBlau;
    private GameObject[] torresTagVermell;
    private GameObject[] pdcTorresVermell;
    private GameObject dragon;
    private GameObject baron;
    private GameObject blue1;
    private GameObject blue2;
    private GameObject red1;
    private GameObject red2;

    private int abilityPower;
    private int armour;
    private int magicArmour;
    private int atackDamage;
    private int atackSpeed;
    private int currentArmor;
    private int experience;
    private int health;
    private int healthRegen;
    private int mana;
    private int manaRegen;
    private int maxExperience;
    private int maxHealth;
    private int maxMana;
    private int movementSpeed;
    private float time;
    private NetManager netManager;
    private int damageAbility;
    private bool shopOpened;
    private bool StatsOpened;
    private bool tornadaBase;
    private Vector3 initialPos;
    private Quaternion initialRotation;
    private PlayerController playerController;

    //VARS MONSTER
    private bool playerInTerritory;
    private float AttackInterval = 1; 
    private float elapsedTime = 0;
    private Unidad player;
    private bool mort;

    //VARS PLAYERS
    private int AssTeam1;
    private int AssTeam2;

    //VARS TOWER
    private bool playerInTower;
    private float elapsedTimeTower = 0;
    private PhotonView targetActual;



    // Use this for initialization
    void Start()
    {
        this.pv = GetComponent<PhotonView>();
        this.netManager = GameObject.FindGameObjectWithTag("NetManager").GetComponent<NetManager>();

        torresTagBlau = GameObject.FindGameObjectsWithTag("StructureBlau");
        torresTagVermell = GameObject.FindGameObjectsWithTag("StructureVermell");
        pdcTorresBlau = GameObject.FindGameObjectsWithTag("PDCTorreBlau");
        pdcTorresVermell = GameObject.FindGameObjectsWithTag("PDCTorreVermell");

        torresBlau = new bool[6] { true, true, true, true, true, true };
        torresVermell = new bool[6] { true, true, true, true, true, true };

        dragon = GameObject.FindGameObjectWithTag("Dragon");
        baron = GameObject.FindGameObjectWithTag("Baron");
        blue1 = GameObject.FindGameObjectWithTag("Blue1");
        blue2 = GameObject.FindGameObjectWithTag("Blue2");
        red1 = GameObject.FindGameObjectWithTag("Red1");
        red2 = GameObject.FindGameObjectWithTag("Red2");

        if (pv.isMine && unidad != null)
        {
            this.initialPos = this.gameObject.transform.position;
            this.initialRotation = this.gameObject.transform.rotation;

            // Si és campió llavors guardem l'element de text on es mostra l'or
            if (this.isChampion)
            {
                this.netManager.playerHUD.imgChamp.sprite = unidad.GetImage();
                this.goldText = GameObject.FindGameObjectWithTag("Gold").GetComponent<Text>();
            }

            // Estadístiques unitat
            this.unidad.Initialize();
            this.abilityPower = unidad.GetAbilityPower();
            this.armour = unidad.GetArmour();
            this.magicArmour = unidad.GetMagicArmour();
            this.atackDamage = unidad.GetAtackDamage();
            this.atackSpeed = unidad.GetAtackSpeed();
            this.experience = unidad.GetExperience();
            this.health = unidad.GetHealth();
            this.healthRegen = unidad.GetHealthRegen();
            this.mana = unidad.GetMana();
            this.manaRegen = unidad.GetManaRegen();
            this.maxExperience = unidad.GetMaxExperience();
            this.maxHealth = unidad.GetMaxHealth();
            this.maxMana = unidad.GetMaxMana();
            this.movementSpeed = unidad.GetMovementSpeed();
            this.time = 0;
            this.playerInTerritory = false;

            // Barres de vida i manà
            if (this.playerHealthBar != null)
            {
                this.playerHealthBar.maxValue = this.maxHealth; // Vida
                this.playerHealthBar.value = this.health; // Vida
            }

            if (this.isChampion)
            {
                this.playerManaBar.value = this.maxMana; // Manà
                this.playerManaBar.maxValue = this.maxMana; // Manà
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.pv = GetComponent<PhotonView>();
        if (this.pv.isMine && !this.isNexus)
        {
            // Regeneracio manà
            this.time += Time.deltaTime;
            if (this.time >= 1)
            {
                // Si és un campió recuperarà manà, vida i sumarà or
                if (isChampion && unidad != null)
                {
                    this.time = 0;
                    this.ModifyMana(this.unidad.GetManaRegen());
                    this.ModifyHealth(this.unidad.GetHealthRegen(), false, 0, this.gameObject.name);
                    this.unidad.SetGold(this.unidad.GetGold() + 10);
                    goldText.text = this.unidad.GetGold().ToString();
                }
            }
            // Skills Input
            if (Input.GetButtonDown("Fire2"))
            {
                if (this.mr_skill != null)
                {
                    if (this.mr_skill.GetManaCost() <= this.mana)
                    {
                        if (this.mr_skill.Execute(DamageAbility(this.mr_skill)))
                        {
                            this.ModifyMana(-this.mr_skill.GetManaCost());
                        }
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                if (this.q_skill != null)
                {
                    if (this.q_skill.GetManaCost() <= this.mana)
                    {
                        if (this.q_skill.Execute(DamageAbility(this.q_skill)))
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
                    if (this.e_skill.GetManaCost() <= this.mana)
                    {
                        if (this.e_skill.Execute(DamageAbility(this.e_skill)))
                        {
                            this.ModifyMana(-this.e_skill.GetManaCost());
                            if (this.animations != null)
                            {
                                //this.animations.Play("attack");
                            }
                        }
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                if (this.b_skill != null)
                {
                    if (this.b_skill.GetManaCost() <= this.mana)
                    {
                        if (this.b_skill.Execute(DamageAbility(this.b_skill)))
                        {
                            this.tornadaBase = true;
                            this.pv.RPC("ModifyHealth", PhotonTargets.All, unidad.GetMaxHealth(), false, 0, this.gameObject.name);
                            this.ModifyMana(unidad.GetMaxMana());
                        }
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                if (pv.isMine)
                {
                    ShopManager shopGO = netManager.shopManager;
                    if (!this.shopOpened)
                    {
                        shopOpened = true;
                        shopGO.gameObject.SetActive(true);
                    }
                    else
                    {
                        shopOpened = false;
                        shopGO.gameObject.SetActive(false);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (pv.isMine)
                {
                    StatsWindow statsWindow = netManager.statsWindow;
                    if (!this.StatsOpened)
                    {
                        StatsOpened = true;
                        statsWindow.AD.text = this.unidad.GetAtackDamage().ToString();
                        statsWindow.AP.text = this.unidad.GetAbilityPower().ToString();
                        statsWindow.AV.text = this.unidad.GetAtackSpeed().ToString();
                        statsWindow.RV.text = this.unidad.GetHealthRegen().ToString();
                        statsWindow.RM.text = this.unidad.GetManaRegen().ToString();
                        statsWindow.A.text = this.unidad.GetArmour().ToString();
                        statsWindow.MR.text = this.unidad.GetMagicArmour().ToString();
                        statsWindow.gameObject.SetActive(true);
                    }
                    else
                    {
                        StatsOpened = false;
                        statsWindow.gameObject.SetActive(false);
                    }
                }
            }
        }

        if (this.mr_skill != null)
        {
            this.mr_skill.SetPhotonView(this.pv);
            this.mr_skill.SetCombatSystem(this);
        }
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
        if (this.b_skill != null)
        {
            this.b_skill.SetPhotonView(this.pv);
            this.b_skill.SetCombatSystem(this);
        }

        // IA dels monstres
        if (this.isMonster)
        {
            if (jugadorEnemic != null && this.unidad != null)
            {
                if (this.unidad.GetHealth() != this.unidad.GetMaxHealth() && !this.mort)
                {
                    PhotonView targetPV = jugadorEnemic.GetComponent<PhotonView>();
                    //rotate to look at player
                    if (!playerInTerritory)
                    {
                        transform.LookAt(jugadorEnemic.gameObject.transform.position);
                        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

                        //move towards player
                        if (Vector3.Distance(transform.position, jugadorEnemic.gameObject.transform.position) < 10)
                        {
                            transform.Translate(new Vector3(4 * Time.deltaTime, 0, 0));

                            if (Time.time > elapsedTime)
                            {
                                targetPV.RPC("ModifyHealth", PhotonTargets.All, -1 * unidad.GetAtackDamage(), true, 1, this.gameObject.name);
                                animations.Stop();
                                animations.Play("Attack");
                                elapsedTime = Time.time + AttackInterval;
                            }
                        }
                        else
                        {
                            playerInTerritory = false;
                            transform.rotation = initialRotation;
                            transform.position = initialPos;
                            this.pv.RPC("ModifyHealth", PhotonTargets.All, this.unidad.GetMaxHealth(), false, 0, this.gameObject.name);
                            animations.Stop();
                            animations.Play("Idle");
                        }
                    }
                    else
                    {
                        transform.Translate(new Vector3(4 * Time.deltaTime, 0, 0));
                    }
                    targetPV.RPC("MoureMonstre", PhotonTargets.All, gameObject.tag, transform.position.x, transform.position.y, transform.position.z);
                }
            }
        }
        if (this.isTorre)
        {
            if (this.unidad != null)
            {
                if (playerInTower)
                {
                    if (jugadorEnemic != null)
                    {
                        PhotonView targetPV = jugadorEnemic.GetComponent<PhotonView>();
                        if (targetActual == null)
                        {
                            atacTorre.GetComponent<MeshRenderer>().enabled = false;
                            targetActual = targetPV;
                            targetActual.RPC("CanviarVariable", PhotonTargets.All, targetPV.gameObject.name);
                        }
                        else
                        {
                            if (Time.time > elapsedTimeTower)
                            {
                                atacTorre.GetComponent<MeshRenderer>().enabled = true;
                                targetActual.RPC("ModifyHealth", PhotonTargets.All, -1 * unidad.GetAtackDamage(), true, 1, this.gameObject.name);
                                elapsedTimeTower = Time.time + AttackInterval;
                            }
                        }
                    }

                    if (unidad.GetHealth() <= 0)
                    {
                        atacTorre.GetComponent<MeshRenderer>().enabled = false;
                        playerInTower = false;
                        targetActual = null;
                    }
                }
                else
                {
                    atacTorre.GetComponent<MeshRenderer>().enabled = false;
                    targetActual = null;
                    Debug.Log("Jugador fora torre");
                }
            }
        }
    }

    [PunRPC]
    private void CanviarVariable(string nom)
    {
        GameObject go = GameObject.Find(nom);
        targetActual = go.GetComponent<PhotonView>();
    }


    [PunRPC]
    private void MoureMonstre(string monstre, float x, float y, float z)
    {
        switch(monstre)
        {
            case "Dragon":
                dragon.transform.position = new Vector3(x, y, z);
                break;
            case "Baron":
                baron.transform.position = new Vector3(x, y, z);
                break;
            case "Blue1":
                blue1.transform.position = new Vector3(x, y, z);
                break;
            case "Blue2":
                blue2.transform.position = new Vector3(x, y, z);
                break;
            case "Red1":
                red1.transform.position = new Vector3(x, y, z);
                break;
            case "Red2":
                red2.transform.position = new Vector3(x, y, z);
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Action action = other.gameObject.GetComponent<Action>();
        if (action != null)
        {
            jugadorEnemic = action.GetJugador();
        }
        else
        {
            if (other.gameObject.tag == "Arma")
            {
                jugadorEnemic = other.transform.root.gameObject.GetComponent<Unidad>();
                bool execSkill = jugadorEnemic.GetComponent<Skill>().execSkill;
                if (execSkill)
                {
                    this.gameObject.GetComponent<PhotonView>().RPC("ModifyHealth", PhotonTargets.All, -1 * jugadorEnemic.GetAtackDamage(), true, 1, this.gameObject.name);
                }
            }
        }

        if (other.gameObject != null && jugadorEnemic != null)
        {
            if (other.gameObject == jugadorEnemic.gameObject)
            {
                playerInTerritory = true;
            }
        }
        Unidad otherUnidad = other.gameObject.GetComponent<Unidad>();
        if (otherUnidad != null)
        {
            //torre
            if (this.isTorre)
            {
                if (otherUnidad.GetCombatSystem().GetTeam() != this.team)
                {
                    jugadorEnemic = otherUnidad;
                    playerInTower = true;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Action action = other.gameObject.GetComponent<Action>();
        Unidad otherUnidad = other.gameObject.GetComponent<Unidad>();

        if (otherUnidad != null)
        {
            //torre
            if (this.isTorre)
            {
                if (otherUnidad.GetCombatSystem().GetTeam() != this.team)
                {
                    Debug.Log("Jugador fora la torre");
                    playerInTower = false;
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
        // Afegim o treiem el manà a la unitat
        this.unidad.SetMana(this.unidad.GetMana() + amount);

        // Comprovem si el manà és inferior a 0 o superior al màxim
        if (this.unidad.GetMana() < 0)
        {
            this.unidad.SetMana(0);
        }
        if (this.unidad.GetMana() > this.unidad.GetMaxMana())
        {
            this.unidad.SetMana(this.unidad.GetMaxMana());
        }

        // Afegim els nous valors a la barra de manà del HUD
        if (this.playerManaBar != null)
        {
            this.playerManaBar.value = this.unidad.GetMana();
            this.playerManaBar.maxValue = this.unidad.GetMaxMana();
        }
    }

    [PunRPC] // PhotonUnityNetwork RPC -> Aquesta funció s'executarà per a tots els de la xarxa
    public void ModifyHealth(int amount, bool enemic, int isMele, string nomJugador)
    {
        // isMele = 0 -> Regeneració de vida
        // isMele = 1 -> És dany d'atac per tant farem servir armadura
        // isMele = 2 -> És dany de poder d'habiliat per tant farem servir armadura màgica

        // Si el campió rep mal quan està tornant a base llavors es cancel·la 
        if (playerController != null)
        {
            if (this.tornadaBase && this.isChampion && enemic)
            {
                playerController.SetCanMove(true);
                this.b_skill.GetComponent<SkillBase>().timePassed = -1;
            }
        }

        // Comprovem si és un nexe qui rep mal
        // Si no s'han destruit les torres enemigues el nexe no rebrà mal
        if (this.isNexus)
        {
            torresTagBlau = GameObject.FindGameObjectsWithTag("StructureBlau");
            torresTagVermell = GameObject.FindGameObjectsWithTag("StructureVermell");
            bool[] boolArrayBlau = new bool[6];
            bool[] boolArrayVermell = new bool[6];

            int posBol = 0;
            foreach (GameObject b in torresTagBlau)
            {
                boolArrayBlau[posBol] = b.GetComponent<Torres>().isDestroyed;
                posBol++;
            }

            posBol = 0;
            foreach (GameObject b in torresTagVermell)
            {
                boolArrayVermell[posBol] = b.GetComponent<Torres>().isDestroyed;
                posBol++;
            }

            // Comprovem de quin equip som, si s'han eliminat els rivals llavors farem mal al nexe
            if (this.team == 0)
            {
                if ((boolArrayBlau[0] && boolArrayBlau[3]) || (boolArrayBlau[1] && boolArrayBlau[4]) || (boolArrayBlau[2] && boolArrayBlau[5]))
                {
                    this.unidad.SetHealth(this.unidad.GetHealth() + amount);
                }
            }
            else
            {
                if ((boolArrayVermell[0] && boolArrayVermell[3]) || (boolArrayVermell[1] && boolArrayVermell[4]) || (boolArrayVermell[2] && boolArrayVermell[5]))
                {
                    this.unidad.SetHealth(this.unidad.GetHealth() + amount);
                }
            }
        }
        // Si no és un nexe llavors comprovarem el mal que rep la unitat segons si l'atac és màgic o no per aplicar una defensa o una altre
        else
        {
            // Comprovar quin tipus de mal rebem (regeneració de vida, dany d'atac, dany màgic
            // Afegim o treiem la vida a la untiat
            if (isMele == 1)
            {
                this.unidad.SetHealth(this.unidad.GetArmour() + this.unidad.GetHealth() + amount);
            }
            else if (isMele == 2)
            {
                this.unidad.SetHealth(this.unidad.GetMagicArmour() + this.unidad.GetHealth() + amount);
            }
            else
            {
                this.unidad.SetHealth(this.unidad.GetHealth() + amount);
            }
        }

        // Comprovem que no super el màxim de vida
        if (this.unidad.GetHealth() > this.unidad.GetMaxHealth())
        {
            this.unidad.SetHealth(this.unidad.GetMaxHealth());
        }

        // Si mor ..
        if (this.unidad.GetHealth() <= 0)
        {
            this.unidad.SetHealth(0);
            
            // RESPAWN
            NetManager manager = GameObject.FindGameObjectWithTag("NetManager").GetComponent<NetManager>();
            if (this.isNexus)
            {
                Debug.Log("Es nexus");
                PhotonNetwork.Destroy(this.gameObject);
                Destroy(GameObject.Find("PDC" + this.gameObject.name));
                this.pv.RPC("GameOver", PhotonTargets.All, this.team);
            }
            // Comprovar si és torre
            else if (this.isTorre)
            {
                int pos = 0;
                bool trobatTorre = false;
                if (this.team == 0)
                {
                    while (!trobatTorre)
                    {
                        if (torresTagBlau[pos] == this.gameObject)
                        {
                            trobatTorre = true;
                        }
                        pos++;
                    }
                    this.pv.RPC("EliminarTorre", PhotonTargets.All, pos, team);
                }
                else
                {
                    while (!trobatTorre)
                    {
                        if (torresTagVermell[pos] == this.gameObject)
                        {
                            trobatTorre = true;
                        }
                        pos++;
                    }
                    this.pv.RPC("EliminarTorre", PhotonTargets.All, pos, team);
                }
            }
            else if (this.isMonster)
            {
                // Els monstres donaran BUFFS al morir
                this.mort = true;
                this.pv.RPC("EliminarMonstreNet", PhotonTargets.All, this.gameObject.tag);
                this.pv.RPC("SetBuffNet", PhotonTargets.All, this.gameObject.tag, nomJugador);

                switch (this.gameObject.tag)
                {
                    case "Blue1":
                        this.pv.RPC("LevelGold", PhotonTargets.All, "Blue", nomJugador);
                        break;                                      
                    case "Blue2":                                   
                        this.pv.RPC("LevelGold", PhotonTargets.All, "Blue", nomJugador);
                        break;
                    case "Red1":
                        this.pv.RPC("LevelGold", PhotonTargets.All, "Red", nomJugador);
                        break;                                      
                    case "Red2":                                    
                        this.pv.RPC("LevelGold", PhotonTargets.All, "Red", nomJugador);
                        break;
                    case "Dragon":
                        this.pv.RPC("LevelGold", PhotonTargets.All, "Dragon", nomJugador);
                        break;
                    case "Baron":
                        this.pv.RPC("LevelGold", PhotonTargets.All, "Baron", nomJugador);
                        break;
                }
            }
            else
            {
                // RESPAWN JUGADOR
                if (this.pv.isMine)
                {
                    this.pv.RPC("LevelGold", PhotonTargets.All, this.gameObject.name, nomJugador);
                    this.pv.RPC("RefreshHudTop", PhotonTargets.All, this.gameObject.name, nomJugador);
                    this.netManager.cameraScene.SetActive(true);
                    this.netManager.cameraMain.SetActive(false);
                    this.netManager.playerHUD.gameObject.SetActive(false);
                    this.gameObject.transform.position = initialPos;
                    this.gameObject.transform.rotation = initialRotation;
                    this.gameObject.GetComponent<PlayerController>().SetCanMove(false);
                    Unidad un = this.gameObject.GetComponent<Unidad>();
                    this.pv.RPC("ModifyHealth", PhotonTargets.All, un.GetMaxHealth(), false, 0, this.gameObject.name);
                    StartCoroutine(RespawnJugador(5f));
                }
            }
        }

        // Afegim els nous valors a la barra de vida del HUD
        if (this.playerHealthBar != null)
        {
            this.playerHealthBar.maxValue = this.unidad.GetMaxHealth();
            this.playerHealthBar.value = this.unidad.GetHealth();
        }
    }

    [PunRPC]
    public void LevelGold(string mort, string jugador)
    {
        GameObject goJugador = GameObject.Find(jugador);
        Unidad unJugador = goJugador.GetComponent<Unidad>();
        GameObject[] playersList = GameObject.FindGameObjectsWithTag("Player");

        switch (mort)
        {
            case "Champion":
                unJugador.SetGold(unJugador.GetGold() + 200);
                unJugador.SetExperience(unJugador.GetExperience() + 300);
                break;
            case "Torre":
                foreach (GameObject player in playersList)
                {
                    CombatSystem cmbtPlayer = player.GetComponent<CombatSystem>();
                    Unidad uPlayer = player.GetComponent<Unidad>();

                    if (cmbtPlayer.GetTeam() == unJugador.GetCombatSystem().GetTeam())
                    {
                        uPlayer.SetGold(unJugador.GetGold() + 300);
                        uPlayer.SetExperience(unJugador.GetExperience() + 500);
                    }
                }
                break;
            case "Blue":
                unJugador.SetGold(unJugador.GetGold() + 150);
                unJugador.SetExperience(unJugador.GetExperience() + 150);
                break;
            case "Red":
                unJugador.SetGold(unJugador.GetGold() + 150);
                unJugador.SetExperience(unJugador.GetExperience() + 150);
                break;
            case "Dragon":
                foreach (GameObject player in playersList)
                {
                    CombatSystem cmbtPlayer = player.GetComponent<CombatSystem>();
                    Unidad uPlayer = player.GetComponent<Unidad>();

                    if (cmbtPlayer.GetTeam() == unJugador.GetCombatSystem().GetTeam())
                    {
                        unJugador.SetGold(unJugador.GetGold() + 400);
                        unJugador.SetExperience(unJugador.GetExperience() + 500);
                    }
                }
                break;
            case "Baron":
                foreach (GameObject player in playersList)
                {
                    CombatSystem cmbtPlayer = player.GetComponent<CombatSystem>();
                    Unidad uPlayer = player.GetComponent<Unidad>();

                    if (cmbtPlayer.GetTeam() == unJugador.GetCombatSystem().GetTeam())
                    {
                        unJugador.SetGold(unJugador.GetGold() + 600);
                        unJugador.SetExperience(unJugador.GetExperience() + 500);
                    }
                }
                break;
        }
        if (unJugador.GetExperience() >= 500)
        {
            unJugador.SetLevel(unJugador.GetLevel() + 1);
            unJugador.SetExperience(unJugador.GetExperience() - 500);

            unJugador.SetMaxHealth(unJugador.GetMaxHealth() + 50);
            unJugador.SetMaxMana(unJugador.GetMaxMana() + 50);
            unJugador.SetHealthRegen(unJugador.GetHealthRegen() + 2);
            unJugador.SetManaRegen(unJugador.GetManaRegen() + 2);
            unJugador.SetAtackDamage(unJugador.GetAtackDamage() + 7);
            unJugador.SetAtackSpeed(unJugador.GetAtackSpeed() + 7);
            unJugador.SetAbilityPower(unJugador.GetAbilityPower() + 7);
            unJugador.SetArmour(unJugador.GetArmour() + 1);
            unJugador.SetMagicArmour(unJugador.GetMagicArmour() + 1);

            //this.netManager.experiencia.value = unJugador.GetExperience();
            //this.netManager.nivell.text = unJugador.GetLevel().ToString();
        }
    }

    [PunRPC]
    public void RefreshHudTop(string mort, string assassi)
    {
        GameObject goMort = GameObject.Find(mort);
        GameObject goAssassi = GameObject.Find(assassi);
        Unidad unMort = goMort.GetComponent<Unidad>();
        Unidad unAssassi = goMort.GetComponent<Unidad>();
        unMort.SetDeaths(unMort.GetDeaths() + 1);
        unAssassi.SetAssassinations(unAssassi.GetAssassinations() + 1);

        GameObject[] playersList = GameObject.FindGameObjectsWithTag("Player");
        AssTeam1 = 0;
        AssTeam2 = 0;

        foreach (GameObject player in playersList)
        {
            CombatSystem cmbtPlayer = player.GetComponent<CombatSystem>();
            Unidad uPlayer = player.GetComponent<Unidad>();

            if (cmbtPlayer.GetTeam() == 0)
            {
                AssTeam1 += uPlayer.GetAssassinations();
            }
            else
            {
                AssTeam2 += uPlayer.GetAssassinations();
            }
        }

        netManager.playerHUD.AssMor.text = AssTeam1 + " vs " + AssTeam2;
    }


    private IEnumerator RespawnJugador(float time)
    {
        yield return new WaitForSeconds(time);
        netManager.cameraScene.SetActive(false);
        netManager.cameraMain.SetActive(true);
        netManager.playerHUD.gameObject.SetActive(true);
        this.gameObject.GetComponent<PlayerController>().SetCanMove(true);
    }

    [PunRPC]
    public void EliminarMonstreNet(string monstre)
    {
        GameObject monstreGO = GameObject.FindGameObjectWithTag(monstre);
        monstreGO.GetComponent<BoxCollider>().enabled = false;
        monstreGO.transform.position = new Vector3(0f, 0f, 0f);
        StartCoroutine(RespawnMonstre(5f, monstre));
    }

    private IEnumerator RespawnMonstre(float time, string monstre)
    {
        yield return new WaitForSeconds(time);
        GameObject gameObj = GameObject.FindGameObjectWithTag(monstre);
        PhotonNetwork.Destroy(gameObj);
        switch (monstre)
        {
            case "Blue1":
                PhotonNetwork.Instantiate(monstre, netManager.blue1.transform.position, netManager.blue1.transform.rotation, 0);
                break;
            case "Blue2":
                PhotonNetwork.Instantiate(monstre, netManager.blue2.transform.position, netManager.blue2.transform.rotation, 0);
                break;
            case "Red1":
                PhotonNetwork.Instantiate(monstre, netManager.red1.transform.position, netManager.red1.transform.rotation, 0);
                break;
            case "Red2":
                PhotonNetwork.Instantiate(monstre, netManager.red2.transform.position, netManager.red2.transform.rotation, 0);
                break;
            case "Dragon":
                PhotonNetwork.Instantiate(monstre, netManager.dragon.transform.position, netManager.dragon.transform.rotation, 0);
                break;
            case "Baron":
                PhotonNetwork.Instantiate(monstre, netManager.baron.transform.position, netManager.baron.transform.rotation, 0);
                break;
        }
    }

    [PunRPC]
    public void SetBuffNet(string monstre, string nomJugador)
    {
        GameObject gameObj = GameObject.Find(nomJugador);
        Unidad jugadorUnidad = gameObj.GetComponent<Unidad>();
        if (jugadorUnidad != null)
        {
            switch (monstre)
            {
                /*Si un campió mata a aquesta unitat guanyarà un buff que
    aportarà una bonificació a la regeneració del manà i la reducció
    de refredament. Es troba en el camp 1. Dóna 150 d’or i 150
    d’experiència.*/
                case "Blue1":
                    jugadorUnidad.SetManaRegen(jugadorUnidad.GetManaRegen() + 10);
                    jugadorUnidad.SetMaxMana(jugadorUnidad.GetMaxMana() + 100);
                    //netManager.b_Blue.gameObject.SetActive(true);
                    StartCoroutine(EliminarBuff(2f, monstre, nomJugador));
                    break;
                case "Blue2":
                    jugadorUnidad.SetManaRegen(jugadorUnidad.GetManaRegen() + 10);
                    jugadorUnidad.SetMaxMana(jugadorUnidad.GetMaxMana() + 100);
                    //netManager.b_Blue.gameObject.SetActive(true);
                    StartCoroutine(EliminarBuff(2f, monstre, nomJugador));
                    break;
                /*Si un campió mata a aquesta unitat guanyarà un buff que
    aportarà una bonificació d’atac i regeneració de la vida. Es troba
    en el camp 2. Dóna 150 d’or i 150 d’experiència.*/
                case "Red1":
                    jugadorUnidad.SetAtackDamage(jugadorUnidad.GetAtackDamage() + 70);
                    jugadorUnidad.SetHealthRegen(jugadorUnidad.GetHealthRegen() + 10);
                    //netManager.b_Red.gameObject.SetActive(true);
                    StartCoroutine(EliminarBuff(2f, monstre, nomJugador));
                    break;
                case "Red2":
                    jugadorUnidad.SetAtackDamage(jugadorUnidad.GetAtackDamage() + 70);
                    jugadorUnidad.SetHealthRegen(jugadorUnidad.GetHealthRegen() + 10);
                    //netManager.b_Red.gameObject.SetActive(true);
                    StartCoroutine(EliminarBuff(2f, monstre, nomJugador));
                    break;
                /*Quan un campió acabi amb aquesta unitat tot el seu equip rebrà
    una millora de totes les seves estadístiques. Es troba en el camp
    gran 2. Dóna 400 d’or a tot l’equip i 500 d’experiència.*/
                case "Dragon":
                        jugadorUnidad.SetAtackDamage(jugadorUnidad.GetAtackDamage() + 100);
                        jugadorUnidad.SetAbilityPower(jugadorUnidad.GetAbilityPower() + 100);
                        jugadorUnidad.SetAtackSpeed(jugadorUnidad.GetAtackSpeed() + 10);
                        jugadorUnidad.SetMovementSpeed(jugadorUnidad.GetMovementSpeed() + 5);
                        //netManager.b_Dragon.gameObject.SetActive(true);
                        StartCoroutine(EliminarBuff(2f, monstre, nomJugador));
                    break;
                /*Quan un campió acabi amb aquesta unitat tots els súbdits del
    seu equip rebran millores de totes les seves estadístiques. Es
    troba en el camp gran 1.Dóna 600 d’or a tot l’equip i 500
    d’experiència.*/

                case "Baron":
                    jugadorUnidad.SetArmour(jugadorUnidad.GetArmour() + 100);
                    jugadorUnidad.SetMagicArmour(jugadorUnidad.GetMagicArmour() + 100);
                    jugadorUnidad.SetManaRegen(jugadorUnidad.GetManaRegen() + 5);
                    jugadorUnidad.SetHealthRegen(jugadorUnidad.GetHealthRegen() + 5);
                    //netManager.b_Baron.gameObject.SetActive(true);
                    StartCoroutine(EliminarBuff(2f, monstre, nomJugador));
                    break;
            }
        }
    }

    // Els buffs dels monstres duraran 60 segons
    private IEnumerator EliminarBuff(float time, string monstre, string playerName)
    {
        Unidad jugadorUnidad = GameObject.Find(playerName).GetComponent<Unidad>();
        if (jugadorUnidad != null)
        {
            yield return new WaitForSeconds(60);
            switch (monstre)
            {
                case "Blue1":
                    jugadorUnidad.SetManaRegen(jugadorUnidad.GetManaRegen() - 10);
                    jugadorUnidad.SetMaxMana(jugadorUnidad.GetMaxMana() - 100);
                    //netManager.b_Blue.gameObject.SetActive(false);
                    break;
                case "Blue2":
                    jugadorUnidad.SetManaRegen(jugadorUnidad.GetManaRegen() - 10);
                    jugadorUnidad.SetMaxMana(jugadorUnidad.GetMaxMana() - 100);
                    //netManager.b_Blue.gameObject.SetActive(false);
                    break;
                case "Red1":
                    jugadorUnidad.SetAtackDamage(jugadorUnidad.GetAtackDamage() - 70);
                    jugadorUnidad.SetHealthRegen(jugadorUnidad.GetHealthRegen() - 10);
                    //netManager.b_Red.gameObject.SetActive(false);
                    break;
                case "Red2":
                    jugadorUnidad.SetAtackDamage(jugadorUnidad.GetAtackDamage() - 70);
                    jugadorUnidad.SetHealthRegen(jugadorUnidad.GetHealthRegen() - 10);
                    //netManager.b_Red.gameObject.SetActive(false);
                    break;
                case "Dragon":
                    jugadorUnidad.SetAtackDamage(jugadorUnidad.GetAtackDamage() - 100);
                    jugadorUnidad.SetAbilityPower(jugadorUnidad.GetAbilityPower() - 100);
                    jugadorUnidad.SetAtackSpeed(jugadorUnidad.GetAtackSpeed() - 10);
                    jugadorUnidad.SetMovementSpeed(jugadorUnidad.GetMovementSpeed() - 5);
                    //netManager.b_Dragon.gameObject.SetActive(false);
                    break;
                case "Baron":
                    jugadorUnidad.SetArmour(jugadorUnidad.GetArmour() - 100);
                    jugadorUnidad.SetMagicArmour(jugadorUnidad.GetMagicArmour() - 100);
                    jugadorUnidad.SetManaRegen(jugadorUnidad.GetManaRegen() - 5);
                    jugadorUnidad.SetHealthRegen(jugadorUnidad.GetHealthRegen() - 5);
                    //netManager.b_Baron.gameObject.SetActive(true);
                    //netManager.b_Baron.gameObject.SetActive(false);
                    break;
            }
        }
    }

    [PunRPC]
    public void EliminarTorre(int pos, int team)
    {
        pos = pos - 1;
        Torres torre;
        if (team == 0)
        {
            torre = torresTagBlau[pos].GetComponent<Torres>();
            torre.GetComponent<Renderer>().enabled = false;
            torre.GetComponent<BoxCollider>().enabled = false;
            torre.isDestroyed = true;
            pdcTorresBlau[pos].SetActive(false);
            torresBlau[pos] = false;
        }
        else
        {
            torre = torresTagVermell[pos].GetComponent<Torres>();
            torre.GetComponent<Renderer>().enabled = false;
            torre.GetComponent<BoxCollider>().enabled = false;
            torre.isDestroyed = true;
            pdcTorresVermell[pos].SetActive(false);
            torresVermell[pos] = false;
        }
    }

    [PunRPC]
    private void GameOver(int looserTeam)
    {
        netManager.GameOver(looserTeam);
    }

    private int DamageAbility(Skill skill)
    {
        if (skill.GetIsMele())
        {
            this.damageAbility = unidad.GetAtackDamage();
        }
        else
        {
            this.damageAbility = unidad.GetAbilityPower();
        }
        return this.damageAbility;
    }

    public Vector3 GetInitialPos()
    {
        return this.initialPos;
    }
}