using System.Collections;
using UnityEngine;


public class Timer : MonoBehaviour
{
    [SerializeField] private FloatValue game_time;
    [SerializeField] private FloatValue timer;


    private Coroutine timer_IE;

    public float GetTimer()
    {
        return timer.Value;
    }
    public void Start_Time()
    {
        if (timer_IE != null)
            StopCoroutine(timer_IE);
        timer_IE = StartCoroutine(IE_Timer(game_time.Value));
    }
    public void StopTimer()
    {
        if (timer_IE != null)
            StopCoroutine(timer_IE);
        timer_IE = null;
    }
    public void Start_Time(float _time)
    {
        if (timer_IE != null)
            StopCoroutine(timer_IE);
        timer_IE = StartCoroutine(IE_Timer(_time));
    }
    private IEnumerator IE_Timer(float _time)
    {
        Debug.Log("IE_Timer");
        timer.Value = _time;

        while (timer.Value >= 0)
        {

            timer.Value -= Time.deltaTime;
            yield return null;
        }
        timer_IE = null;
       
    }



}
