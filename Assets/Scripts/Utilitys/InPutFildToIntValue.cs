using TMPro;
using UnityEngine;

public class InPutFildToIntValue : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private FloatValue floatValue;
    [SerializeField] private IntValue intValue;
    [SerializeField] private StringValue stringValue;

    [SerializeField] private bool setOnStart = false;

    public void TextUpdate(string _text)
    {
        if (floatValue && float.TryParse(_text, out var result))
        {
            floatValue.Value = result;
        }
        else if (intValue && int.TryParse(_text, out var result1))
        {
            intValue.Value = result1;
        }
        else if (stringValue)
        {
            stringValue.Value = _text;
        }
    }

    void Start()
    {
        if (setOnStart)
        {
            SetOnStart();
        }
    }

    public void SetOnStart()

    {
        TextUpdate(inputField.text);
    }
}
