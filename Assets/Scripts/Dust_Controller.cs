
using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;


public class DustAlphaWrapper
{
    public List<float> dust_alpha = new List<float>();
}
[System.Serializable]
public class DustDataWrapper
{
    public List<Vector2> dust_Positions = new List<Vector2>();
    public List<Vector3> dust_Rotation = new List<Vector3>();
}
public class Dust_Controller : MonoBehaviour
{
    //   [SerializeField] private RectTransform target_image;
    public static Dust_Controller Instance;

    [SerializeField] private Dust_Controller_Setting setting;
    [SerializeField] private SpriteData_Value select_image;

    [SerializeField] private RectTransformValue target_image;

    private List<Vector2> dust_Positions = new List<Vector2>();
    private List<Vector3> dust_Rotation = new List<Vector3>();
    private List<Dust> dust_obj = new List<Dust>();


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

    // Event Call
    public void Create()
    {
        SpawnDusts();
    }
    public void Clear_Dust()
    {
        foreach (var T in dust_obj)
        {
            Destroy(T.gameObject);
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
        Rect bg_Rect = target_image.Value.rect;

        for (int i = 0; i < setting.number_Of_Dusts; i++)
        {
            Vector2 randomPos = GetValidPosition(bg_Rect);


            var dust = Instantiate(setting.dust_prefap, target_image.Value);
            var dust_s = dust.GetComponent<Dust>();
            dust_s.Set_Position(randomPos);
            var rotate = dust_s.Random_Rotation();
            dust_obj.Add(dust_s);
            dust_Positions.Add(randomPos);
            dust_Rotation.Add(rotate);
        }
        DustDataWrapper dustDataWrapper = new DustDataWrapper();
        dustDataWrapper.dust_Positions = dust_Positions;
        dustDataWrapper.dust_Rotation = dust_Rotation;
        string dustDataJson = JsonUtility.ToJson(dustDataWrapper);
        photonView.RPC("SpawnDustOther", RpcTarget.Others, dustDataJson);
    }

    private void SpawnDusts_FormData(Vector2[] _dustPosition, Vector3[] _dustRotation)

    {
        Clear_Dust();
        Rect bg_Rect = target_image.Value.rect;

        for (int i = 0; i < setting.number_Of_Dusts; i++)
        {

            var dust = Instantiate(setting.dust_prefap, target_image.Value);
            var dust_s = dust.GetComponent<Dust>();
            dust_s.Set_Position(_dustPosition[i]);
            dust_s.Set_Rotation(_dustRotation[i]);
            dust_obj.Add(dust_s);

        }
    }
    [PunRPC]
    private void SpawnDustOther(string _jsonData)
    {
        DustDataWrapper dustDataWrapper = JsonUtility.FromJson<DustDataWrapper>(_jsonData);
        dust_Positions = dustDataWrapper.dust_Positions;
        dust_Rotation = dustDataWrapper.dust_Rotation;
        SpawnDusts_FormData(dust_Positions.ToArray(), dust_Rotation.ToArray());

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



    public void SendDustAlpha()
    {
        DustAlphaWrapper dustAlphaWrapper = new DustAlphaWrapper();

        foreach (var dust in dust_obj)
        {
            dustAlphaWrapper.dust_alpha.Add(dust.Get_AlphaCount);
            Debug.Log(dust.Get_AlphaCount);
            dust.ResetAlphaCount();
        }

        string jsonData = JsonUtility.ToJson(dustAlphaWrapper);
        photonView.RPC("ReceiveDustAlpha", RpcTarget.Others, jsonData);
    }

    [PunRPC]
    private void ReceiveDustAlpha(string _jsonData)
    {

        DustAlphaWrapper dustAlphaWrapper = JsonUtility.FromJson<DustAlphaWrapper>(_jsonData);

        for (int i = 0; i < dustAlphaWrapper.dust_alpha.Count; i++)
        {
            dust_obj[i].SetDustAlpha(dustAlphaWrapper.dust_alpha[i]);
            Debug.Log(dustAlphaWrapper.dust_alpha[i]);
        }
    }
}
