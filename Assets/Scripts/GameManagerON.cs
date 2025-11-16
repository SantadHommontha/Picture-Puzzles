using Photon.Pun;
using UnityEngine;

public class GameManagerON : MonoBehaviourPunCallbacks
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    private Game_State game_State;

    public void StartState(Game_State _game_State)
    {
        game_State = _game_State;
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
                SendGameStartToOther();
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
    bool gameDataSend;
    // Update is called once per frame
    void Update()
    {
        UpdateState();

        if (gameManager.game_State == Game_State.Play && !gameDataSend)
        {
            SendGameStartToOther();
            gameDataSend = true;
        }
    }




    private void SendGameStartToOther()
    {
        Debug.Log($"SendGameStart {RoomData.Instance.gameStart}");
        photonView.RPC("RPC_ReciveGameStartFormMaster", RpcTarget.Others, RoomData.Instance.gameStart);
    }
    [PunRPC]
    private void RPC_ReciveGameStartFormMaster(bool _gameStart)
    {
        Debug.Log($"SendGameStart {_gameStart}");
        RoomData.Instance.gameStart = _gameStart;
    }
}
