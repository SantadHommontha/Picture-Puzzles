using TMPro;
using UnityEngine;

public class ShowAswer : MonoBehaviour
{
    [SerializeField] private TMP_Text tMP_Text;
    void FixedUpdate()
    {
        if (tMP_Text.text != RoomData.Instance.aswer)
        {
            UpdateText();
        }
    }
    [ContextMenu("UpdateText")]
    public void UpdateText()
    {
        ShowText(RoomData.Instance.aswer);
    }
    public void ShowText(string _text)
    {
        tMP_Text.text = _text;
    }
}
