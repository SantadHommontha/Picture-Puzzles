using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
public class RoomManager : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
    }





}
