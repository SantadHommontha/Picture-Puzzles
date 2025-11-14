using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackToLoadingScene : MonoBehaviour
{
    public string sceneName = "Loading";
    void Awake()
    {
        if (!PhotonNetwork.IsConnected)
            SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
