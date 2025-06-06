using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Test_Show_Conncect_And_Master : MonoBehaviour
{
    [SerializeField] private Image server;
    [SerializeField] private Image master;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsConnected)
            server.color = Color.green;
        else
            server.color = Color.red;

        if (PhotonNetwork.IsMasterClient)
            master.color = Color.green;
        else
            master.color = Color.red;
    }
}
