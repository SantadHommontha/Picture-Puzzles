using TMPro;
using UnityEngine;

public class SetGameTime : MonoBehaviour
{
    [SerializeField] private TMP_InputField tMP_InputField;


    public void OnTextChange(string _text)
    {
        if (int.TryParse(_text, out var result))
            RoomData.Instance.gameTime = result;
    }
}
