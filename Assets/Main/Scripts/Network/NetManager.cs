using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NetManager : MonoBehaviour
{
    public static NetManager current;

    [Header("Nexus")]
    public GameObject nexusBlau;
    public GameObject nexusVermell;

    [Header("Monsters")]
    public GameObject baron;
    public GameObject dragon;
    public GameObject blue1;
    public GameObject blue2;
    public GameObject red1;
    public GameObject red2;

    [Header("Altres")]
    public GameObject cameraScene;
    public GameObject cameraMain;
    public Text connectionState;

    [Header("Windows")]
    public LoginWindow loginWindow;
    public LobbyWindow lobbyWindow;
    public CreateRoomWindow createRoomWindow;
    public ChampTeamSelectWindow champTeamSelectWindow;
    public VictoryWindow victoryWindow;
    public PlayerHUD playerHUD;
    public RegisterWindow registerWindow;
    public ShopManager shopManager;
    public StatsWindow statsWindow;

    [Header("Game Version")]
    public string version;

    [Header("PhotonView")]
    public PhotonView pv;

    [Header("Experiència")]
    public Slider experiencia;
    public Text nivell;

    /*[Header("Buffs")]
    public Image b_Skill;
    public Image b_Blue;
    public Image b_Red;
    public Image b_Dragon;
    public Image b_Baron;*/

    private string playerPrefabName;
    private Transform[] spawnPoints;
    private float timeToRespawn;
    private float time;
    private string playerName;
    private int playerTeam;
    private int playerNum;
    private Unidad jugador;
    private bool entrar = true;
    private bool partidaIniciada;
    private int playersReady;
    private PhotonView pvPlayer;

    private string temps;
    private string strSeconds, strMinutes;
    private float seconds;
    private float minutes;
    private float hours;

    private string api = "http://localhost:8000/api";

    // Use this for initialization
    void Start()
    {
        int i;
        current = this;
        this.playersReady = 0;
        GameObject sp = GameObject.FindGameObjectWithTag("Respawn");

        this.spawnPoints = new Transform[sp.transform.childCount];

        for (i = 0; i < sp.transform.childCount; i++)
        {
            this.spawnPoints[i] = sp.transform.GetChild(i);
        }
        this.timeToRespawn = 5;
        this.time = this.timeToRespawn;

        this.loginWindow.gameObject.SetActive(true);
        this.playerHUD.gameObject.SetActive(false);
    }

    void OnJoinedLobby()
    {
        // Si falla saltarà a "OnJoinedRoomFailed()"
        //PhotonNetwork.JoinRoom("Partida1");
        this.lobbyWindow.gameObject.SetActive(true);
        this.victoryWindow.gameObject.SetActive(false);
        PhotonNetwork.player.NickName = this.playerName;
    }

    void OnJoinedRoom()
    {
        this.createRoomWindow.gameObject.SetActive(false);
        this.lobbyWindow.gameObject.SetActive(false);
        this.champTeamSelectWindow.gameObject.SetActive(true);
        this.playerNum = PhotonNetwork.playerList.Length - 1;

        this.champTeamSelectWindow.SetTeam(0);
        if (this.playerNum % 2 == 0)
        {
            this.champTeamSelectWindow.SetTeam(0);
        }
        else
        {
            this.champTeamSelectWindow.SetTeam(1);
        }

        if (PhotonNetwork.player.IsMasterClient)
        {
            GameObject nexusBlauGO = PhotonNetwork.Instantiate("NexusBlau", this.nexusBlau.transform.position, this.nexusBlau.transform.rotation, 0);
            GameObject nexusVermellGO = PhotonNetwork.Instantiate("NexusVermell", this.nexusVermell.transform.position, this.nexusVermell.transform.rotation, 0);
            //GameObject dragonGO = PhotonNetwork.Instantiate("Dragon", this.dragon.transform.position, this.dragon.transform.rotation, 0);

            // MONSTERS
            PhotonNetwork.Instantiate("Baron", this.baron.transform.position, this.baron.transform.rotation, 0);
            PhotonNetwork.Instantiate("Dragon", this.dragon.transform.position, this.dragon.transform.rotation, 0);
            PhotonNetwork.Instantiate("Blue1", this.blue1.transform.position, this.blue1.transform.rotation, 0);
            PhotonNetwork.Instantiate("Blue2", this.blue2.transform.position, this.blue2.transform.rotation, 0);
            PhotonNetwork.Instantiate("Red1", this.red1.transform.position, this.red1.transform.rotation, 0);
            PhotonNetwork.Instantiate("Red2", this.red2.transform.position, this.red2.transform.rotation, 0);

            Vector3[] blueTowerPositions = GetComponent<Torres>().GetBlueTowerPositions();
            Vector3[] redTowerPositions = GetComponent<Torres>().GetRedTowerPositions();
            Quaternion rotation1 = new Quaternion(0f, -180f, 0f, 0f);
            Quaternion rotation2 = new Quaternion(0f, 0f, 0f, 0f);
            int iB = 0, iV = 0;
            foreach (Vector3 pos in blueTowerPositions)
            {
                GameObject torre = PhotonNetwork.Instantiate("TorreBlau", pos, rotation1, 0);
                torre.name = "TorreBlau" + iB;
                iB++;
            }
            foreach (Vector3 pos in redTowerPositions)
            {
                GameObject torre = PhotonNetwork.Instantiate("TorreVermell", pos, rotation2, 0);
                torre.name = "TorreVermell" + iV;
                iV++;
            }
        }
    }

    void OnPhotonJoinRoomFailed()
    {
        //PhotonNetwork.CreateRoom("Partida1");
    }

    public void PlayerDeath()
    {
        this.cameraScene.SetActive(true);
        this.cameraMain.SetActive(false);
        this.playerHUD.gameObject.SetActive(false);
        this.time = 0;
    }

    void SpawnPlayer()
    {
        Vector3 position = this.spawnPoints[playerNum].position;
        object[] data;

        this.cameraScene.SetActive(false);
        this.cameraMain.SetActive(true);

        data = new object[2];
        data[0] = this.playerName;
        data[1] = this.playerTeam;

        GameObject gameObject = PhotonNetwork.Instantiate(playerPrefabName, position, Quaternion.identity, 0, data);
        gameObject.name = this.playerName;
        jugador = gameObject.GetComponent<Unidad>();

        this.playerHUD.gameObject.SetActive(true);
    }

    public void GameOver(int looserTeam)
    {
        this.victoryWindow.gameObject.SetActive(true);
        if (looserTeam == 0)
        {
            this.victoryWindow.victoryText.text = "HA GUANYAT L'EQUIP VERMELL!";
        }
        else
        {
            this.victoryWindow.victoryText.text = "HA GUANYAT L'EQUIP BLAU!";
        }

        foreach (PhotonPlayer jugador in PhotonNetwork.playerList)
        {
            PhotonNetwork.DestroyPlayerObjects(jugador);
        }
        PhotonNetwork.LeaveRoom();
        this.createRoomWindow.gameObject.SetActive(false);
        this.playerHUD.gameObject.SetActive(false);
        this.champTeamSelectWindow.gameObject.SetActive(false);
        this.lobbyWindow.gameObject.SetActive(true);
    }

    void Update()
    {
        if (PhotonNetwork.room != null)
        {
            if (PhotonNetwork.room.MaxPlayers == PhotonNetwork.room.PlayerCount && !this.partidaIniciada)
            {
                GameObject[] jugadors = GameObject.FindGameObjectsWithTag("Jugador");
                this.playersReady = jugadors.Length;
                
                
                if (this.playersReady >= PhotonNetwork.room.MaxPlayers)
                {
                    this.partidaIniciada = true;
                    this.champTeamSelectWindow.gameObject.SetActive(false);
                    this.SpawnPlayer();
                }
            }
        }
        this.connectionState.text = PhotonNetwork.connectionStateDetailed.ToString();


        if (this.partidaIniciada)
        {
            seconds += Time.deltaTime;

            if (seconds >= 60)
            {
                minutes += 1;
                seconds = 0;
            }

            if (minutes >= 60)
            {
                hours += 1;
                minutes = 0;
            }
            if ((int)seconds < 10) strSeconds = "0" + (int)seconds;
            else strSeconds = "" + (int)seconds;
            if (minutes < 10) strMinutes = "0" + minutes;
            else strMinutes = "" + minutes;

            temps = strMinutes + ":" + strSeconds;
            playerHUD.timer.text = temps;
        }



        /////////////////////////////////////REFRESH/////////////////////////////////////
        //timeRefresh += 1 * Time.deltaTime;
        //Debug.Log(timeRefresh);
        //if (timeRefresh % 2 == 0)
        //{
        //    this.lobbyWindow.RefreshRoomList();
        //}
        //al entrar a la room refrescar 1 cop primera vegada
        /*if (this.lobbyWindow.gameObject.GetActive() == true && entrar)
        {
           
            entrar = false;
        }*/


        /////////////////////////////////////////////////////////////////////////////////////


        //partida inicida
        /*Debug.Log("FORA Partida iniciada");
        if (this.partidaIniciada)
        {
            Debug.Log("Partida iniciada");
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString();

            string[] secondsArr = seconds.Split('.');
            if (minutes.Length < 2)
            {
                minutes = "0" + minutes;
            }
            if (secondsArr[0].Length < 2)
            {
                secondsArr[0] = "0" + secondsArr[0];
            }
            pv.RPC("TotalTimer", PhotonTargets.All, minutes, secondsArr[0]);
            this.playerHUD.timer.text = mins + " : " + secs;
            Debug.Log(mins + " : " + secs);
        }*/

    }    

    public Unidad GetJugador()
    {
        return this.jugador;
    }

    // Gestió de finestres
    public void SetChampTeam()
    {
        if (this.champTeamSelectWindow.championName != null)
        {
            if (this.champTeamSelectWindow.championName.text != "Selecciona campió")
            {
                this.playerPrefabName = this.champTeamSelectWindow.championName.text;
                this.playerTeam = this.champTeamSelectWindow.teamId;

                // Enviar nom del jugador per posar el nom als objectes de les còpies en xarxa
                object[] data = new object[2];
                data[0] = this.playerName;
                data[1] = true;

                if (pvPlayer != null)
                {
                    pvPlayer.RPC("RemovePlayerNet", PhotonTargets.All, "J-" + this.playerName);
                }
                GameObject jugadorGO = PhotonNetwork.Instantiate("Jugador", new Vector3(0, 0, 0), new Quaternion(), 0, data);
                jugadorGO.name = "J-" + this.playerName;
                pvPlayer = jugadorGO.GetComponent<PhotonView>();
                this.champTeamSelectWindow.errorSelect.text = "Esperant resta de jugadors";


                // TODO CANVIAAAAR
                //this.champTeamSelectWindow.gameObject.SetActive(false);
               // this.SpawnPlayer();



            }
            else
            {
                this.champTeamSelectWindow.errorSelect.text = "Has de seleccionar un campió";
            }
        }
        else
        {
            this.champTeamSelectWindow.errorSelect.text = "Error a la selecció de campió";
        }
    }

    public void Play()
    {
        this.loginWindow.gameObject.SetActive(false);
        PhotonNetwork.ConnectUsingSettings(this.version);
    }

    public void CreateRoom()
    {
        this.lobbyWindow.gameObject.SetActive(false);
        this.createRoomWindow.gameObject.SetActive(true);
    }

    public void CancelCreateRoom()
    {
        this.lobbyWindow.gameObject.SetActive(true);
        this.createRoomWindow.gameObject.SetActive(false);
    }

    public void CreateRoom(string roomName, int maxPlayers)
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = (byte)maxPlayers;
        PhotonNetwork.CreateRoom(roomName, ro, null);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
        this.playerHUD.gameObject.SetActive(false);
        this.cameraMain.SetActive(false);
        this.cameraScene.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
        this.createRoomWindow.gameObject.SetActive(false);
        this.lobbyWindow.gameObject.SetActive(false);
        this.loginWindow.gameObject.SetActive(true);
    }
    public void DisconnectGame()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
        PhotonNetwork.LeaveRoom();
        this.createRoomWindow.gameObject.SetActive(false);
        this.playerHUD.gameObject.SetActive(false);
        this.champTeamSelectWindow.gameObject.SetActive(false);
        this.lobbyWindow.gameObject.SetActive(true);
    }

    public void BackLobby()
    {
        PhotonNetwork.LeaveRoom();
        this.champTeamSelectWindow.gameObject.SetActive(false);
        this.lobbyWindow.gameObject.SetActive(true);
    }

    public void SignIn()
    {
        //Recuperar datos de inputs
        string nickname = loginWindow.user.text;
        string password = loginWindow.password.text;

        // Enviar dades del formulari al WebService
        string url = api + "/auth/login";
        Debug.Log(url);
        Debug.Log("nickname: " + nickname);
        Debug.Log("password: " + password);

        WWWForm form = new WWWForm();
        form.AddField("nickname", nickname);
        form.AddField("password", password);
        WWW www = new WWW(url, form);

        // Executar Coroutine per esperar resposta del servidor
        WaitForLogin(www);
        //StartCoroutine(WaitForLogin(www));
    }

    public void BackLogin()
    {
        this.registerWindow.gameObject.SetActive(false);
        this.loginWindow.gameObject.SetActive(true);
    }

    public void ShowSignUp()
    {
        this.loginWindow.gameObject.SetActive(false);
        this.registerWindow.gameObject.SetActive(true);
        
    }

    public void SignUp()
    {
        registerWindow.error.color = Color.red;
        string username = registerWindow.user.text;
        string password = registerWindow.password.text;
        string password_confirmation = registerWindow.confpass.text;

        // Comprovar número de caràcters del nom d'usuari
        if (username.Length > 4 || username.Length < 12)
        {
            // Comprovar que les dues contrasenyes són iguals
            if (!password.Equals(password_confirmation))
            {
                registerWindow.error.text = "Les contrasenyes no són iguals";
            }
            else
            {
                // Comprovar número de caràcters de la contrasenya
                if (password.Length < 6 || password_confirmation.Length >= 12)
                {
                    registerWindow.error.text = "La contrasenya ha de tenir entre 6 i 12 caràcters";
                }
                else
                {
                    // Enviar dades del formulari al WebService
                    string url = api + "/auth/register";

                    WWWForm form = new WWWForm();
                    form.AddField("nickname", username);
                    form.AddField("password", password);
                    WWW www = new WWW(url, form);

                    // Executar Coroutine per esperar resposta del servidor
                    StartCoroutine(WaitForRegister(www));
                }
            }
        }
        else
        {
            registerWindow.error.text = "El nom d'usuari ha de tenir entre 4 i 12 caràcters";
        }
    }

    void WaitForLogin(WWW www)
    {
        Debug.Log("WaitForLogin");
        /*yield return www;
        Debug.Log(www.error);

        // Comprova que ha pogut enviar les dades al servidor
        if (www.error == null)
        {
            Login login = JsonUtility.FromJson<Login>(www.data);

            // Si el login es correcte passarà a la següent pantalla
            if (login.success == 1)
            {*/
                playerName = loginWindow.user.text;
                lobbyWindow.txtBenvinguda.text = "Benvingut, " + playerName;
                Play();
   /*         }
            else
            {
                loginWindow.error.text = login.message;
                loginWindow.user.text = "";
                loginWindow.password.text = "";
            }
        }
        else
        {
            loginWindow.error.text = "Hi ha hagut un error al connectar al servidor!";
        }*/
    }

    IEnumerator WaitForRegister(WWW www)
    {
        yield return www;

        // Comprova que ha pogut enviar les dades al servidor
        if (www.error == null)
        {
            Login login = JsonUtility.FromJson<Login>(www.data);

            // Si el registre es correcte mostrarà un missatge per pantalla
            if (login.success == 1)
            {
                registerWindow.error.text = login.message;
                registerWindow.error.color = Color.green;
            }
            else
            {
                registerWindow.error.text = login.message;
                registerWindow.user.text = "";
                registerWindow.password.text = "";
                registerWindow.confpass.text = "";
            }
        }
        else
        {
            registerWindow.error.text = "Hi ha hagut un error al connectar al servidor.";
        }
    }
}