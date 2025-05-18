using UnityEngine;
using UnityEngine.UI;

public class Random_Btn : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private BoolValue random_btn;



    [Space]
    
    [SerializeField] private bool start_true = true;
    void Start()
    {
        if(start_true)
            On_Toggle_Update(true);
    }







    public void On_Toggle_Update(bool _bool)
    {
        random_btn.Value = _bool;
    }
}
