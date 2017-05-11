using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomWindow : MonoBehaviour {

    public InputField roomNameField;
    public Text maxPlayersLabel;
    public int maxNumPlayers;

    public void ModifyNumPlayers(int num)
    {
        this.maxNumPlayers += num;
        if (this.maxNumPlayers < 2)
        {
            this.maxNumPlayers = 2;
        }
        if (this.maxNumPlayers > 10)
        {
            this.maxNumPlayers = 10;
        }

        this.maxPlayersLabel.text = this.maxNumPlayers.ToString();
    }

    public void CreateRoom()
    {
        string roomName = this.roomNameField.text;
        if (roomName == string.Empty)
        {
            roomName = "Partida";
        }
        NetManager.current.CreateRoom(roomName, this.maxNumPlayers);
    }
}
