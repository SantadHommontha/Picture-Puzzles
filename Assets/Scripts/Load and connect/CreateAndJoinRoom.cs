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
    [SerializeField] private string adminCode = "mine";
    [SerializeField] private BoolValue isAdmin;
    // [SerializeField] GameObject red;
    // [SerializeField] private GameObject green;
    public bool useToCrateRoom;
    [HideInInspector] public bool createNewRoom = false;
    void Start()
    {
        // PhotonNetwork.AutomaticallySyncScene = true;
        // PhotonNetwork.EnableCloseConnection = false;
        // PhotonNetwork.NetworkingClient.LoadBalancingPeer.SerializationProtocolType = ExitGames.Client.Photon.SerializationProtocol.GpBinaryV16;
        // PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "asia";
        // PhotonNetwork.ConnectUsingSettings();
        if (useToCrateRoom) return;
        PhotonNetwork.IsMessageQueueRunning = true;
        if (!PhotonNetwork.IsConnected)
        {
            ChangeMassage("Your Not Connect The Server", false);
            PhotonNetwork.IsMessageQueueRunning = false;
            SceneManager.LoadScene("Loading");
        }
        else
        {

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
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        if (createNewRoom)
        {
            createNewRoom = false;
            Debug.Log("CreateNewRoom");
            RoomOptions options = new RoomOptions();
            options.IsVisible = true;
            options.IsOpen = true;
            options.MaxPlayers = 5;
            RoomData.Instance.isAdmin = true;
            RoomData.Instance.roomCode = GenerateCode.GenerateRandomCode();
            PhotonNetwork.CreateRoom(RoomData.Instance.roomCode.ToLower(), options);
        }
    }

    public void CreateRoom()
    {
        Debug.Log("CreateRoom F");
        RoomOptions options = new RoomOptions();
        options.IsVisible = true;
        options.IsOpen = true;
        options.MaxPlayers = 5;
        RoomData.Instance.isAdmin = true;
        RoomData.Instance.roomCode = GenerateCode.GenerateRandomCode();
        PhotonNetwork.CreateRoom(RoomData.Instance.roomCode.ToLower(), options);
    }
    public void CreateNewRoom()
    {
        createNewRoom = true;
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {


    }
    public void JoinRoom()
    {
        Debug.Log("JoinRoom F");
        RoomData.Instance.isPlayer = true;
        PhotonNetwork.JoinRoom(roomInput.text.ToLower());
    }


    public void EnterRoomBTN()
    {
        RoomData.Instance.Reset();
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
        PhotonNetwork.IsMessageQueueRunning = false;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_scenename);
    }
}
