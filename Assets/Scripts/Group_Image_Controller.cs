using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[CreateAssetMenu(fileName = "Group_Image_Controller", menuName = "Scriptable Objects/Group_Image_Controller")]
public class Group_Image_Controller : MonoBehaviour
{


    [SerializeField] private Group_Image[] group_Images;

    [Header("Setting")]
    [SerializeField] private Group_Image_Controller_Setting setting;

    // [SerializeField] private GameObject score_area;
    [SerializeField] private GameObject score_sontent;

    [Header("Value")]
    [SerializeField] private Select_Group_Value select_Group_Value;
    [SerializeField] private BoolValue back_btn;
    [SerializeField] private SpriteData_Value sprite_Data;

    void Start()
    {
       

        Show_Group();

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
        img_a_image.Set_UP(_spriteData.image, _spriteData.name, _spriteData);
        img_a_image.is_title = false;
    }

    public void Show_Group()
    {
        Delete_All_Content();
        foreach (var g in group_Images)
        {
            Create_Group_Image(g);
        }
    }

    public void Random_Image(Group_Image _group_Image)
    {
        var sp = _group_Image.sprite_datas[UnityEngine.Random.Range(0, _group_Image.sprite_datas.Count)];

        sprite_Data.Value = sp;
    }

    public void Show_Image_In_Group(Component _senser,object _data)
    {
        if (_data is Group_Image)
        {
            Delete_All_Content();
            var sp =  _data as Group_Image;
            foreach (var g in sp.sprite_datas)
            {
                Create_Image(g);
            }
        }




    }


}
