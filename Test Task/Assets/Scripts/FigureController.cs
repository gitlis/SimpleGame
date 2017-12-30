using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureController : BaseController
{
    private Transform figure;
    private SpriteRenderer sRenderer;
    private TimerController timer;
    private FigureMouseController figureMouse;

    [SerializeField]
    public Color Color { get; set; }
    [SerializeField]
    public FigureType TypeFigure { get; set; }

    public enum FigureType
    {
        Square,
        Circle
    }

    void Awake ()
    {
        figure = GetComponent<Transform>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (TypeFigure == FigureType.Square) figureMouse = figure.gameObject.GetComponentInChildren<FigureMouseController>();
        if (TypeFigure == FigureType.Circle) timer = figure.gameObject.GetComponentInChildren<TimerController>();
        On();
    }

    public void SetColor(Color color)
    {
        Color = color;
        sRenderer.color = Color;
    }

    public override void On()
    {
        base.On();
        if (timer != null) timer.On();
        if (figureMouse != null) figureMouse.On();
    }

    void FixedUpdate()
    {
        if (TypeFigure == FigureType.Circle)
        {
            if (!timer.Enabled) Off();

        }

        if (TypeFigure == FigureType.Square)
        {
            if (!figureMouse.Enabled) Off();
        } 
    }

}
