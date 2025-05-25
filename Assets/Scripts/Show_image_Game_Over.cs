using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Constraints;

public class Show_image_Game_Over : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text name_text;
    [SerializeField] private TMP_Text text_btn;
    
    [Header("Value")]
    [SerializeField] private SpriteData_Value select_image;


   
    private bool toggle = false;


    public void Show_Image()
    {
        if (name_text) name_text.text = select_image.Value.name;
        image.sprite = select_image.Value.image;
        toggle = true;
        text_btn.text = "Hide";
    }

    public void Hide_Image()
    {
        if (name_text) name_text.text = "NAME";
        image.sprite = null;
        toggle = false;
        text_btn.text = "Show";
    }

    // Call With Event
    public void Toggle()
    {
        if (toggle)
            Hide_Image();
        else
            Show_Image();


    }





}
