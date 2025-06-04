using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerDataDisplay : MonoBehaviour
{
    [SerializeField] private PlayerDataValue playerDataValue;
    [SerializeField] private TMP_Text name_text;
    void Start()
    {
        playerDataValue.OnValueChange += UpdateText;
    }

    void OnEnable()
    {
        UpdateText(playerDataValue.Value);
    }


    public void UpdateText(playerDataDisplay _playerData)
    {
        name_text.text = _playerData.player_name;
        //  Debug.Log($"PlayerName: " + _playerData.playerName);
    }

    public void Kick()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            TeamManager.instance.Kick(playerDataValue.Value.player_id);
        }
    }
}
