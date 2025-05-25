using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

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
    private IEnumerator IE_Timer(float _time)
    {
        timer.Value = _time;

        while (timer.Value >= 0)
        {

            timer.Value -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return null;
    }



}
