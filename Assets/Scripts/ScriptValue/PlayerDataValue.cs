using UnityEngine;

[System.Serializable]
public struct playerDataDisplay
{
    public string player_name;
    public string player_id;
}


[CreateAssetMenu(menuName = "Values/PlayerDataValue")]
public class PlayerDataValue : ScriptableValue<playerDataDisplay>
{
    [SerializeField] private playerDataDisplay initialValue = new playerDataDisplay();

    public override void ResetValue()
    {
        value.player_name = "";
        value.player_id = "";

    }


    public override void SetValue(playerDataDisplay _value)
    {
        base.SetValue(_value);
        OnValueChange.Invoke(value);
    }
}
