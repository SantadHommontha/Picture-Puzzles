using UnityEngine;

public class UpAndDowwNumberToIntOrFloatValue : MonoBehaviour
{
    [SerializeField] private IntValue v_intVale;
    [SerializeField] private FloatValue v_floatValue;


    [Space]
    [SerializeField] private bool clamMaxValue;
    [SerializeField] private float maxValue;
    [Space]
    [SerializeField] private bool clamMinValue;
    [SerializeField] private float minValue;
    public void UP()
    {
        ChangeValue(1);
    }

    public void UP(float _value)
    {
        ChangeValue(_value);
    }
    public void DOWN()
    {
        ChangeValue(-1);
    }
    public void DOWN(float _value)
    {
        ChangeValue(_value);
    }



    private void ChangeValue(float _value)
    {
        if(v_floatValue || v_intVale)
        {
            if(v_floatValue != null)
            {

                v_floatValue.Value += _value;
                if(clamMaxValue )
                {
                    if (v_floatValue.Value > maxValue)
                    {
                    v_floatValue.Value = maxValue;
                    }        
                }
                if(clamMinValue)
                {
                    if (v_floatValue.Value < minValue)
                    {
                        v_floatValue.Value = minValue;
                    }
                }
             
            }
            else
            {
                v_intVale.Value += (int)_value;
                if (clamMaxValue )
                {
                    if( v_intVale.Value > maxValue)
                    {
                    v_intVale.Value = (int)maxValue;
                    }
                }
                if(clamMinValue)
                {
                    if (v_intVale.Value < minValue)
                    {
                        v_intVale.Value = (int)minValue;
                    }
                }
            }
        }
    }

}
