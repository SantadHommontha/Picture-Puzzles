using UnityEngine;

[CreateAssetMenu(menuName = "Values/SpriteValue")]
public class SpriteValue : ScriptableValue<Sprite>
{
      [SerializeField] Sprite initialValue;

    public override void ResetValue()
    {
        SetValue(initialValue);
    }
}
