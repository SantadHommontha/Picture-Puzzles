using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
using System.Collections;
public class ConnectToLobby : MonoBehaviourPunCallbacks
{

    [SerializeField] private Image loadBar;
    private void Start()
    {
        loadBar.fillAmount = 0;
         PhotonNetwork.IsMessageQueueRunning = true;
        if (PhotonNetwork.IsConnected)
        {
            StartCoroutine(CountDownToloadScene());
        }
        else
        {

            PhotonNetwork.ConnectUsingSettings();

        }
    }

    public void FillBar(float _fillamount)
    {
        if (loadBar)
        {
            loadBar.fillAmount = _fillamount;
        }
    }
    public void ConnectToMaster()
    {
        Debug.Log("Connecting To Server");
        FillBar(0.3f);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Join Server");
        PhotonNetwork.JoinLobby();
        FillBar(0.6f);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Join lobby");
        FillBar(0.8f);
        StartCoroutine(CountDownToloadScene());

    }

    private IEnumerator CountDownToloadScene()
    {
        yield return new WaitForSeconds(0.5f);
        FillBar(1f);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Enter Room");
    }


}
