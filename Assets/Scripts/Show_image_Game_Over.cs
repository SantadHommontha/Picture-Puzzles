using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Constraints;

public class Show_image_Game_Over : MonoBehaviour
{
    [SerializeField] private Image image_show;
    [SerializeField] private GameObject image_with_dust;
    [SerializeField] private TMP_Text name_text;
    [SerializeField] private TMP_Text text_btn;
    

    [Header("Value")]
    [SerializeField] private SpriteData_Value select_image;



    private bool toggle = false;

    public void LoadImage()
    {
        image_show.sprite = select_image.Value.image;
    }
    public void Show_Image()
    {
        if (name_text) name_text.text = select_image.Value.name;
        LoadImage();
        image_show.gameObject.SetActive(true);
        image_with_dust.SetActive(false);
        toggle = true;
        text_btn.text = "Hide";
    }

    public void Hide_Image()
    {
        if (name_text) name_text.text = "NAME";
        image_show.gameObject.SetActive(false);
        image_with_dust.SetActive(true);
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
