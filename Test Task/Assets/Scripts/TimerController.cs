using UnityEngine;
using UnityEngine.UI;

public class TimerController : BaseController
{
    private float timerTime;
    private Slider slider;

    [SerializeField]
    public float TimerSecondsLasting { get; set; }

    void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    private void RunTimer()
    {
        timerTime += Time.deltaTime;
        slider.value = 1 - timerTime / TimerSecondsLasting;
        if (slider.value <= 0) Off();
    }

    private void StartTimer()
    {
        timerTime = 0;
    }

    public override void On()
    {
        base.On();
        slider.value = 1;
        if (TimerSecondsLasting == 0) TimerSecondsLasting = 10f;
        StartTimer();
    }

    void FixedUpdate()
    {
       if (Enabled) RunTimer();
    }

    internal void SetLasting(float lasing)
    {
        TimerSecondsLasting = lasing;
    }
}
