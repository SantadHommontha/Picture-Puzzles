using UnityEngine;


public enum Game_State
{
    Main_Menu,
    Enter_Room,
    Choose_Image, // MC
    Wait_For_Play, // Play
    Play,
    Game_Over
}


public class GameManager : MonoBehaviour
{

    [SerializeField] private Game_State game_State;





    [Header("Value")]
    [SerializeField] private SpriteData_Value select_image;
    [SerializeField] private Select_Group_Value select_Group_Value;
    [Space]
    [SerializeField] private BoolValue random_btn_value;






    [Header("Event")]
    [SerializeField] private GameEvent event_canvas_choose_image;
    [SerializeField] private GameEvent event_canvas_play;
    [SerializeField] private GameEvent event_spawn_dust;
    [SerializeField] private GameEvent event_spawn_image_in_group;
    [SerializeField] private GameEvent event_spawn_group;
    [SerializeField] private GameEvent event_random_image;
    void Start()
    {
        event_canvas_choose_image.Raise(this, -979);


        select_image.OnValueChange += On_Player_Select_Image;
        select_Group_Value.OnValueChange += On_Player_Select_Group_Image;




        event_spawn_group.Raise(this, -979);








    }

    private void On_Player_Select_Group_Image(Group_Image _group_Image)
    {
        if (random_btn_value.Value)
        {
            event_random_image.Raise(this,-979);
        }
        else
        {
            event_spawn_image_in_group.Raise(this, _group_Image);
        }

    }
    private void On_Player_Select_Image(SpriteData _value)
    {
        event_canvas_play.Raise(this, _value);
    }



    private void Start_State(Game_State _new_State)
    {

        End_State();
        game_State = _new_State;
        switch (game_State)
        {
            case Game_State.Main_Menu:
                break;
            case Game_State.Enter_Room:
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


    private void End_State()
    {
        switch (game_State)
        {
            case Game_State.Main_Menu:
                break;
            case Game_State.Enter_Room:
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

    public void Update_State()

    {
        switch (game_State)
        {
            case Game_State.Main_Menu:
                break;
            case Game_State.Enter_Room:
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

    void Update()
    {
        Update_State();
    }
}
