using UnityEngine;
using UnityEngine.UI;

public class ToggleToBoolValue : MonoBehaviour
{
    [SerializeField] private BoolValue v_boolvalue;

    [SerializeField] private Toggle toggle;
    [SerializeField] private bool setOnStart;

    public void OnValueChange(bool _newValue)
    {
        v_boolvalue.Value = _newValue;
       
    }

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(setOnStart)
        {
            OnValueChange(toggle.isOn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
