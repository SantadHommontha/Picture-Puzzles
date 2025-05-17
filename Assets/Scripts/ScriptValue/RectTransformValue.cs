using UnityEngine;


[CreateAssetMenu(menuName = "Values/RectTransform_Value", fileName = "RectTransform_Value")]
public class RectTransformValue : ScriptableValue<RectTransform>
{
    [SerializeField] RectTransform initialValue;

    public override void ResetValue()
    {
        SetValue(initialValue);
    }
}
