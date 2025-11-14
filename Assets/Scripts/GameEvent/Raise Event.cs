using UnityEngine;

public class RaiseEvent : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;

    [ContextMenu("RaiseEvent")]
    public void RaiseEventF()
    {
        gameEvent.Raise(this, -999);
    }


}
