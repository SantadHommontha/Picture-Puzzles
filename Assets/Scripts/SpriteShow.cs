using UnityEngine;

public class SpriteShow : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    public static Sprite[] currentSpriteSelete;
    public Sprite GetSprite(int _index)
    {
        if (_index < sprites.Length)
        {
            return sprites[_index];
        }
        else
        {
            return null;
        }

        currentSpriteSelete = sprites;
    }
}
