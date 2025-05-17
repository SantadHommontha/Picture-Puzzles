using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Group_A_Image : MonoBehaviour, IPointerDownHandler
{

    public bool is_title;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text title_name;

    [SerializeField] private Select_Group_Value select_Group;
    [SerializeField] private SpriteData_Value sprite_Data;
    private Group_Image group_Image;
    private SpriteData spriteData;


    public void Set_Image(Sprite _image)
    {
        image.sprite = _image;
    }

    public void Set_Title_Name(string _name)
    {
        title_name.text = _name;
    }


    public void Set_UP(Sprite _title_image, string _title_name, Group_Image _group_Image)
    {
        Set_Image(_title_image);
        Set_Title_Name(_title_name);
        group_Image = _group_Image;
    }


    public void Set_UP(Sprite _title_image, string _title_name, SpriteData _spriteData)
    {
        Set_Image(_title_image);
        Set_Title_Name(_title_name);
        spriteData = _spriteData;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        print($"Click At: {name}");

        if (is_title)
        {
            select_Group.Value = group_Image;
        }
        else
        {
            sprite_Data.Value = spriteData;
        }
    }







}
