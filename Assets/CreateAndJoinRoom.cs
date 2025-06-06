using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField createInput;
    [SerializeField] private TMP_InputField joinInput;
    [SerializeField] GameObject red;
    [SerializeField] private GameObject green;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.EnableCloseConnection = false;
        PhotonNetwork.NetworkingClient.LoadBalancingPeer.SerializationProtocolType = ExitGames.Client.Photon.SerializationProtocol.GpBinaryV16;
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "asia";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    public void CreateRoom()
    {
        Debug.Log("Creating Room: " + createInput.text);
        RoomOptions options = new RoomOptions();
        options.IsVisible = true;
        options.IsOpen = true;
        options.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(createInput.text, options);
    }

    public void JoinRoom()
    {
        Debug.Log("Joining Room: " + joinInput.text);
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Successfully Joined Room: " + PhotonNetwork.CurrentRoom.Name);
        green.SetActive(true);
        red.SetActive(false);
    }
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnCreateRoomFailed(short code, string message)
    {
        Debug.LogError($"❌ Create Room Failed: {message} ({code})");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Join Room Failed: {message} (Code: {returnCode})");
        red.SetActive(true);
        green.SetActive(false);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError($"Join Room Failed: {message} (Code: {returnCode})");
    }

}
