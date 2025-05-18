using UnityEngine.UI;
using UnityEngine;

public class Show_image : MonoBehaviour
{


    [SerializeField] private Image image;
    [SerializeField] private SpriteData_Value select_image;


    void Start()
    {
        select_image.OnValueChange += Show_Image;
    }

    private void Show_Image(SpriteData _spriteData)
    {

        image.sprite = _spriteData.image;

    }

    // Call With Event
    public void Show_Image()
    {

        image.sprite = select_image.Value.image;

    }

    void OnEnable()
    {
        Show_Image();
    }


    void OnDestroy()
    {
        select_image.OnValueChange -= Show_Image;
    }













}
