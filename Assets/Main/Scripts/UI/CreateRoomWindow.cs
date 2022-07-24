using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomWindow : Window {

    public InputField roomNameField;
    public Text maxPlayersLabel, msgError;
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

        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        if (rooms.GetLength(0) == 0)
        {
            if (roomName == string.Empty)
            {
                this.msgError.text = "Posa-li un nom a la partida.";
            }
            else
            {
                this.msgError.text = "";
                NetManager.current.CreateRoom(roomName, this.maxNumPlayers);
            }
        }
        else
        {
            foreach (RoomInfo room in rooms)
            {
                if (room.name == roomName)
                {
                    this.msgError.text = "Ja hi ha una partida amb aquest nom.";
                }
                else
                {
                    NetManager.current.CreateRoom(roomName, this.maxNumPlayers);
                }
            }
        }
    }
}
