using UnityEngine;
using UnityEngine.UI;

public class ShowExImage : MonoBehaviour
{
    [SerializeField] private BoolValue v_showExImage;
    [SerializeField] private Image[] images;
    
    
    
    private void CheckValue(bool _value)
    {
        if (images[0] == null) return;
        if (images[0].enabled == _value ) return;
            foreach (Image image in images)
            {
              image.enabled = _value;
            }
    }
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        CheckValue(v_showExImage.Value);
    }
}
