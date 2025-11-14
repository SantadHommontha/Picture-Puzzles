using UnityEngine;
using UnityEngine.UI;

public class RNG : MonoBehaviour
{
    private static RNG _instance;
    public static RNG Instance => _instance;
    public Image targetImage;    // the UI Image on scene
    public Image[] images;
    public GameObject PicCfm;
    void Start()
    {
        _instance = this;


    }

    void Update()
    {

    }

    public void RandomImage()
    {
        int x = GetRandomNumber();
        targetImage.sprite = images[x].sprite;
        PicCfm.SetActive(true);
    }
    public Texture2D GetTexture2D()
    {
        var n = GetRandomNumber();
        return images[n].sprite.texture; ;
    }
    public int GetRandomNumber()
    {
        int roll = Random.Range(0, 6); // gives 0-5
        return roll;
    }
}
