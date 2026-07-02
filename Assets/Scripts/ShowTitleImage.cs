using UnityEngine;

using UnityEngine.UI;

public class ShowTitleImage : MonoBehaviour
{
    [SerializeField] private BoolValue v_showExImage;

    [Space]
    [SerializeField] private Image image;

    [Space]
    [SerializeField] private bool invertValue;



    private void OnValueChange(bool _value)
    {
        print($"ggg {_value}");
        image.enabled = invertValue ? !_value:  _value;
    }

    private void OnEnable()
    {
        v_showExImage.OnValueChange += OnValueChange;
        
        OnValueChange(v_showExImage.Value);


    }

    private void OnDisable()
    {
        v_showExImage.OnValueChange -= OnValueChange;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
