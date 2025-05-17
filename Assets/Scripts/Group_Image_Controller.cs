using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Group_Image_Controller", menuName = "Scriptable Objects/Group_Image_Controller")]
public class Group_Image_Controller : MonoBehaviour
{
    [SerializeField] private SpriteValue selcet_sprite;

    [SerializeField] private Group_Image[] group_Images;

    [SerializeField] private Group_Image_Controller_Setting setting;

    // [SerializeField] private GameObject score_area;
    [SerializeField] private GameObject score_sontent;


    [SerializeField] private Select_Group_Value select_Group_Value;
    [SerializeField] private BoolValue back_btn;


    void Start()
    {
        select_Group_Value.OnValueChange += Show_Image_In_Group;


        Show_Image_Group();
        back_btn.OnValueChange += Show_Image_Group;
    }


    private List<Transform> Get_All_Children(Transform _parent)
    {
        List<Transform> all_children = new List<Transform>();
        for (int i = 0; i < _parent.childCount; i++)
        {
            all_children.Add(_parent.GetChild(i));
        }
        return all_children;
    }


    private void Delete_All_Content()
    {
        foreach (var content in Get_All_Children(score_sontent.transform))
        {
            Destroy(content.gameObject);
        }
    }


    private void Create_Group_Image(Group_Image _group_image)
    {
        GameObject img = Instantiate(setting.image_prefap, score_sontent.transform);
        var img_a_image = img.GetComponent<Group_A_Image>();
        img_a_image.Set_Image(_group_image.title_image);
        img_a_image.Set_UP(_group_image.title_image, _group_image.title_name, _group_image);
        img_a_image.is_title = true;
    }

    private void Create_Image(SpriteData _spriteData)
    {
        GameObject img = Instantiate(setting.image_prefap, score_sontent.transform);
        var img_a_image = img.GetComponent<Group_A_Image>();
        img_a_image.Set_UP(_spriteData.image, _spriteData.name, null);
        img_a_image.is_title = false;
    }
    private void Show_Image_Group() => Show_Image_Group(true);
    private void Show_Image_Group(bool _value)
    {
        if (_value)
        {
            Delete_All_Content();
            foreach (var g in group_Images)
            {
                Create_Group_Image(g);
            }
        }

    }


    public void Show_Image_In_Group(Group_Image _group_Image)
    {
        Delete_All_Content();

        foreach (var g in _group_Image.sprite_datas)
        {
            Create_Image(g);
        }



    }


}
