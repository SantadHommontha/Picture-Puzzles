using UnityEngine.UI;
using Photon.Pun;
using UnityEngine;
using TMPro;

public class ShowStatus : MonoBehaviour
{
    [SerializeField] private Image connect;
    [SerializeField] private Image lobby;
    [SerializeField] private Image inroom;
    [SerializeField] private Image master;
    [SerializeField] private TMP_Text roomName;

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsConnected)
            connect.color = Color.green;
        else
            connect.color = Color.red;
        if (PhotonNetwork.InLobby)
            lobby.color = Color.green;
        else
            lobby.color = Color.red;

        if (PhotonNetwork.InRoom)
            inroom.color = Color.green;
        else
            inroom.color = Color.red;

        if (PhotonNetwork.IsMasterClient)
            master.color = Color.green;
        else
            master.color = Color.red;

        roomName.text = PhotonNetwork.CurrentRoom != null ? PhotonNetwork.CurrentRoom.Name : "Null";
    }
}
