using Photon.Pun;
using TMPro;
using UnityEngine;

public class EnterNameBtn : MonoBehaviour
{
    [SerializeField] private TMP_InputField enterName;


    public void EnterName()
    {
        Debug.Log("EnterName");
        PlayerData playerData = new PlayerData();
        playerData.playerName = enterName.text;
        playerData.playerID = PhotonNetwork.LocalPlayer.UserId;
        TeamManager.Instance.JoinTeam(playerData);
    }



}
