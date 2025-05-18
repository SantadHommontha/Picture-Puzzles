using UnityEngine;

public class Back_Btn : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
   public void Click()
   {
       gameEvent.Raise(this,-979);
   }
}
