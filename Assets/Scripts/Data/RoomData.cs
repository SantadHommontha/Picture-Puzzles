
using UnityEngine;

[System.Serializable]
public class RoomDataWapper
{
    public bool gameStart;
    public float gameTime = 5f;
    public float timer;
    public string aswer;
}
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
    private bool applicationIsQuitting = false;
    public string roomCode;
    public bool isAdmin;
    public bool isPlayer;
    public PlayerData[] playerDatas;
    public bool gameStart;
    public float gameTime = 5f;
    public float timer;
    public string aswer;

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
    public void Defualt()
    {
        timer = 0f;
        gameStart = false;
    }
    public void Reset()
    {
        roomCode = "";
        isAdmin = false;
        isPlayer = false;
        playerDatas = null;
        gameStart = false;
        // gameTime = 0f;
        aswer = "";
        timer = 0f;
    }

}
