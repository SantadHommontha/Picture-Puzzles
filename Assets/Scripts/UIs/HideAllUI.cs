using UnityEngine;

public class HideAllUI : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;
    public void HideAll()
    {
        foreach (var T in gameObjects)
        {
            T.SetActive(false);
        }
    }
    public void ShowAll()
    {
        foreach (var T in gameObjects)
        {
            T.SetActive(true);
        }
    }
}
