using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Show_image : MonoBehaviour
{


    [SerializeField] private Image image;
    [SerializeField] private SpriteData_Value select_image;
    [SerializeField] private TMP_Text name_text;

    void Start()
    {
        select_image.OnValueChange += Show_Image;
    }

    private void Show_Image(SpriteData _spriteData)
    {
        if (name_text) name_text.text = _spriteData.name;
        image.sprite = _spriteData.image;

    }

    // Call With Event
    public void Show_Image()
    {
        Show_Image(select_image.Value);


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
