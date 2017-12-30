using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLevel : MonoBehaviour
{
    public float SecondsLevelLasting { get; set; }
    public float RemainedLevelSeconds { get; set; }
    public string RemainedTime { get; set; }
    private WaitForSeconds delayLevelTime;

    public delegate void Event();
    public event Event IsTimeChanged;
    public event Event IsEnded;


    private void Awake()
    {
        delayLevelTime = new WaitForSeconds(1f);
    }

    public void StartTime()
    {
        RemainedLevelSeconds = SecondsLevelLasting;
        GetRemainedTime();
        StartCoroutine(WorkingLevelTime());
    }

    public void EndTime()
    {
        StopCoroutine(WorkingLevelTime());
        IsEnded.Invoke();
    }

    public void GetRemainedTime()
    {
        int minutes = (int)(RemainedLevelSeconds / 60);
        int seconds = (int)(RemainedLevelSeconds % 60);
        IsTimeChanged.Invoke();
        RemainedTime = minutes.ToString() + " : " + seconds.ToString();
    }

    private IEnumerator WorkingLevelTime()
    {
        while (RemainedLevelSeconds >= 0)
        {
            if (RemainedLevelSeconds == 0)
            {
                EndTime();
            }
            else
            {
                RemainedLevelSeconds--;
                GetRemainedTime();
            }
            yield return delayLevelTime;
        }
    }

}
