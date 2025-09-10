using Photon.Pun;
using UnityEngine;

public class TestScript : MonoBehaviourPunCallbacks
{
  
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    
}
