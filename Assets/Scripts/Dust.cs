using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dust : MonoBehaviour, IDust, IPointerEnterHandler
{
    // [SerializeField] private Color[] colors;
    [SerializeField] private Dust_Setting setting;

    private RectTransform rectTransform;
    private Image image;
    public string dustName;
    private Color start_color;
    private float alpha = 255;
    public float Get_AlphaCount()
    {

        float n = alphaCount;
        alphaCount = 0;
        return n;
    }
    public float alphaCount { get; private set; } = 0;

    private bool isDragging = false;
    [SerializeField] private bool usefirstColor = false;

    [Header("Value")]
    [SerializeField] private BoolValue game_start;
    public Dust_Controller dust_Controller;
    public bool is_dead = false;

    public int color_index { get; private set; } = 0;
    public int sprite_index { get; private set; } = 0;

    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public string GenerateRandomString(int length = 10)
    {
        System.Random random = new System.Random();
        char[] result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = chars[random.Next(chars.Length)];
        }
        return new string(result);
    }



#region SetUp
   
    public void Random_Sprite()
    {
        Set_Sprite_By_Index(UnityEngine.Random.Range(0, setting.dust_sprites.Length));
    }
    public void Set_Sprite_By_Index(int _index)
    {
        sprite_index = _index;
        image.sprite = setting.dust_sprites[sprite_index];
    }
    public void Random_Color()
    {
        if (usefirstColor)
            Set_Color_By_Index(0);
        else
            Set_Color_By_Index(UnityEngine.Random.Range(0, setting.dust_colors.Length));
    }
    public void Set_Color_By_Index(int _index)
    {
        color_index = _index;
        image.color = setting.dust_colors[color_index];
    }
    public Vector3 Random_Rotation()
    {
        var rt = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));
        Set_Rotation(rt);
        return rt;
    }

    public void Random_NameID()
    {
        dustName = GenerateRandomString();
    }
#endregion

    #region  Set Value
    public void Set_Rotation(Vector3 _rotate)
    {
        rectTransform.rotation = Quaternion.Euler(_rotate);
    }
    public void Set_Position(float _x, float _y)
    {
        Set_Position(new Vector2(_x, _y));
    }

    public void Set_Name(string _name)
    {
        dustName = _name;
    }


    public void Set_Position(Vector2 _position)
    {
        rectTransform.anchoredPosition = _position;
    }

    public void ResetAlphaCount()
    {
        alphaCount = 0;
        Debug.Log("ResetAlphaCount: " + alphaCount);
    }
    public void SetDustAlpha(float _data)
    {
        alpha -= _data;
        Debug.Log($"SetDustAlpha : {dustName} : {alphaCount}");
        DecressDust();
    }
    #endregion

    #region Wipe
    public void Wipe()
    {
        var t = setting.wipe_speed * Time.deltaTime;
        alphaCount += t;
        alpha -= t;
        DecressDust();

    }
    private void DecressDust()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, UnityEngine.Mathf.Clamp01((alpha / 255)));
        if (alpha <= 10)
        {
            //Destroy(gameObject);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            is_dead = true;
            image.raycastTarget = false;
            gameObject.SetActive(false);

        }
    }
    #endregion

    public void SetUp()
    {
        Random_Sprite();
        Random_Color();
        Random_Rotation();
        dustName = GenerateRandomString();
    }

#region  Unity Function
    void Start()
    {
        SetUp();
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        start_color = image.color;
    }
    #endregion

    #region  PointTer
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!game_start.Value) return;
        Debug.Log(name);
        if (isDragging)
        {

        }
        Wipe();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!game_start.Value) return;
        if (Input.GetMouseButton(0))
        {
            Wipe();
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Wipe();
            }
        }


    }
    #endregion
}