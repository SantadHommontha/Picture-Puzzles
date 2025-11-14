using UnityEngine.UI;
using UnityEngine;

public class SptiteShowItem : MonoBehaviour
{
    [SerializeField] private Image image;
    public void Show(int _index)
    {
        image.sprite = SpriteShow.currentSpriteSelete[_index];
    }
}
