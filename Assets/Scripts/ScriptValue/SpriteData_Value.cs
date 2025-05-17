using UnityEngine;

[CreateAssetMenu(fileName = "Select_image_Value", menuName = "Values/SpriteData_Value")]
public class SpriteData_Value : ScriptableValue<SpriteData>
{
    [SerializeField] SpriteData initialValue;

    public override void ResetValue()
    {
        SetValue(initialValue);
    }
}
