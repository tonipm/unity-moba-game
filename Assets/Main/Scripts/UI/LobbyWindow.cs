using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyWindow : Window
{

    public GameObject roomListPrefab;
    public RectTransform listPanel;
    private List<GameObject> actualRooms;
    public Text txtBenvinguda;

    private void Start()
    {
        //this.actualRooms = new List<GameObject>();
    }

    public void RefreshRoomList()
    {
        this.actualRooms = new List<GameObject>();
        Debug.Log("Refresh");
        // Netejar llista de sales
        if (this.actualRooms != null)
        {
            if (this.actualRooms.Count > 0)
            {
                foreach (GameObject room in this.actualRooms)
                {
                    Destroy(room);
                }
                this.actualRooms.Clear();
            }
        }



        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        int j = 100;

        foreach (RoomInfo room in rooms)
        {
            GameObject roomGO = Instantiate(this.roomListPrefab) as GameObject;
            RectTransform roomRT = roomGO.GetComponent<RectTransform>();
            RoomData rd = roomGO.GetComponent<RoomData>();

            roomRT.parent = this.listPanel;
            roomRT.anchoredPosition = new Vector2(0, j);
            roomRT.localScale = Vector3.one;

            rd.roomName.text = room.Name;
            rd.numPlayers.text = room.PlayerCount + "/" + room.MaxPlayers;
            rd.btnEntrar.onClick.AddListener( () => NetManager.current.JoinRoom(room.Name) );

            actualRooms.Add(roomGO);
            j -= 40;
        }
    }
}
