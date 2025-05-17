using UnityEngine;

[CreateAssetMenu(fileName = "Dust_Setting", menuName = "Scriptable Objects/Dust_Setting")]
public class Dust_Setting : ScriptableObject
{
    public float wipe_speed = 10;
    public Sprite[] dust_sprites;
}
