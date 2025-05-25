using TMPro;
using UnityEngine;

public class Display_Value : MonoBehaviour
{

    [SerializeField] private TMP_Text text_ui;
    [SerializeField] private string start_text;
    [SerializeField] private string end_text;
    [Header("Value")]
    [SerializeField] private IntValue intValue;
    [SerializeField] private FloatValue floatValue;
    [SerializeField] private StringValue stringValue;

    void Start()
    {   
        if (stringValue) stringValue.OnValueChange += Show_UI;
        if (intValue) intValue.OnValueChange += IntValue;
        if (floatValue) floatValue.OnValueChange += FloatValue;
    }

    private void Show_UI(string _text) => text_ui.text = $"{start_text}{_text}{end_text}";



    private void IntValue(int _int) => Show_UI(_int.ToString());
    private void FloatValue(float _float) => Show_UI(_float.ToString("F1"));


}
