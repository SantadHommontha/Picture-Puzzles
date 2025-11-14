using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class GameManagerHandle : MonoBehaviour
{
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    private void Start()
    {
        Debug.Log(RoomData.Instance.isAdmin);
        if (RoomData.Instance.isAdmin)
        {
            gameManager.StartState(Game_State.Choose_Image);
        }
        else
        {
            gameManager.StartState(Game_State.Enter_Name);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
