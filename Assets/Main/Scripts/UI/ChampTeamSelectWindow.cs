using UnityEngine;
using UnityEngine.UI;

public class ChampTeamSelectWindow : MonoBehaviour {

    public Text championName, teamName;
    public int teamId;

    public void SetChampionName(string name)
    {
        this.championName.text = name;
    }

    public void SetTeam(int _teamId)
    {
        this.teamId = _teamId;
        if (this.teamId == 0)
        {
            this.teamName.text = "Equip Blau";
        }
        else
        {
            this.teamName.text = "Equip Vermell";
        }
    }
}
