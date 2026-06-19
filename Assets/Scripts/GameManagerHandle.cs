<<<<<<< HEAD
<<<<<<< HEAD
﻿using Photon.Pun;
using Photon.Realtime;
=======
>>>>>>> parent of 18c8e95 (fix game time out)
=======
>>>>>>> parent of 18c8e95 (fix game time out)
using System.Collections;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class GameManagerHandle : MonoBehaviourPunCallbacks
{
    private GameManager gameManager;
    [SerializeField] private Timer timer;
    [SerializeField] private FloatValue timerValue;
    private Game_State currentState;
    private bool aswer = false;

    [SerializeField] private GameObject diconnectPalnet;
    private Coroutine co_KeepIm;
    void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    private void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        Debug.Log(RoomData.Instance.isAdmin);
        if (RoomData.Instance.isAdmin)
        {
            gameManager.changeState += StartState;
            gameManager.StartState(Game_State.Choose_Image);

        }
        else
        {
            gameManager.StartState(Game_State.Enter_Name);
        }


        diconnectPalnet.SetActive(false);
    }


    public void StartState(Game_State _new_State)
    {
        EndState();
        currentState = _new_State;
        Debug.Log(_new_State);
        switch (_new_State)
        {
            case Game_State.Enter_Name:

                break;
            case Game_State.Choose_Image:
                if (PhotonNetwork.IsMasterClient)
                {

                    timerValue.OnValueChange += GameTimerUpdate;
                    if (co_KeepIm != null)
                        StopCoroutine(co_KeepIm);
                    co_KeepIm = StartCoroutine(KeepIn());
                }
                else
                {

                }
                break;
            case Game_State.Wait_For_Play:

                break;

            case Game_State.SetUPImage:

                break;
            case Game_State.Play:

                break;
            case Game_State.GameStart:


                if (PhotonNetwork.IsMasterClient)
                {
                    PixelatedHandle.Instance.StartSendFadeData();
                    timer.Start_Time(RoomData.Instance.gameTime);
                    SendGameDataToOther();
                }
                else
                {

                }
                break;
            case Game_State.Game_Over:
                if (PhotonNetwork.IsMasterClient)
                {
                    PixelatedHandle.Instance.StopSendFadeData();
                    timer.StopTimer();
                    SetGameOver();

                }
                else
                {

                }

                break;
            case Game_State.ShowImage:
                if (PhotonNetwork.IsMasterClient)
                {
                    ShowImageAswer(aswer);
                    timerValue.OnValueChange -= GameTimerUpdate;
                }
                else
                {

                }
                break;
        }
    }
    public void EndState()
    {

        switch (currentState)
        {
            case Game_State.Enter_Name:

                break;
            case Game_State.Choose_Image:

                break;
            case Game_State.Wait_For_Play:

                break;

            case Game_State.SetUPImage:

                break;
            case Game_State.Play:

                break;
            case Game_State.GameStart:

                break;
            case Game_State.Game_Over:
                if (PhotonNetwork.IsMasterClient)
                {
                    timerValue.OnValueChange -= GameTimerUpdate;
                }
                else
                {

                }

                break;
            case Game_State.ShowImage:


                break;
        }
    }
    public void UpdateState()
    {

        switch (currentState)
        {
            case Game_State.Enter_Name:

                break;
            case Game_State.Choose_Image:

                break;
            case Game_State.Wait_For_Play:

                break;

            case Game_State.SetUPImage:

                break;
            case Game_State.Play:

                break;
            case Game_State.GameStart:

                break;
            case Game_State.Game_Over:

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    // Correct();
                    //   ShowImageAswer(true);

                    //  StartState(Game_State.ShowImage);
                    gameManager.StartState(Game_State.ShowImage);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    //  InCorrect();
                    //   ShowImageAswer(false);
                    //   StartState(Game_State.ShowImage);
                    gameManager.StartState(Game_State.ShowImage);
                }
                Debug.Log("111111");
                break;
            case Game_State.ShowImage:

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                   // Correct();
                    ShowImageAswer(true);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                  //  InCorrect();
                    ShowImageAswer(false);
                }
                Debug.Log("2222222");
                break;
        }
    }

    void Update()
    {
        UpdateState();
        IfDisconnect();
    }


    private void IfDisconnect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            diconnectPalnet.SetActive(true);
        }
    }
    public void ShowImageAswer(bool _bool)
    {
        RoomData.Instance.aswer = _bool ? "Correct" : "InCorrect";
        photonView.RPC("RPC_ReciveAswer", RpcTarget.Others, _bool);
    }
    [PunRPC]
    public void RPC_ReciveAswer(bool _bool)
    {
        gameManager.StartState(Game_State.ShowImage);
        if (_bool)
        {
            RoomData.Instance.aswer = "Correct";
        }
        else
        {
            RoomData.Instance.aswer = "InCorrect";
        }
    }
    public void SendGameDataToOther()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        RoomDataWapper roomDataWapper = new RoomDataWapper()
        {
            gameStart = RoomData.Instance.gameStart,
        };
        var json = JsonUtility.ToJson(roomDataWapper);
        photonView.RPC("RPC_ReiveGameDataForMaster", RpcTarget.Others, json);
    }
    [PunRPC]
    private void RPC_ReiveGameDataForMaster(string _json)
    {
        if (PhotonNetwork.IsMasterClient) return;
        var roomData = JsonUtility.FromJson<RoomDataWapper>(_json);
        RoomData.Instance.gameStart = roomData.gameStart;
        gameManager.StartState(Game_State.GameStart);
    }

    private void GameTimerUpdate(float _timer)
    {

        if (!PhotonNetwork.IsMasterClient) return;
        RoomData.Instance.timer = _timer;
        if (_timer <= 0)
        {
            gameManager.StartState(Game_State.Game_Over);
            Debug.Log("Game OVer");
        }

        photonView.RPC("RPC_ReciveTimer", RpcTarget.Others, _timer);
    }

    [PunRPC]
    public void RPC_ReciveTimer(float _timer)
    {
        if (PhotonNetwork.IsMasterClient) return;
        timerValue.Value = _timer;
        RoomData.Instance.timer = _timer;
    }

    private void SetGameOver()
    {
        photonView.RPC("RPC_ReciveGameOver", RpcTarget.Others);
    }
    [PunRPC]
    private void RPC_ReciveGameOver()
    {
        gameManager.StartState(Game_State.Game_Over);
    }


    public void Correct()
    {
        aswer = true;
        gameManager.StartState(Game_State.ShowImage);
    }
    public void InCorrect()
    {
        aswer = false;
        gameManager.StartState(Game_State.ShowImage);
    }
    public void SHoww()
    {
        gameManager.StartState(Game_State.ShowImage);
    }

    IEnumerator KeepIn()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            photonView.RPC("RPC_KeepIn", RpcTarget.MasterClient);
        }
    }
    [PunRPC]
    private void RPC_KeepIn()
    {
        Debug.Log("HI");
    }

    public override void OnLeftRoom()
    {
        if (co_KeepIm != null)
        {
            StopCoroutine(co_KeepIm);
            co_KeepIm = null;
        }
<<<<<<< HEAD
<<<<<<< HEAD
        wasInRoom = false;
    }



    private bool wasInRoom = false;

    public override void OnJoinedRoom()
    {
        // จดจำไว้ว่าเราเข้าห้องสำเร็จแล้ว
        wasInRoom = true;
        Debug.Log("Joined Room Successfully!");
    }

    //public override void OnLeftRoom()
    //{
    //    // ถ้าเป็นการจงใจกดปุ่มออกจากห้องเอง ให้รีเซ็ตค่านี้
    //    wasInRoom = false;
    //}

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning($"Disconnected from Photon. Cause: {cause}");

        // ถ้าเราไม่ได้จงใจกด Disconnect เอง และเราเคยอยู่ในห้อง
        if (cause != DisconnectCause.DisconnectByClientLogic && wasInRoom)
        {
            Debug.Log("Attempting to Reconnect and Rejoin the room...");
            // คำสั่งพระเอก: จะพยายามต่อเน็ตใหม่และกลับเข้าห้องเดิมทันที
            bool isReconnecting = PhotonNetwork.ReconnectAndRejoin();

            if (!isReconnecting)
            {
                Debug.LogError("Failed to initiate ReconnectAndRejoin.");
                PhotonNetwork.ConnectUsingSettings();
            }
        }
=======
>>>>>>> parent of 18c8e95 (fix game time out)
=======
>>>>>>> parent of 18c8e95 (fix game time out)
    }
}
