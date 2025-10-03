
using UnityEngine;
using UnityEngine.SceneManagement;
public class ConnectToLobbyHandle : MonoBehaviour
{

    private ConnectToLobby connectToLobby;


    private void Awake()
    {
        connectToLobby = GetComponent<ConnectToLobby>();
    }
    void Start()
    {

        connectToLobby.ConnectToMaster();

    }
}
