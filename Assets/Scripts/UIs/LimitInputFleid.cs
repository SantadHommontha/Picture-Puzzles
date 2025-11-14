using TMPro;
using UnityEngine;


public class LimitInputFleid : MonoBehaviour
{

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private int limitCharater = 4;
    public void CharacterUpdate(string _input)
    {
        if (_input.Length <= limitCharater)
        {
            inputField.text = _input.ToUpper();
        }
        else
        {
            inputField.text = _input.Substring(0, limitCharater).ToUpper();
        }
    }
}
