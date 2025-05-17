using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SpriteData{
     public Sprite image;
    public string name;
}

[CreateAssetMenu(fileName = "Group Image", menuName = "Scriptable Objects/Group Image")]
public class Group_Image : ScriptableObject
{
    public Sprite title_image;
    public string title_name;
    public List<SpriteData> sprite_datas;





















    // public void Add_Sprite(Sprite _sprite)
    // {
    //     sprite_datas.Add(_sprite);
    // }

    // public void Add_Sprites(List<Sprite> _sprites)
    // {
    //     sprite_datas = _sprites;
    // }

    // public void Add_Title_Sprite(Sprite _title_sprite)
    // {
    //     title_image = _title_sprite;
    // }


    // public void Add_Image_Set(List<Sprite> _sprites, Sprite _title_sprite)
    // {
    //     Add_Sprites(_sprites);
    //     Add_Title_Sprite(_title_sprite);
    // }
}
