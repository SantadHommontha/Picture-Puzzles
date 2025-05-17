using UnityEngine;

[CreateAssetMenu(fileName = "Select_image_Value", menuName = "Values/Select_image_Value")]
public class Select_image_Value : ScriptableValue<Select_Image>
{
    [SerializeField] Select_Image initialValue;

    public override void ResetValue()
    {
        SetValue(initialValue);
    }
}
