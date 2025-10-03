using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections.Generic;
using ExitGames.Client.Photon;




public class JoinTeamResult
{
    public bool status;
    public string report;
}
public class TeamManager : MonoBehaviourPunCallbacks
{
    private static TeamManager _instance;
    public static TeamManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<TeamManager>();

                if (_instance == null)
                {
                    _instance = new GameObject("TeamManager", typeof(TeamManager)).GetComponent<TeamManager>();

                }
            }
            return _instance;
        }
    }

    private static bool applicationIsQuitting = false;
    //[SerializeField] private int maxTeamCount = 3;
    [SerializeField] private TMP_Text report;
  //  [SerializeField] private StringValue code;
    private Team team = new Team();


    [Header("Value")]
    [SerializeField] private List<PlayerDataValue> playerDataDisplay = new List<PlayerDataValue>();

    [SerializeField] private BoolValue enterGame;

    public Team Team_Script => team;
    void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;

    }
    void Start()
    {
        //    team.OnPlayerTeamChange += SetPlayerDataToScript;
        //  team.OnPlayerTeamChange += UpdateTeamToOther;


    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
    public void JoinTeam(PlayerData _playerData)
    {
        string josnData = JsonUtility.ToJson(_playerData);

        photonView.RPC("TryJoinTeam", RpcTarget.MasterClient, josnData);
    }
    private void SetPlayerDataToScript()
    {
        var allplayer = team.GetAllPlayer();
        int num = 0;
        Debug.Log("--------------------------------");
        ClearPlayerDataScript();
        foreach (var T in allplayer)
        {
            Debug.Log(T.playerName);
            var pd = new playerDataDisplay();
            pd.player_id = T.playerID;
            pd.player_name = T.playerName;
            playerDataDisplay[num].SetValue(pd);
            num++;
        }
    }
    private void ClearPlayerDataScript()
    {
        int num = 0;
        foreach (var T in playerDataDisplay)
        {
            var pd = new playerDataDisplay();
            pd.player_id = "";
            pd.player_name = "";
            T.SetValue(pd);
            num++;
        }
    }
    [PunRPC]
    private void TryJoinTeam(string _playerData, PhotonMessageInfo _info)
    {
        //    Debug.Log(_playerData);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(_playerData); //แปลงกลัย
        playerData.info = _info;
        JoinTeamResult joinTeamResult = new JoinTeamResult(); // สร้างตัวตอบส่งคำตอบ

        var player = team.GetPlayerByID(playerData.playerID);
        if (player != null)
        {
            joinTeamResult.report = "Have Player ID In Game";
            joinTeamResult.status = false;
        }
        else
        {
            joinTeamResult.report = "Add Player";
            joinTeamResult.status = true;
            team.AddPlayer(playerData);
        //    SetPlayerDataToScript();

        }
        RoomData.Instance.playerDatas = team.GetAllPlayer().ToArray();
        var jsonData = JsonUtility.ToJson(joinTeamResult);
        photonView.RPC("JoinTeamResult", _info.Sender, jsonData);


    }
    [PunRPC]
    private void JoinTeamResult(string _result)
    {
        JoinTeamResult joinTeamResult = JsonUtility.FromJson<JoinTeamResult>(_result);
        report.text = joinTeamResult.report;
        if (joinTeamResult.status)
        {
            GameManager.Instance.StartState(Game_State.Wait_For_Play);
        }
        else
        {
            report.text = joinTeamResult.report;
        }

    }

    private void LeaveAll()
    {
        foreach (var T in team.GetAllPlayer())
        {
            Kick(T.playerID);
        }
    }

    [ContextMenu("Show")]
    private void Log()
    {
        foreach (var T in team.GetAllPlayer())
        {
            Debug.Log($"P: " + T.playerName);
        }
    }

    public void Kick(string _playerID)
    {
        var data = team.GetPlayerByID(_playerID);
        team.RemovePlayer(_playerID);

        SetPlayerDataToScript();
        photonView.RPC("Leave", data.info.Sender);

    }
    [PunRPC]
    private void Leave()
    {
        RoomManager.Instance.LeaveRoom();
    }


    // [PunRPC]
    // private void GoToChooseTeam()
    // {
    //     chooseTeam_canvas.SetActive(true);
    //     play_canvas.SetActive(false);
    //     end_canvas.SetActive(false);
    //     enterGame.Value = false;

    // }



    private void UpdateTeamToOther()
    {
        //     if (!PhotonNetwork.IsMasterClient) return;
        //     TeamsWrapper teamsWrapper = new TeamsWrapper(team.GetAllPlayer());

        //     ExitGames.Client.Photon.Hashtable playerdata = new ExitGames.Client.Photon.Hashtable()
        // {
        //     {RoomPropertiesName.TeamData,JsonUtility.ToJson(teamsWrapper)}
        // };

        //     PhotonNetwork.CurrentRoom.SetCustomProperties(playerdata);
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        CheckRoomProperties();
    }
    private void CheckRoomProperties()
    {
        // if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(RoomPropertiesName.TeamData))
        // {

        //     TeamsWrapper teamsWrapper = JsonUtility.FromJson<TeamsWrapper>((string)PhotonNetwork.CurrentRoom.CustomProperties[RoomPropertiesName.TeamData]);

        //     if (PhotonNetwork.IsMasterClient) return;
        //     team.ClearAll();

        //     foreach (var T in teamsWrapper.playerDatas)
        //     {
        //         team.AddPlayer(T);
        //     }
        // }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {

        //  Debug.Log((string)propertiesThatChanged[RoomPropertiesName.TeamData]);
        //  CheckRoomProperties();

        // if (propertiesThatChanged.ContainsKey(RoomPropertiesName.TeamData))
        // {

        //     TeamsWrapper teamsWrapper = JsonUtility.FromJson<TeamsWrapper>((string)propertiesThatChanged[RoomPropertiesName.TeamData]);

        //     if (PhotonNetwork.IsMasterClient) return;
        //     team.ClearAll();

        //     foreach (var T in teamsWrapper.playerDatas)
        //     {
        //         team.AddPlayer(T);
        //     }
        // }
    }




}

[System.Serializable]
public class TeamsWrapper
{
    public List<PlayerData> playerDatas;
    public TeamsWrapper(List<PlayerData> _playerDatas)
    {
        this.playerDatas = _playerDatas;
    }
}
