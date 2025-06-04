using Photon.Pun;
using TMPro;

using UnityEngine;


public class TeamJoinHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;

    [SerializeField] private StringValue myName;

    // เรียกใช้โดยตัว UI Buttom

    // Call With UI Btn
    public void JoinTeam()
    {
        PlayerData playerData = new PlayerData();
        myName.Value = nameInput.text;
        playerData.playerName = myName.Value;
        playerData.playerID = PhotonNetwork.LocalPlayer.UserId;

        TeamManager.instance.JoinTeam(playerData);
    }
}



