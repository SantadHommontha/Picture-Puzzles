using UnityEngine;
using UnityEngine.UI;

public class RNG : MonoBehaviour
{
    public Image targetImage;    // the UI Image on scene
    public Image[] images;
    public GameObject PicCfm;
    void Start()
    {
        

        
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

    int GetRandomNumber()
    {
        int roll = Random.Range(0,6); // gives 0-5
        return roll;
    }
}
