using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Unity.VisualScripting;



public class JoinTeamResult
{
    public bool complete;
    public string report;
}
public class TeamManager : MonoBehaviourPunCallbacks
{
    public static TeamManager instance;
    [SerializeField] private int maxTeamCount = 3;
    [SerializeField] private TMP_Text report;
    [SerializeField] private StringValue code;
    private Team team = new Team();


    [SerializeField] private GameObject chooseTeam_canvas;
    [SerializeField] private GameObject play_canvas;

    [SerializeField] private GameObject end_canvas;


    [SerializeField] private GameObject control_canvas;



    [SerializeField] private BoolValue enterGame;

    public Team Team_Script => team;
    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }
    void Start()
    {
        team.OnPlayerTeamChange += UpdateTeamToOther;


    }
    public void JoinTeam(PlayerData _playerData)
    {
        string josnData = JsonUtility.ToJson(_playerData);

        photonView.RPC("TryJoinTeam", RpcTarget.MasterClient, josnData);
    }

    [PunRPC]
    private void TryJoinTeam(string _playerData, PhotonMessageInfo _info)
    {
        //    Debug.Log(_playerData);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(_playerData);
        playerData.info = _info;
        JoinTeamResult joinTeamResult = new JoinTeamResult();

        if (playerData.playerName == "admin")
        {
            LeaveAll();
          //  RoomManager.Instance.ChangeMaster(_info.Sender);
            joinTeamResult.complete = true;
            joinTeamResult.report = "Yor Are Admin";
            var jsonData = JsonUtility.ToJson(joinTeamResult);

            chooseTeam_canvas.SetActive(true);
            play_canvas.SetActive(false);
            end_canvas.SetActive(false);
            control_canvas.SetActive(false);


            photonView.RPC("JoinTeamResult", _info.Sender, jsonData);

            //  JoinTeamResult(jsonData);
        }
        else
        {
            if (playerData.code == code.Value)
            {
                if (team.PlayerCount(playerData.teamName) < maxTeamCount && team.TryToAddPlayer(playerData))
                {

                    joinTeamResult.complete = true;
                    joinTeamResult.report = "Add Team Complete";
                }
                else
                {

                    joinTeamResult.complete = false;
                    joinTeamResult.report = "Add Team Fail";
                }
            }
            else
            {

                joinTeamResult.complete = false;
                joinTeamResult.report = "Code Not Correct";
            }

            var jsonData = JsonUtility.ToJson(joinTeamResult);
            photonView.RPC("JoinTeamResult", _info.Sender, jsonData);
        }




    }
    [PunRPC]
    private void JoinTeamResult(string _result)
    {

        JoinTeamResult joinTeamResult = JsonUtility.FromJson<JoinTeamResult>(_result);

        if (joinTeamResult.complete)
        {
            report.text = joinTeamResult.report;
            if (PhotonNetwork.IsMasterClient)
            {
                control_canvas.SetActive(true);
                chooseTeam_canvas.SetActive(false);
            }
            else
            {
                play_canvas.SetActive(true);
                chooseTeam_canvas.SetActive(false);
            }


            enterGame.Value = true;
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

    [ContextMenu("LogShow")]
    private void Log()
    {
        team.LogShow();
    }

    [ContextMenu("Kick")]
    public void Kick(string _playerID)
    {
        //  team.RemovePlayer(_playerID);
        photonView.RPC("Leave", RpcTarget.MasterClient, _playerID);

    }
    [PunRPC]
    private void Leave(string _playerID)
    {
        photonView.RPC("GoToChooseTeam", team.GetPlayerByID(_playerID).info.Sender);
        team.RemovePlayer(_playerID);
    }


    [PunRPC]
    private void GoToChooseTeam()
    {
        chooseTeam_canvas.SetActive(true);
        play_canvas.SetActive(false);
        end_canvas.SetActive(false);
        enterGame.Value = false;

    }



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
