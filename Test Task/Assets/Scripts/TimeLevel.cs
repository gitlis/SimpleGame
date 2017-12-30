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
    public event Event TimeChanged;
    public event Event Ended;


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
    }

    public void GetRemainedTime()
    {
        int minutes = (int)(RemainedLevelSeconds / 60);
        int seconds = (int)(RemainedLevelSeconds % 60);
        TimeChanged.Invoke();
        RemainedTime = minutes.ToString() + " : " + seconds.ToString();
    }

    private IEnumerator WorkingLevelTime()
    {
        while (RemainedLevelSeconds >= 0)
        {
            if (RemainedLevelSeconds == 0)
            {
                EndTime();
                Ended.Invoke();
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
