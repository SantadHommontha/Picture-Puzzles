
using UnityEngine;

public static class RoomData
{
    public static string roomCode;
    public static bool isAdmin;
    public static bool isPlayer;
    public static PlayerData[] playerDatas;


    public static void Reset()
    {
        roomCode = "";
        isAdmin = false;
        isPlayer = false;
        playerDatas = null;
    }
  
}
