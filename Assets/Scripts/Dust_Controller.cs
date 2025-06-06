
using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;


[System.Serializable]
public class DustAlphaWrapper
{
    public List<string> dust_name = new List<string>();
    public List<float> dust_valu = new List<float>();
    public string send_playerID;
    // public Dictionary<string, float> alpha = new Dictionary<string, float>();
}
[System.Serializable]
public class DustDataWrapper
{
    public List<Vector2> dust_Positions = new List<Vector2>();
    public List<Vector3> dust_Rotation = new List<Vector3>();
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
    // private List<Dust> dust_obj = new List<Dust>();
    private Dictionary<string, Dust> dust_obj = new Dictionary<string, Dust>();

    private PhotonView photonView;


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
            Vector2 randomPos = GetValidPosition(bg_Rect);


            var dust = Instantiate(setting.dust_prefap, target_image.Value);
            var dust_s = dust.GetComponent<Dust>();
            dust_s.Set_Position(randomPos);
            dust_s.Random_NameID();
            var rotate = dust_s.Random_Rotation();
            dust_obj.Add(dust_s.dustName, dust_s);
            dust_Positions.Add(randomPos);
            dust_Rotation.Add(rotate);
            dust_Name.Add(dust_s.dustName);
        }
        DustDataWrapper dustDataWrapper = new DustDataWrapper();
        dustDataWrapper.dust_Positions = dust_Positions;
        dustDataWrapper.dust_Rotation = dust_Rotation;
        dustDataWrapper.dust_DustName = dust_Name;
        string dustDataJson = JsonUtility.ToJson(dustDataWrapper);
        photonView.RPC("SpawnDustOther", RpcTarget.Others, dustDataJson);
    }

    private void SpawnDusts_FormData(Vector2[] _dustPosition, Vector3[] _dustRotation, string[] _dustName)

    {
        Clear_Dust();
        Rect bg_Rect = target_image.Value.rect;

        for (int i = 0; i < setting.number_Of_Dusts; i++)
        {
            Debug.Log($"Dust ID: " + _dustName);
            var dust = Instantiate(setting.dust_prefap, target_image.Value);
            var dust_s = dust.GetComponent<Dust>();
            dust_s.Set_Position(_dustPosition[i]);
            dust_s.Set_Rotation(_dustRotation[i]);
            dust_s.Set_Name(_dustName[i]);
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
        SpawnDusts_FormData(dust_Positions.ToArray(), dust_Rotation.ToArray(), dust_Name.ToArray());

    }
    private Vector2 GetValidPosition(Rect bgRect)
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


        Debug.Log("SendDustAlpha");

        //  dustAlphaWrapper.dust_alpha.Clear();
        dustAlphaWrapper.dust_valu.Clear();
        dustAlphaWrapper.dust_name.Clear();
        foreach (var dust in dust_obj)
        {
            if (dust.Value.Get_AlphaCount > 0)
            {
                dustAlphaWrapper.dust_name.Add(dust.Value.name);
                dustAlphaWrapper.dust_valu.Add(dust.Value.Get_AlphaCount);
                // dustAlphaWrapper.alpha.Add(dust.Key, dust.Value.Get_AlphaCount);
                //   dustAlphaWrapper.dust_alpha.Add(dust.Get_AlphaCount);
                //  dustAlphaWrapper.dust_name.Add(dust.dustName);
            }

        }


        photonView.RPC("ReceiveDustAlpha", RpcTarget.MasterClient, SerializeDustAlphaWrapper(dustAlphaWrapper));
    }

    [PunRPC]
    private void ReceiveDustAlpha(byte[] _byteData, PhotonMessageInfo info)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("ReceiveDustAlpha");

            // DustAlphaWrapper dustAlphaWrapper = JsonUtility.FromJson<DustAlphaWrapper>(_jsonData);

            // for (int i = 0; i < dustAlphaWrapper.dust_alpha.Count; i++)
            // {
            //     if (!dust_obj[i].is_dead)
            //     {
            //         dust_obj[i].SetDustAlpha(dustAlphaWrapper.dust_alpha[i]);
            //         Debug.Log(dustAlphaWrapper.dust_alpha[i]);

            //     }
            // }

            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (player.ActorNumber != info.Sender.ActorNumber)
                {
                    photonView.RPC("UpdateDushAlpha", player, _byteData);
                }
            }



            GGG(_byteData);
        }

    }

    private void GGG(byte[] _byteData)
    {

        Debug.Log("GGG");

        //   dustAlphaWrapper.dust_alpha.Clear();
        string json = System.Text.Encoding.UTF8.GetString(_byteData);
        Debug.Log(json);
        dustAlphaWrapper = JsonUtility.FromJson<DustAlphaWrapper>(json);
        // for (int i = 0; i < dustAlphaWrapper.dust_alpha.Count; i++)
        // {
        //     if (!dust_obj[i].is_dead)
        //     {
        //         Debug.Log("---: " + dustAlphaWrapper.dust_alpha[i]);
        //         dust_obj[i].SetDustAlpha(dustAlphaWrapper.dust_alpha[i]);
        //         Debug.Log(dustAlphaWrapper.dust_alpha[i]);

        //     }
        // }
        
    }
    [PunRPC]
    private void UpdateDushAlpha(byte[] _byteData)
    {
        string _jsonData = System.Text.Encoding.UTF8.GetString(_byteData);
        DustAlphaWrapper dustAlphaWrapper = JsonUtility.FromJson<DustAlphaWrapper>(_jsonData);
        Debug.Log("UpdateDushAlpha");
        // for (int i = 0; i < dustAlphaWrapper.dust_alpha.Count; i++)
        // {
        //     if (!dust_obj[i].is_dead)
        //     {
        //         dust_obj[i].SetDustAlpha(dustAlphaWrapper.dust_alpha[i]);
        //         Debug.Log(dustAlphaWrapper.dust_alpha[i]);

        //     }
        // }

      
    }
}
