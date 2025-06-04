using NUnit.Framework.Constraints;
using Photon.Realtime;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Login : MonoBehaviour
{
    public static Login Instance;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private string admin_name;

    private bool iamAdmin = false;
    [SerializeField] private TMP_Text massage;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    //Call With UI Btn
    public void Enter_Btn()
    {
        string _input = input.text.ToLower();
        Debug.Log("Login: " + _input);
        if (_input == admin_name.ToLower())
        {
            var rc = MasterController.Instance.RandomRoomCode();
            Debug.Log("RC: " + rc);
            RoomManager.Instance.CreateRoom(rc);
            massage.text = "Create Room";
            iamAdmin = true;
        }
        else
        {
            if (RoomManager.Instance.RoomHasCreate(_input))
            {
                RoomManager.Instance.JoinRoom(_input);
                massage.text = "Have Room";
            }
            else
            {
                massage.text = "No Room";
            }
        }
    }
}
