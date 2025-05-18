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

    private bool isDragging = false;


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
        image.color = colors[UnityEngine.Random.Range(0, colors.Length)];
    }
    public void Random_Rotation()
    {
        var rt = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));
        rectTransform.rotation = Quaternion.Euler(rt);
    }

    public void Set_Position(float _x, float _y)
    {
        Set_Position(new Vector2(_x, _y));
    }


    public void Set_Position(Vector2 _position)
    {
        rectTransform.anchoredPosition = _position;
    }

    public void Wipe()
    {
        alpha -= setting.wipe_speed * Time.deltaTime;

        image.color = new Color(image.color.r, image.color.g, image.color.b, UnityEngine.Mathf.Clamp01((alpha / 255)));
        if (alpha <= 10)
        {
            Destroy(gameObject);
        }
    }


    public void SetUp()
    {
        Random_Sprite();
        Random_Color();
        Random_Rotation();
    }

    void Start()
    {
        SetUp();
    }



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
        Debug.Log(name);
        if (isDragging)
        {


        }
        Wipe();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

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
