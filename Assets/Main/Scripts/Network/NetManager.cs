using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetManager : MonoBehaviour {
    public static NetManager current;

    [Header("Nexus")]
    public GameObject nexusBlau;
    public GameObject nexusVermell;

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

    [Header("Game Version")]
    public string version;

    private string playerPrefabName;
    private Transform[] spawnPoints;
    private float timeToRespawn;
    private float time;
    private string playerName;
    private int playerTeam;
    private int playerNum;
    private bool partidaIniciada;
    private float startTime;

    // Use this for initialization
    void Start ()
    {
        int i;
        current = this;
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
        playerName = "Jugador";
        PhotonNetwork.player.NickName = this.playerName;
    }

    void OnJoinedRoom()
    {
        this.createRoomWindow.gameObject.SetActive(false);
        this.lobbyWindow.gameObject.SetActive(false);
        this.champTeamSelectWindow.gameObject.SetActive(true);
        this.playerNum = PhotonNetwork.playerList.Length - 1;

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
            PhotonNetwork.Instantiate("NexusBlau", this.nexusBlau.transform.position, this.nexusBlau.transform.rotation, 0);
            PhotonNetwork.Instantiate("NexusVermell", this.nexusVermell.transform.position, this.nexusVermell.transform.rotation, 0);
            /*
            GameObject[] structuresBlau = GameObject.FindGameObjectsWithTag("StructureBlau");
            GameObject[] structuresVermell = GameObject.FindGameObjectsWithTag("StructureVermell");

            foreach (GameObject structure in structuresBlau)
            {
                structure.GetComponent<CombatSystem>().SetTeam(0);
            }
            foreach (GameObject structure in structuresVermell)
            {
                structure.GetComponent<CombatSystem>().SetTeam(1);
            }*/
        }

        /*GameObject structuresBlau = GameObject.FindGameObjectWithTag("StructureBlau");
        structuresBlau.GetComponent<CombatSystem>().SetTeam(0);
        GameObject structuresVermell = GameObject.FindGameObjectWithTag("StructureVermell");
        structuresVermell.GetComponent<CombatSystem>().SetTeam(1);*/
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

        PhotonNetwork.Instantiate(playerPrefabName, position, Quaternion.identity, 0, data);

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
    }

    void Update () {
        this.connectionState.text = PhotonNetwork.connectionStateDetailed.ToString();
        if (this.time < this.timeToRespawn)
        {
            this.time += Time.deltaTime;
            if (this.time >= this.timeToRespawn)
            {
                this.SpawnPlayer();
            }
        }
        if (this.partidaIniciada)
        {
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
            this.playerHUD.timer.text = minutes + " : " + secondsArr[0];
        }
    }

    // Gestió de finestres
    public void SetChampTeam()
    {
        this.playerPrefabName = this.champTeamSelectWindow.championName.text;
        this.playerTeam = this.champTeamSelectWindow.teamId;
        this.champTeamSelectWindow.gameObject.SetActive(false);
        this.SpawnPlayer();
        // INICIA PARTIDA
        partidaIniciada = true;
        this.startTime = Time.time;
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

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
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
}
