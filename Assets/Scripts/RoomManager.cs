using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine.UI;
public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
    public List<RoomInfo> GGGG = new List<RoomInfo>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image load_fild;




    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }


    void Start()
    {

    }
    public void ConnectToServer()
    {
        Debug.Log("Try Connect To Server");
        PhotonNetwork.ConnectUsingSettings();
        load_fild.fillAmount = 0;
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
        Debug.Log("Connect To Server");
        load_fild.fillAmount = 0.4f;
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Join a Lobby");
        load_fild.fillAmount = 1f;

        GameManager.instance.Start_State(Game_State.Enter_Room);

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        GameManager.instance.AfterJoinRoom();
        Debug.Log("Join a Room");

    }
    public void JoinRoom(string _roomName)
    {
        PhotonNetwork.JoinRoom(_roomName);
    }
    public void CreateRoom(string _roomName)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(_roomName);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        GameManager.instance.Start_State(Game_State.Main_Menu);
    }
    public bool RoomHasCreate(string _roomName)
    {
        // เช็คว่ามีห้องที่ต้องการอยู่หรือไม่
        if (cachedRoomList.ContainsKey(_roomName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // อัปเดตลิสต์ห้อง
          Debug.Log(" Room Name");
        foreach (RoomInfo room in roomList)
        {
              Debug.Log(" Room Name --" + room.Name);
            if (room.RemovedFromList)
            {
                cachedRoomList.Remove(room.Name);
                Debug.Log("Remove Room Name");
            }
            else
            {
                cachedRoomList[room.Name] = room;
                Debug.Log("Add Room Name");
            }
        }

    }
    [ContextMenu("Showe")]
    public void Show()
    {
        foreach (var T in cachedRoomList)
        {
            Debug.Log($"FF: " + T.Value.Name);
        }
    }


}
