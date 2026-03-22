using Photon.Pun;
using UnityEngine;

public class Show_While_Disconnect : MonoBehaviour
{
    [SerializeField] private GameObject target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        if(!PhotonNetwork.IsConnected)
        {
            target.SetActive(true);
        }
        else
        {
            target.SetActive(false);
        }
    }
}
