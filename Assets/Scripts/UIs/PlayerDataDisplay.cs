using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerDataDisplay : MonoBehaviour
{

    [SerializeField] private TMP_Text name_text;
    [SerializeField] private int displayIndex;
    [SerializeField] private string playerID;

    [SerializeField] private StringValue playerName;
    [SerializeField] private StringValue playerId;

    void Start()
    {
        // playerDataValue.OnValueChange += UpdateText;
        playerName.OnValueChange += UpdateText;
    }

    void OnEnable()
    {


        // var ap = TeamManager.Instance.Team_Script.GetAllPlayer();
        // if (displayIndex < ap.Count || ap != null)
        // {
        //     playerID = ap[displayIndex].playerID;
        //     UpdateText(ap[displayIndex].playerName);
        // }
    }



    public void UpdateText(string _playerName)
    {
        if (name_text.text != _playerName)
            name_text.text = _playerName;
    }

    public void Kick()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            TeamManager.Instance.Kick(playerId.Value);
        }
    }
}
