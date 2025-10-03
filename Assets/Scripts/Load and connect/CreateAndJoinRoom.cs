using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Collections;
using UnityEngine.SceneManagement;
public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    //  [SerializeField] private TMP_InputField createInput;
    [SerializeField] private TMP_InputField roomInput;
    [SerializeField] private TMP_Text massage;
    [SerializeField] private string adminCode = "onion";
    // [SerializeField] GameObject red;
    // [SerializeField] private GameObject green;

    void Start()
    {
        // PhotonNetwork.AutomaticallySyncScene = true;
        // PhotonNetwork.EnableCloseConnection = false;
        // PhotonNetwork.NetworkingClient.LoadBalancingPeer.SerializationProtocolType = ExitGames.Client.Photon.SerializationProtocol.GpBinaryV16;
        // PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "asia";
        // PhotonNetwork.ConnectUsingSettings();
        if (!PhotonNetwork.IsConnected)
        {
            ChangeMassage("Your Not Connect The Server", false);
        }
    }



    private void ChangeMassage(string _text, bool _clearMassage = true)
    {
        if (massage)
        {
            massage.text = _text;
            if (_clearMassage) StartCoroutine(CountDownForClearMassage());
        }
    }
    private IEnumerator CountDownForClearMassage()
    {
        yield return new WaitForSeconds(3);
        ChangeMassage("");
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
        RoomOptions options = new RoomOptions();
        options.IsVisible = true;
        options.IsOpen = true;
        options.MaxPlayers = 5;
        RoomData.isAdmin = true;
        RoomData.roomCode = GenerateCode.GenerateRandomCode();
        PhotonNetwork.CreateRoom(RoomData.roomCode.ToLower(), options);
    }

    public void JoinRoom()
    {
        RoomData.isPlayer = true;
        PhotonNetwork.JoinRoom(roomInput.text.ToLower());
    }


    public void EnterRoomBTN()
    {
        if (roomInput.text.ToLower() == adminCode)
        {
            CreateRoom();
        }
        else
        {
            JoinRoom();
        }
    }
    public override void OnCreateRoomFailed(short code, string message)
    {
        ChangeMassage($"Create Room Failed");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ChangeMassage($"Join Room Failed");
    }

    public override void OnCreatedRoom()
    {
        ChangeMassage("Create Room");
        StartCoroutine(CountDownToLoadScene("Game"));
    }

    public override void OnJoinedRoom()
    {
        ChangeMassage("Joined Room");
        StartCoroutine(CountDownToLoadScene("Game"));
    }


    private IEnumerator CountDownToLoadScene(string _scenename)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_scenename);
    }
}
