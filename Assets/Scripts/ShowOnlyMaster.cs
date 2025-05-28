using Photon.Pun;
using UnityEngine;

public class ShowOnlyMaster : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private bool invert;
    void FixedUpdate()
    {
        if (!invert)
            target.SetActive(PhotonNetwork.IsMasterClient);
        else
            target.SetActive(!PhotonNetwork.IsMasterClient);
    }
}
