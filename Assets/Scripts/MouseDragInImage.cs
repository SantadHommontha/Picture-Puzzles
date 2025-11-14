using UnityEngine;

public class MouseDragInImage : MonoBehaviour
{

    void OnMouseDown()
    {
        PixelatedHandle.Instance.OnMouseDragForOther();
    }
}
