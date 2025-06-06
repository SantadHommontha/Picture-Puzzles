using System.Collections;
using UnityEngine.SceneManagement;
using Photon.Pun;

using UnityEngine;


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
    public static GameManager instance;
    [SerializeField] private Game_State game_State;
    [SerializeField] private float time_to_update;
    private PhotonView photonView;
    // private Timer timer;

    [SerializeField] private GameObject play_canvas_Btn;
    [SerializeField] private GameObject gameOver_canvas_Btn;
    [SerializeField] private GameObject waite_text_obj;
    [SerializeField] private GameObject timer_ui_obj;
    private Timer timer_script;

    [Header("Value")]
    [SerializeField] private SpriteData_Value select_image;
    [SerializeField] private Select_Group_Value select_Group_Value;
    [Space]
    [SerializeField] private BoolValue random_btn_value;
    [SerializeField] private Game_State_Value game_State_Value;
    [SerializeField] private FloatValue timer;
    [SerializeField] private FloatValue game_timer_value;
    [SerializeField] private BoolValue game_start;





    [Header("Event")]
    [SerializeField] private GameEvent event_canvas_main_menu;
    [SerializeField] private GameEvent event_canvas_enter_room;
    [SerializeField] private GameEvent event_canvas_ener_name;
    [SerializeField] private GameEvent event_canvas_choose_image;
    [SerializeField] private GameEvent event_canvas_wait_for_play;
    [SerializeField] private GameEvent event_canvas_play;
    [SerializeField] private GameEvent event_canvas_game_over;
    [SerializeField] private GameEvent event_canvas_show_sclect_Group;
    [Space]
    [SerializeField] private GameEvent event_spawn_dust;
    [SerializeField] private GameEvent event_spawn_image_in_group;
    [SerializeField] private GameEvent event_spawn_group;
    [SerializeField] private GameEvent event_random_image;
    [Space]
    [SerializeField] private GameEvent event_update_data;


    [Space]
    [SerializeField] private string sceneName;

    public void SetUp()
    {
        timer.Value = 0;
    }
    void Awake()
    {
        // timer = GetComponent<Timer>();
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }
    void Start()
    {
        //event_canvas_choose_image.Raise(this, -979);


        select_image.OnValueChange += On_Player_Select_Image;
        select_Group_Value.OnValueChange += On_Player_Select_Group_Image;

        if (photonView == null)
            photonView = GetComponent<PhotonView>();
        if (!timer_script)
            timer_script = GetComponent<Timer>();

        //  event_spawn_group.Raise(this, -979);


        // StartCoroutine(Cool());
        //  StartCoroutine(LoadSceneAsync(sceneName));

        Start_State(Game_State.Main_Menu);
        //game_start.OnValueChange += SendGameStart;
        //  timer.OnValueChange += SendGameTime;

    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            Debug.Log($"Loading Progress: {asyncLoad.progress}");

            // เมื่อ asyncLoad.progress >= 0.9 หมายถึงโหลดเสร็จแล้ว รอการ activate
            if (asyncLoad.progress >= 0.9f)
            {
                Debug.Log("Almost done loading...");
            }

            yield return null;
        }

        Debug.Log("Scene Loaded Completely");

    }

    private void On_Player_Select_Group_Image(Group_Image _group_Image)
    {
        if (game_State_Value.Value == Game_State.Choose_Image)
        {
            if (random_btn_value.Value)
            {
                event_random_image.Raise(this, -979);
                event_canvas_show_sclect_Group.Raise(this, -979);
            }
            else
            {
                event_spawn_image_in_group.Raise(this, _group_Image);
            }
        }


    }
    private void On_Player_Select_Image(SpriteData _value)
    {
        // if (game_State_Value.Value == Game_State.Choose_Image)
        // {
        //     event_canvas_play.Raise(this, _value);
        // }
    }
    public void AfterJoinRoom()
    {
        if (PhotonNetwork.IsMasterClient)
            Start_State(Game_State.Choose_Image);
        else
            Start_State(Game_State.Enter_Name);
    }
    // Call With UI
    public void Start_Game()

    {
        if (select_Group_Value.Value)

            Start_State(Game_State.Play);
    }

    public void Select_Image()

    {
        if (select_Group_Value.Value)
        {
            Start_State(Game_State.Wait_For_Play);
            Group_Image_Controller.Instance.SendImageNameScele();
        }
    }

    public void Switch_To_Choose_State() => Start_State(Game_State.Choose_Image);

    public void Clear_Select_Group()
    {
        select_Group_Value.SetValue(null);
    }


    public void Start_State(Game_State _new_State)
    {

        End_State();
        game_State = _new_State;

        Debug.Log($"Game State:" + game_State);
        game_State_Value.Value = game_State;
        switch (game_State)
        {
            case Game_State.Main_Menu:

                event_canvas_main_menu.Raise(this, -979);
                RoomManager.Instance.ConnectToServer();
                break;
            case Game_State.Enter_Room:
                event_canvas_enter_room.Raise(this, -979);
                break;
            case Game_State.Enter_Name:
                event_canvas_ener_name.Raise(this, -979);
                break;
            case Game_State.Choose_Image:

                if (PhotonNetwork.IsMasterClient)
                {
                    event_canvas_choose_image.Raise(this, -979);
                    game_start.Value = false;
                    gameOver_canvas_Btn.SetActive(false);
                }

                break;
            case Game_State.Wait_For_Play:
                event_canvas_wait_for_play.Raise(this, -979);
                if (PhotonNetwork.IsMasterClient)
                {
                    Dust_Controller.Instance.Create();
                    play_canvas_Btn.SetActive(true);
                    gameOver_canvas_Btn.SetActive(false);
                }
                else
                {
                    waite_text_obj.SetActive(true);
                }
                timer_ui_obj.SetActive(true);
                break;
            case Game_State.Play:
                event_canvas_play.Raise(this, -979);
                if (PhotonNetwork.IsMasterClient)
                {
                    game_start.Value = true;

                    play_canvas_Btn.SetActive(false);
                    gameOver_canvas_Btn.SetActive(false);
                    timer_script.Start_Time();
                    // StartCoroutine(TimeToUpdate());
                    SendOtherToStart();
                }
                else
                {
                    play_canvas_Btn.SetActive(false);
                    gameOver_canvas_Btn.SetActive(false);
                    waite_text_obj.SetActive(false);
                    StartCoroutine(TimeToUpdate());
                    timer_script.Start_Time();
                }

                // timer.Start_Time();
                break;
            case Game_State.Game_Over:

                event_canvas_game_over.Raise(this, -979);
                if (PhotonNetwork.IsMasterClient)
                {
                    game_start.Value = false;
                    play_canvas_Btn.SetActive(false);
                    gameOver_canvas_Btn.SetActive(true);
                    SendOtherToStart();
                }
                else
                {
                    game_start.Value = false;
                    play_canvas_Btn.SetActive(false);
                    gameOver_canvas_Btn.SetActive(false);
                }
                timer_ui_obj.SetActive(false);
                break;

        }
    }


    private void End_State()
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

    private void Update_State()

    {
        // if (!PhotonNetwork.IsMasterClient)
        //     return;
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

                if (timer.Value <= 0)
                {
                    Start_State(Game_State.Game_Over);
                }
                break;
            case Game_State.Game_Over:
                break;

        }
    }

    void Update()
    {
        Update_State();
    }

    #region Send Value To Other
    // Send

    private void SendOtherToStart()
    {
        float[] n = new float[2];

        n[0] = game_start.Value ? 1 : 0;
        n[1] = game_timer_value.Value;

        photonView.RPC("ReceiveGameStart", RpcTarget.Others, n);
    }
    private void SendGameStart(bool _data)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        photonView.RPC("ReceiveGameStart", RpcTarget.Others, _data);
    }

    public void SendGameTime()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        photonView.RPC("ReceiveGameTime", RpcTarget.Others, timer.Value);
    }

    private void SendGameState()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        photonView.RPC("ReceiveGameState", RpcTarget.Others, game_State);

    }

    // Receive
    [PunRPC]
    private void ReceiveGameStart(float[] _n)
    {
        if (PhotonNetwork.IsMasterClient) return;


        if (_n[0] == 1f)
        {
            game_timer_value.Value = _n[1];
            game_start.Value = true;
            Start_State(Game_State.Play);
        }
        else
        {
            game_start.Value = false;
        }
    }
    [PunRPC]
    private void ReceiveGameTime(float _timer)
    {
        if (PhotonNetwork.IsMasterClient) return;
        timer.Value = _timer;
        timer_script.Start_Time(timer.Value);
    }
    [PunRPC]
    private void ReceiveGameState(int _stateIndex)
    {
        if (PhotonNetwork.IsMasterClient) return;
        Debug.Log($"S: {_stateIndex}");
        switch (_stateIndex)
        {
            case 0:
                Start_State(Game_State.None);

                break;
            case 1:
                Start_State(Game_State.Main_Menu);
                break;
            case 2:
                Start_State(Game_State.Enter_Room);
                break;
            case 3:
                //   Start_State(Game_State.Choose_Image);
                break;
            case 4:
                Start_State(Game_State.Wait_For_Play);
                break;
            case 5:
                Start_State(Game_State.Play);
                break;
                // case 6:
                //     Start_State(Game_State.Game_Over);
                //     break;
        }
    }
    #endregion

    private IEnumerator TimeToUpdate()
    {
        while (game_start.Value)
        {
            yield return new WaitForSeconds(time_to_update);
            // if (PhotonNetwork.IsMasterClient)
            // {

            // }
            // else
            // {

            // }
            Dust_Controller.Instance.SendDustAlpha();

            Debug.Log("Update Data");
        }
    }
    //Test
    private IEnumerator Cool()
    {
        Start_State(Game_State.Main_Menu);

        yield return new WaitForSeconds(1);

        Start_State(Game_State.Enter_Room);

        yield return new WaitForSeconds(1);

        Start_State(Game_State.Choose_Image);

        yield return new WaitForSeconds(1);

        Start_State(Game_State.Wait_For_Play);

        yield return new WaitForSeconds(1);

        Start_State(Game_State.Play);

        yield return new WaitForSeconds(1);

        Start_State(Game_State.Game_Over);

        yield return new WaitForSeconds(1);
    }



}
