using System.Collections;
using TMPro;
using UnityEngine;

public class CountTimeForAnwer : MonoBehaviour
{
    [SerializeField] private float time = 10f;
    [SerializeField] private TMP_Text tmp_text;
    private Coroutine ct;
    public GameEvent afterCountTimeEvent;
 

    public void StartTimer(float _time)
    {
        if (ct != null)
        {
            StopCoroutine(ct);
        }
        ct = StartCoroutine(CountTime(_time));
    }
    public void StartTimer()
    {
        StartTimer(time);
    }
    public void Stopimer()
    {
        StopCoroutine(ct);
    }
   
    private IEnumerator CountTime(float _timer)
    {
        string stringFormat = "F0";
        while(_timer > 0)
        {
            _timer -= Time.deltaTime;
            tmp_text.text = _timer.ToString(stringFormat);
            if(_timer < 1)
            {
             stringFormat = "F1";
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        afterCountTimeEvent?.Raise(this,-979);
    }
}

