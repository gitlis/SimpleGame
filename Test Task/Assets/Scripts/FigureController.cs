using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureController : BaseController
{
    private Transform figure;
    private SpriteRenderer sRenderer;
    private TimerController timer;

    [SerializeField]
    public Color Color { get; set; }
    [SerializeField]
    public FigureType TypeFigure { get; set; }

    public Vector3 HomePosition { get; set; }
    public bool IsCatched;

    public enum FigureType
    {
        Square,
        Circle
    }

    void Awake ()
    {
        figure = GetComponent<Transform>();
        sRenderer = GetComponent<SpriteRenderer>();
        IsCatched = false;
    }

    void Start()
    {
        if (TypeFigure == FigureType.Circle) timer = figure.GetComponentInChildren<TimerController>();
        On();
    }

    public void SetColor(Color color)
    {
        Color = color;
        sRenderer.color = Color;
    }

    public void ReturnToHome()
    {
       if (!IsCatched) StartCoroutine(ReturnFigure());
       StopCoroutine(ReturnFigure());
    }

    public override void On()
    {
        base.On();
        if (timer != null) timer.On();
    }

    public override void Off()
    {
        base.Off();
        if (timer != null) timer.Off();
    }

    void FixedUpdate()
    {
        if (TypeFigure == FigureType.Circle)
        {
            if (!timer.Enabled) Off();

        }
    }

    private IEnumerator ReturnFigure()
    {
        while (figure.position != HomePosition)
        {
            if (IsCatched) break;

            var direction = HomePosition - figure.position;
            figure.Translate(direction * Time.deltaTime);
            yield return new WaitForEndOfFrame();

        }
    }

}
