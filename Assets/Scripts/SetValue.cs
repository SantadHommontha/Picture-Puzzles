using UnityEngine;

public class SetValue : MonoBehaviour
{
    [SerializeField] private RectTransformValue target_image;
    [SerializeField] private RectTransform rectTransform;


    void Awake()
    {
        if (!rectTransform)
            rectTransform = GetComponent<RectTransform>();
    }
    void OnEnable()
    {
        target_image.Value = rectTransform;
    }
}
