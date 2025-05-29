using Photon.Pun;
using TMPro;

using UnityEngine;


public class TeamJoinHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField codeInput;

    [SerializeField] private StringValue myName;
    [SerializeField] private StringValue teamName;

    [SerializeField] private GameObject redTeamUI;
    [SerializeField] private GameObject blueTeamUI;
    // เรียกใช้โดยตัว UI Buttom
    public void FirstTeam()
    {
      //  JoinTeam(TeamName.FirstTeam);
        redTeamUI.SetActive(true);
        blueTeamUI.SetActive(false);
    }
    // เรียกใช้โดยตัว UI Buttom
    public void SecondTeam()
    {
        //JoinTeam(TeamName.SecondTeam);
        redTeamUI.SetActive(false);
        blueTeamUI.SetActive(true);
    }

    private void JoinTeam(string _teamName)
    {
        PlayerData playerData = new PlayerData();

        playerData.playerName = nameInput.text;
        playerData.code = codeInput.text;
        playerData.teamName = _teamName;
        playerData.playerID = PhotonNetwork.LocalPlayer.UserId;
        myName.Value = nameInput.text;
        teamName.Value = _teamName;
        TeamManager.instance.JoinTeam(playerData);
    }
}



