using System.Collections.Generic;

using Photon.Pun;
using UnityEngine;

//[CreateAssetMenu(fileName = "Group_Image_Controller", menuName = "Scriptable Objects/Group_Image_Controller")]
public class Group_Image_Controller : MonoBehaviour
{
    public static Group_Image_Controller Instance;
    private PhotonView photonView;
    [SerializeField] private Group_Image[] group_Images;

    [Header("Setting")]
    [SerializeField] private Group_Image_Controller_Setting setting;

    // [SerializeField] private GameObject score_area;
    [SerializeField] private GameObject score_sontent;

    [Header("Value")]
    [SerializeField] private Select_Group_Value select_Group_Value;
    [SerializeField] private BoolValue back_btn;
    [SerializeField] private SpriteData_Value sprite_Data;

    private Dictionary<string, SpriteData> allSpriteData = new Dictionary<string, SpriteData>();
    

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        if (photonView == null)
            photonView = GetComponent<PhotonView>();

        LoadAllSpriteToDictionary();

    }
    private void LoadAllSpriteToDictionary()
    {
        allSpriteData.Clear();
        foreach (var gi in group_Images)
        {
            foreach (var sd in gi.sprite_datas)
            {
                
                allSpriteData.Add(sd.name, sd);
            }
        }
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


    public void Delete_All_Content()
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


    // Call With Event
    public void Random_Image()
    {

        var sp = select_Group_Value.Value.sprite_datas[UnityEngine.Random.Range(0, select_Group_Value.Value.sprite_datas.Count)];

        sprite_Data.Value = sp;
    }

    public void Show_Image_In_Group(Component _senser, object _data)
    {
        if (_data is Group_Image)
        {
            Delete_All_Content();
            var sp = _data as Group_Image;
            foreach (var g in sp.sprite_datas)
            {
                Create_Image(g);
            }
        }




    }
    public void SendImageNameScele()
    {
        photonView.RPC("ReviceImageNameScele", RpcTarget.Others, sprite_Data.Value.name);
    }

    [PunRPC]
    private void ReviceImageNameScele(string _imageName)
    {
        sprite_Data.Value = allSpriteData[_imageName];
    }
}
