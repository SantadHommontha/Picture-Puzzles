using UnityEngine;

public class Btn_bool : MonoBehaviour
{
    [SerializeField] private BoolValue boolValue;
   public void Click()
   {
        boolValue.Value = true;
        boolValue.SetValue(false);
   }
}
