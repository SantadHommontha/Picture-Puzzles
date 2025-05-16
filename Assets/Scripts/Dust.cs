using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dust : MonoBehaviour,IDust ,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    [SerializeField] private Sprite[] dust_image;
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
      //  alpha = start_color.a;
      //  Debug.Log(alpha);
    }



    public void Random_Sprite()
    {
        image.sprite = dust_image[UnityEngine.Random.Range(0, dust_image.Length)];
    }


    public void Set_Position(float _x, float _y)
    {
        Set_Position(new Vector2(_x,_y));
    }


    public void Set_Position(Vector2 _position)
    {
        rectTransform.anchoredPosition = _position;
    }

    public void Wipe()
    {

    }

    void Start()
    {

    }

    void Update()
    {
        
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
            alpha -= 10;

            image.color = new Color(image.color.r, image.color.g, image.color.b, UnityEngine.Mathf.Clamp01((alpha / 255)));
            if (alpha <= 0)
            {
                gameObject.SetActive(false);
            } 
            //do
        }
    }
}
