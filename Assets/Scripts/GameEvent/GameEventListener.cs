using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class CustomEvent : UnityEvent<Component, object> { }
public class GameEventListener : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField][TextArea] private string description;
#endif
    public CustomEvent response;
    public GameEvent gameEvent;

    public void OnEventRised(Component _sender, object _data)
    {

        response?.Invoke(_sender, _data);
    }

    private void OnEnable()
    {
        if (gameEvent == null) return;
        gameEvent.RegisterListener(this);
    }


    private void OnDisable()
    {
        if (gameEvent == null) return;
        gameEvent?.UnregisterListener(this);
    }










}
