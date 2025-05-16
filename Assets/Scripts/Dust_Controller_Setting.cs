using UnityEngine;

[CreateAssetMenu(fileName = "Dust_Setting/Dust_Controller_Setting", menuName = "Scriptable Objects/Dust_Controller_Setting")]
public class Dust_Controller_Setting : ScriptableObject
{
    public int number_Of_Dusts = 100;
    public float min_Distance = 50f;
    public GameObject dust_prefap;
}
