using Unity.VisualScripting;
using UnityEngine;

public class DebugRoomData : MonoBehaviour
{
    public string roomCode;
    public bool isAdmin;
    public bool isPlayer;
    public PlayerData[] playerDatas;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SetValue();
    }

    private void SetValue()
    {
        roomCode = RoomData.Instance.roomCode;
        isAdmin = RoomData.Instance.isAdmin;
        isPlayer = RoomData.Instance.isPlayer;
        PlayerData[] playerDatas = RoomData.Instance.playerDatas;
    }
}
