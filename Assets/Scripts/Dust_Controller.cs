
using UnityEngine;
using System.Collections.Generic;


public class Dust_Controller : MonoBehaviour
{
    //   [SerializeField] private RectTransform target_image;
    public static Dust_Controller instance;

    [SerializeField] private Dust_Controller_Setting setting;
    [SerializeField] private SpriteData_Value select_image;

    [SerializeField] private RectTransformValue target_image;

    private List<Vector2> dust_Positions = new List<Vector2>();
    private List<GameObject> dust_obj = new List<GameObject>();
    void Awake()
    {

        //  target_image = GetComponent<RectTransform>();
    }
    void Start()
    {
        // select_image.OnValueChange += Create;
    }

    // Event Call
    public void Create()
    {
        SpawnDusts();
    }
    public void Clear_Dust()
    {
        foreach (var T in dust_obj)
        {
            Destroy(T);
        }
    }
    private void SpawnDusts()
    {
        Clear_Dust();
        dust_Positions.Clear();
        Rect bg_Rect = target_image.Value.rect;

        for (int i = 0; i < setting.number_Of_Dusts; i++)
        {
            Vector2 randomPos = GetValidPosition(bg_Rect);


            var dust = Instantiate(setting.dust_prefap, target_image.Value);
            var dust_s = dust.GetComponent<Dust>();
            dust_s.Set_Position(randomPos);
            dust_obj.Add(dust);
            dust_Positions.Add(randomPos);
        }

    }
    private Vector2 GetValidPosition(Rect bgRect)
    {
        Vector2 randomPos;
        int count = 0;

        do
        {
            float randomX = Random.Range(bgRect.xMin, bgRect.xMax);
            float randomY = Random.Range(bgRect.yMin, bgRect.yMax);
            randomPos = new Vector2(randomX, randomY);
            count++;
            if (count > 9)
            {
                return randomPos;
            }
        } while (IsPositionOccupied(randomPos));

        return randomPos;
    }


    private bool IsPositionOccupied(Vector2 position)
    {
        foreach (Vector2 dustPos in dust_Positions)
        {

            if (Vector2.Distance(dustPos, position) < setting.min_Distance)
            {
                return true;
            }
        }
        return false;
    }
}
