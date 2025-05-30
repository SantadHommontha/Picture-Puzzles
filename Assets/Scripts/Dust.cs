using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dust : MonoBehaviour, IDust, IPointerEnterHandler
{
    [SerializeField] private Color[] colors;
    [SerializeField] private Dust_Setting setting;

    private RectTransform rectTransform;
    private Image image;

    private Color start_color;
    private float alpha = 255;
    public float Get_AlphaCount
    {
        get
        {
            float n = alphaCount;
            alphaCount = 0;
            return n;
        }
    }
    private float alphaCount = 0;
    private bool isDragging = false;
    [SerializeField] private bool usefirstColor = false;

    [Header("Value")]
    [SerializeField] private BoolValue game_start;
    public Dust_Controller dust_Controller;
    public bool is_dead = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        start_color = image.color;
    }



    public void Random_Sprite()
    {
        image.sprite = setting.dust_sprites[UnityEngine.Random.Range(0, setting.dust_sprites.Length)];
    }
    public void Random_Color()
    {
        if (usefirstColor)
            image.color = colors[0];
        else
            image.color = colors[UnityEngine.Random.Range(0, colors.Length)];
    }
    public Vector3 Random_Rotation()
    {
        var rt = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));
        Set_Rotation(rt);
        return rt;
    }

    public void Set_Rotation(Vector3 _rotate)
    {
        rectTransform.rotation = Quaternion.Euler(_rotate);
    }
    public void Set_Position(float _x, float _y)
    {
        Set_Position(new Vector2(_x, _y));
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
        Debug.Log("SetDustAlpha: " + alphaCount);
        DecressDust();
    }
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


    public void SetUp()
    {
        Random_Sprite();
        Random_Color();
        Random_Rotation();
    }

    // void Start()
    // {
    //     SetUp();
    // }



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
}