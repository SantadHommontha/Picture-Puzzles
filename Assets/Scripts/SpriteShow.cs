using System.Collections.Generic;
using UnityEngine;

public class SpriteShow : MonoBehaviour
{
    public Sprite[] sprites;
    [SerializeField] private SptiteShowItem[] sptiteShowItem;
    public int index;
    public static Sprite[] currentSpriteSelete;
    public static int currentIndex;
    public static List<SpriteShow> allSpriteShow = new List<SpriteShow>();
    
    void Start()
    {
        if (!allSpriteShow.Contains(this))
            allSpriteShow.Add(this);
    }

    public Sprite GetSprite(int _index)
    {
        SelectSpriteGround();
        if (_index < sprites.Length)
        {
            return sprites[_index];
        }
        else
        {
            return null;
        }


    }
    public void ShowItem()
    {
        for (int i = 0; i < sptiteShowItem.Length; i++)
        {
            sptiteShowItem[i].Show(i);
        }
    }
    public void SelectSpriteGround()
    {
        currentSpriteSelete = sprites;
        currentIndex = index;
    }

    // public static Sprite GetSpriteByIndex(int _index)
    // {

    //     if (_index == index)
    //     {

    //     }
    //   }

}
