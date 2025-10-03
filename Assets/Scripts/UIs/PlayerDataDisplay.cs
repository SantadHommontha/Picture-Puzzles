using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerDataDisplay : MonoBehaviour
{

    [SerializeField] private TMP_Text name_text;
    [SerializeField] private int displayIndex;
    [SerializeField] private string playerID;
    void Start()
    {
        // playerDataValue.OnValueChange += UpdateText;
    }

    void OnEnable()
    {


        var ap = TeamManager.Instance.Team_Script.GetAllPlayer();
        if (displayIndex < ap.Count || ap != null)
        {
            playerID = ap[displayIndex].playerID;
            UpdateText(ap[displayIndex].playerName);
        }
    }


    public void UpdateText(string _playerName)
    {
        name_text.text = _playerName;

        //  Debug.Log($"PlayerName: " + _playerData.playerName);
    }

    public void Kick()
    {
        // if (PhotonNetwork.IsMasterClient)
        // {
        //     TeamManager.Instance.Kick(playerDataValue.Value.player_id);
        // }
    }
}
