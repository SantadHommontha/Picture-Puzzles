using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private SpriteData_Value select_image;







    [Header("Event")]
    [SerializeField] private GameEvent event_canvas_choose_image;
    [SerializeField] private GameEvent event_canvas_play;
    [SerializeField] private GameEvent event_spawn_dust;
    void Start()
    {
        event_canvas_choose_image.Raise(this, -979);


        select_image.OnValueChange += On_Player_Select_Image;

    }


    private void On_Player_Select_Image(SpriteData _value)
    {
        event_canvas_play.Raise(this, -979);
    }



    void Update()
    {

    }
}
