using UnityEngine;

[CreateAssetMenu(fileName = "Game_State_Value", menuName = "Values/Game_State_Value")]
public class Game_State_Value : ScriptableValue<Game_State>
{
    [SerializeField] Game_State initialValue;

    public override void ResetValue()
    {
        SetValue(initialValue);
    }
}
