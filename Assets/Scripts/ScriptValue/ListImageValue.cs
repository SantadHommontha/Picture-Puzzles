using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Values/ListImageValue")]
public class ListImageValue : ScriptableValue<List<Sprite>>
{
      [SerializeField] List<Sprite> initialValue;

    public override void ResetValue()
    {
        SetValue(initialValue);
    }
}
