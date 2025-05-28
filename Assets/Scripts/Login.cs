using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private string admin_name;
    //Call With UI Btn
    public void Enter_Btn()
    {
        string _input = input.text.ToLower();
        if (_input == admin_name.ToLower())
        {

        }
        else
        {
            
        }
    }
}
