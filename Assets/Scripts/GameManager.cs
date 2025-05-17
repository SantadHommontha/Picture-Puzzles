using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private SpriteData_Value select_image;
    [SerializeField] private Select_Group_Value select_Group_Value;







    [Header("Event")]
    [SerializeField] private GameEvent event_canvas_choose_image;
    [SerializeField] private GameEvent event_canvas_play;
    [SerializeField] private GameEvent event_spawn_dust;
    [SerializeField] private GameEvent event_spawn_image_in_group;
    [SerializeField] private GameEvent event_spawn_group;
    void Start()
    {
        event_canvas_choose_image.Raise(this, -979);


        select_image.OnValueChange += On_Player_Select_Image;
        select_Group_Value.OnValueChange += On_Player_Select_Group_Image;




        event_spawn_group.Raise(this,-979);

    






    }

    private void On_Player_Select_Group_Image(Group_Image _group_Image)
    {
        event_spawn_image_in_group.Raise(this, _group_Image);
    }
    private void On_Player_Select_Image(SpriteData _value)
    {
        event_canvas_play.Raise(this, _value);
    }



    void Update()
    {

    }
}
