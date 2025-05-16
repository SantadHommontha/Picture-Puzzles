
using UnityEngine;
using System.Collections.Generic;


public class Dust_Controller : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private GameObject dustPrefab;
    [SerializeField] private int numberOfDusts = 100;
    [SerializeField] private float minDistance = 50f;

    private List<Vector2> dustPositions = new List<Vector2>();

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
        Rect bgRect = rectTransform.rect;

        for (int i = 0; i < numberOfDusts; i++)
        {
            Vector2 randomPos = GetValidPosition(bgRect);

            // สร้างฝุ่นและตั้งตำแหน่ง
            GameObject dust = Instantiate(dustPrefab, rectTransform);
            var dust_s = dust.GetComponent<Dust>();
            dust_s.Set_Position(randomPos);
            // RectTransform dustRect = dust.GetComponent<RectTransform>();
            // dustRect.anchoredPosition = randomPos;

            // เก็บตำแหน่ง
            dustPositions.Add(randomPos);



        }
        dustPositions.Clear();
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
        foreach (Vector2 dustPos in dustPositions)
        {

            if (Vector2.Distance(dustPos, position) < minDistance)
            {
                return true;
            }
        }
        return false;
    }
}
