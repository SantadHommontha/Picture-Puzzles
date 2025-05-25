using UnityEngine;

public class Show_Hide_Image_Btn : MonoBehaviour
{
    [SerializeField] private GameObject target_obj;
    [SerializeField] private bool start_hide;

    private bool toggle = false;
    void Start()
    {
        Set_UP();
    }


    private void Set_UP()
    {
        if (start_hide)
            Hide();
        else
            Show();
    }
    public void Show()
    {

        target_obj.SetActive(true);
        toggle = true;

    }


    public void Hide()
    {

        target_obj.SetActive(false);
        toggle = false;
    }

    public void Toggle()
    {
        if (toggle)
            Hide();
        else
            Show();
    }
}
