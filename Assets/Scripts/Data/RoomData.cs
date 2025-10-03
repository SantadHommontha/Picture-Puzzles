
using UnityEngine;

public class RoomData : MonoBehaviour
{

    private static RoomData _instance;
    public static RoomData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<RoomData>();

                if (_instance == null)
                {
                    _instance = new GameObject("RoomData", typeof(RoomData)).GetComponent<RoomData>();
                }
            }
            return _instance;
        }
    }
    private  bool applicationIsQuitting = false;
    public  string roomCode;
    public  bool isAdmin;
    public  bool isPlayer;
    public  PlayerData[] playerDatas;

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;


        DontDestroyOnLoad(this.gameObject);
    }
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public  void Reset()
    {
        roomCode = "";
        isAdmin = false;
        isPlayer = false;
        playerDatas = null;
    }

}
