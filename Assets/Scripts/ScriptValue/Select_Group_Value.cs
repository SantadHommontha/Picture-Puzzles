using UnityEngine;

[CreateAssetMenu(fileName = "Select_Group", menuName = "Values/Select_Group")]
public class Select_Group_Value : ScriptableValue<Group_Image>
{
      [SerializeField] Group_Image initialValue;

    public override void ResetValue()
    {
        SetValue(initialValue);
    }
}
