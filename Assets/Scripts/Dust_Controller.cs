
using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;


[System.Serializable]
public class DustAlphaWrapper
{
    public List<string> dust_name = new List<string>();
    public List<float> dust_valu = new List<float>();
    public string playerID;
    public string player_name;

}
[System.Serializable]
public class DustDataWrapper
{
    public List<Vector2> dust_Positions = new List<Vector2>();
    public List<Vector3> dust_Rotation = new List<Vector3>();
    public List<int> dust_color_index = new List<int>();
    public List<int> dust_sprite_index = new List<int>();
    public List<string> dust_DustName = new List<string>();
}
public class Dust_Controller : MonoBehaviour
{
    //   [SerializeField] private RectTransform target_image;
    public static Dust_Controller Instance;

    [SerializeField] private Dust_Controller_Setting setting;
    [SerializeField] private SpriteData_Value select_image;

    [SerializeField] private RectTransformValue target_image;

    public List<Vector2> dust_Positions = new List<Vector2>();
    public List<Vector3> dust_Rotation = new List<Vector3>();
    public List<string> dust_Name = new List<string>();
    public List<int> dust_color_index = new List<int>();
    public List<int> dust_sprite_index = new List<int>();
    private Dictionary<string, Dust> dust_obj = new Dictionary<string, Dust>();

    private PhotonView photonView;
    [Space]
    [Header("Value")]
    [SerializeField] private StringValue player_name;

    public void SetUp()
    {
        Clear_Dust();
        dust_Positions.Clear();
    }
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
        //  target_image = GetComponent<RectTransform>();
    }
    void Start()
    {
        if (photonView == null)
            photonView = GetComponent<PhotonView>();
        // select_image.OnValueChange += Create;
    }
    public void RemoveDust(Dust dust)
    {
        dust_obj.Remove(dust.dustName);
    }
    // Event Call
    public void Create()
    {
        SpawnDusts();
    }
    public void Clear_Dust()
    {
        foreach (var T in dust_obj)
        {
            Destroy(T.Value.gameObject);
        }
        dust_obj.Clear();
        dust_Positions.Clear();
        dust_Rotation.Clear();
        dust_color_index.Clear();
        dust_sprite_index.Clear();
    }
    private void SpawnDusts()
    {
        Clear_Dust();
        dust_Positions.Clear();
        dust_Rotation.Clear();
        dust_Name.Clear();
        Rect bg_Rect = target_image.Value.rect;

        for (int i = 0; i < setting.number_Of_Dusts; i++)
        {
            Vector2 randomPos = RamdonPositionInRect(bg_Rect);


            var dust = Instantiate(setting.dust_prefap, target_image.Value);
            var dust_s = dust.GetComponent<Dust>();
            dust_s.Set_Position(randomPos);
            dust_s.Random_NameID();
            var rotate = dust_s.Random_Rotation();
            dust_s.Random_Color();
            dust_s.Random_Sprite();

            dust_Positions.Add(randomPos);
            dust_Rotation.Add(rotate);
            dust_Name.Add(dust_s.dustName);
            dust_color_index.Add(dust_s.color_index);
            dust_sprite_index.Add(dust_s.sprite_index);
            dust_obj.Add(dust_s.dustName, dust_s);
        }
        DustDataWrapper dustDataWrapper = new DustDataWrapper();
        dustDataWrapper.dust_Positions = dust_Positions;
        dustDataWrapper.dust_Rotation = dust_Rotation;
        dustDataWrapper.dust_DustName = dust_Name;
        dustDataWrapper.dust_color_index = dust_color_index;
        dustDataWrapper.dust_sprite_index = dust_sprite_index;
        string dustDataJson = JsonUtility.ToJson(dustDataWrapper);
        photonView.RPC("SpawnDustOther", RpcTarget.Others, dustDataJson);
    }

    private void SpawnDusts_FormData(Vector2[] _dustPosition, Vector3[] _dustRotation, string[] _dustName, int[] _color_index, int[] _sprite_index)

    {
        Clear_Dust();
        Rect bg_Rect = target_image.Value.rect;

        for (int i = 0; i < setting.number_Of_Dusts; i++)
        {
          //  Debug.Log($"Dust ID: " + _dustName);
            var dust = Instantiate(setting.dust_prefap, target_image.Value);
            var dust_s = dust.GetComponent<Dust>();
            dust_s.Set_Position(_dustPosition[i]);
            dust_s.Set_Rotation(_dustRotation[i]);
            dust_s.Set_Name(_dustName[i]);
            dust_s.Set_Color_By_Index(_color_index[i]);
            dust_s.Set_Sprite_By_Index(_sprite_index[i]);
            dust_obj.Add(dust_s.dustName, dust_s);

        }
    }
    [PunRPC]
    private void SpawnDustOther(string _jsonData)
    {
        DustDataWrapper dustDataWrapper = JsonUtility.FromJson<DustDataWrapper>(_jsonData);
        dust_Positions = dustDataWrapper.dust_Positions;
        dust_Rotation = dustDataWrapper.dust_Rotation;
        dust_Name = dustDataWrapper.dust_DustName;
        dust_color_index = dustDataWrapper.dust_color_index;
        dust_sprite_index = dustDataWrapper.dust_sprite_index;
        SpawnDusts_FormData(dust_Positions.ToArray(), dust_Rotation.ToArray(), dust_Name.ToArray(), dust_color_index.ToArray(), dust_sprite_index.ToArray());

    }
    private Vector2 RamdonPositionInRect(Rect bgRect)
    {
        Vector2 randomPos;
        int count = 0;

        do
        {
            float randomX = Random.Range(bgRect.xMin, bgRect.xMax);
            float randomY = Random.Range(bgRect.yMin, bgRect.yMax);
            randomPos = new Vector2(randomX, randomY);
            count++;
            if (count > 9)
            {
                return randomPos;
            }
        } while (IsPositionOccupied(randomPos));

        return randomPos;
    }


    private bool IsPositionOccupied(Vector2 position)
    {
        foreach (Vector2 dustPos in dust_Positions)
        {

            if (Vector2.Distance(dustPos, position) < setting.min_Distance)
            {
                return true;
            }
        }
        return false;
    }

    public void RequitDustAlpha()
    {
        //  if (!PhotonNetwork.IsMasterClient) return;

        Debug.Log("RequitDustAlpha");
        photonView.RPC("SendDustAlpha", RpcTarget.Others);
    }
    DustAlphaWrapper dustAlphaWrapper = new DustAlphaWrapper();
    private byte[] SerializeDustAlphaWrapper(DustAlphaWrapper _dustAlphaWrapper)
    {
        string jsonData = JsonUtility.ToJson(dustAlphaWrapper);
        return System.Text.Encoding.UTF8.GetBytes(jsonData);
    }

    public void SendDustAlpha()
    {
        dustAlphaWrapper.dust_valu.Clear();
        dustAlphaWrapper.dust_name.Clear();

        foreach (var dust in dust_obj)
        {
            if (dust.Value.alphaCount > 0)
            {
                dustAlphaWrapper.dust_name.Add(dust.Value.dustName);
                dustAlphaWrapper.dust_valu.Add(dust.Value.Get_AlphaCount());
                dustAlphaWrapper.playerID = PhotonNetwork.LocalPlayer.UserId;
                dustAlphaWrapper.player_name = player_name.Value;
            }
        }
        if (dustAlphaWrapper.dust_name.Count > 0)
            photonView.RPC("ReceiveDustAlpha", RpcTarget.MasterClient, SerializeDustAlphaWrapper(dustAlphaWrapper));
    }

    [PunRPC]
    private void ReceiveDustAlpha(byte[] _byteData, PhotonMessageInfo info)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (player.ActorNumber != info.Sender.ActorNumber)
                {
                    photonView.RPC("UpdateDushAlpha", player, _byteData);
                }
            }
            // UpdateDushAlpha(_byteData);
        }

    }

    private void GGG(byte[] _byteData)
    {
        string json = System.Text.Encoding.UTF8.GetString(_byteData);
        Debug.Log(json);
        dustAlphaWrapper = JsonUtility.FromJson<DustAlphaWrapper>(json);
        for (int i = 0; i < dustAlphaWrapper.dust_name.Count; i++)
        {
            Debug.Log($"Set Dust {dustAlphaWrapper.dust_name[i]} : {dustAlphaWrapper.dust_valu[i]}");
            dust_obj[dustAlphaWrapper.dust_name[i]].SetDustAlpha(dustAlphaWrapper.dust_valu[i]);
        }

    }
    [PunRPC]
    private void UpdateDushAlpha(byte[] _byteData)
    {
        string _jsonData = System.Text.Encoding.UTF8.GetString(_byteData);
        DustAlphaWrapper dustAlphaWrapper = JsonUtility.FromJson<DustAlphaWrapper>(_jsonData);
        for (int i = 0; i < dustAlphaWrapper.dust_name.Count; i++)
        {
            Debug.Log($"Set Dust {dustAlphaWrapper.dust_name[i]} : {dustAlphaWrapper.dust_valu[i]}");
            dust_obj[dustAlphaWrapper.dust_name[i]].SetDustAlpha(dustAlphaWrapper.dust_valu[i]);
        }
    }
}
