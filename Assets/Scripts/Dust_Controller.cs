
using UnityEngine;
using System.Collections.Generic;


public class Dust_Controller : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
  
    [SerializeField] private Dust_Controller_Setting setting;

    private List<Vector2> dust_Positions = new List<Vector2>();

    void Awake()
    {

        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {

    }
    [ContextMenu("Create")]
    public void Create()
    {
        SpawnDusts();
    }
    private void SpawnDusts()
    {
        Rect bg_Rect = rectTransform.rect;

        for (int i = 0; i < setting.number_Of_Dusts; i++)
        {
            Vector2 randomPos = GetValidPosition(bg_Rect);

            
            var dust = Instantiate(setting.dust_prefap, rectTransform);
            var dust_s = dust.GetComponent<Dust>();
            dust_s.Set_Position(randomPos);
          
            dust_Positions.Add(randomPos);
        }
        dust_Positions.Clear();
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
