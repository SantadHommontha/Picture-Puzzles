using System.Collections;
using UnityEngine.SceneManagement;
using Photon.Pun;

using UnityEngine;
using Unity.VisualScripting;


public enum Game_State
{
    None,
    Main_Menu,
    Enter_Room,
    Enter_Name,
    Choose_Image, // MC
    Wait_For_Play, // Play
    Play,
    Game_Over
}


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();

                if (_instance == null)
                {
                    _instance = new GameObject("GameManager", typeof(GameManager)).GetComponent<GameManager>();

                }
            }
            return _instance;
        }
    }
    private static bool applicationIsQuitting = false;


    private void Awake()
    {
        if (_instance != null || _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void Start()
    {
        if (RoomData.Instance.isAdmin)
        {
            StartState(Game_State.Choose_Image);
        }
        else
        {
            StartState(Game_State.Enter_Name);
        }
    }

    [SerializeField] private Game_State game_State = Game_State.None;
    public void StartState(Game_State _new_State)
    {
        EndState();
        game_State = _new_State;
        switch (game_State)
        {
            case Game_State.Main_Menu:
                break;
            case Game_State.Enter_Room:
                break;
            case Game_State.Enter_Name:

                break;
            case Game_State.Choose_Image:
                break;
            case Game_State.Wait_For_Play:
                break;
            case Game_State.Play:

                break;
            case Game_State.Game_Over:
                break;
        }
    }

    public void EndState()
    {

        switch (game_State)
        {
            case Game_State.Main_Menu:
                break;
            case Game_State.Enter_Room:
                break;
            case Game_State.Enter_Name:

                break;
            case Game_State.Choose_Image:
                break;
            case Game_State.Wait_For_Play:
                break;
            case Game_State.Play:

                break;
            case Game_State.Game_Over:
                break;
        }
    }
    public void UpdateState()
    {

        switch (game_State)
        {
            case Game_State.Main_Menu:
                break;
            case Game_State.Enter_Room:
                break;
            case Game_State.Enter_Name:

                break;
            case Game_State.Choose_Image:
                break;
            case Game_State.Wait_For_Play:
                break;
            case Game_State.Play:

                break;
            case Game_State.Game_Over:
                break;
        }
    }

}
