using System.Collections;
using TMPro;
using UnityEngine;

public class TimerBeforeAnser : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    private float timer = 0;
    private float countTime = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTime()
    {

    }

   private IEnumerator CountTime(float _time)
    {
        timer = _time;
        float deltaTime = 1;
        while (timer > 0)
        {
            timerText.text = timer.ToString();
            yield return new WaitForSeconds(deltaTime);
            timer -= deltaTime;

            if(timer < 1 && deltaTime > 1)
            {
                deltaTime = 0.1f;
            }
        }
    }
}
