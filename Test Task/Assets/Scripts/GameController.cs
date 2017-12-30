using System.Collections;
using System.Linq;
using UnityEngine;

public class GameController : BaseController
{
    public UIManager uiManager;

    public Transform camera2D;
    private Transform canvas;

    private int countOfSquares;
    private int countOfCircles;
    private GameObject squaresBox;
    private GameObject circlesBox;

    private FigureController[] squaresOnScene;
    private FigureController[] circlesOnScene;
    public GameSettings.Settings settings;
    public bool IsLevelDone = false;


    void Awake()
    {
        camera2D = Camera.main.transform;
        canvas = GameObject.Find("Canvas").transform;

        countOfSquares = 3;
        countOfCircles = 3;
        squaresOnScene = new FigureController[countOfSquares];
        circlesOnScene = new FigureController[countOfCircles];
        settings = new GameSettings().SettingsGame;
    }

    public void StartGame()
    {
        CreateSquares(countOfSquares);
        CreateCircles(countOfCircles);
        IsLevelDone = true;
    }

    public void EndGame()
    {
        Destroy(squaresBox, 1f);
        Destroy(circlesBox, 1f);
    }

    public override void On()
    {
        base.On();
        if (squaresBox != null)
        {
            foreach (var figure in squaresOnScene)
                figure.On();

            squaresBox.gameObject.SetActive(true);
        }
        if (circlesBox != null)
        {
            foreach (var figure in circlesOnScene)
                figure.On();

            circlesBox.gameObject.SetActive(true);
        }
    }

    public override void Off()
    {
        base.Off();
        if (squaresBox != null)
        {
            foreach (var figure in squaresOnScene)
                figure.Off();

            squaresBox.gameObject.SetActive(false);
        }
        if (circlesBox != null)
        {
            foreach (var figure in squaresOnScene)
                figure.Off();

            circlesBox.gameObject.SetActive(false);
        }
    }

    public void CreateSquares(int count)
    {
        squaresBox = new GameObject();
        squaresBox.transform.parent = canvas;
        squaresBox.name = "Squares";

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(-30, 30 * (i - 1), 3);
            Vector3 position = camera2D.position + offset;

            var square = CreateFigure(FigureController.FigureType.Square, position);
            square.transform.SetParent(squaresBox.transform);

            if (squaresOnScene != null) squaresOnScene[i] = square.GetComponent<FigureController>();
            else i--;

        }
    }

    public void CreateCircles(int count)
    {
        circlesBox = new GameObject();
        circlesBox.transform.parent = canvas;
        circlesBox.name = "Circles";

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(30, 30 * (i - 1), 3);
            Vector3 position = camera2D.position + offset;

            var circle = CreateFigure(FigureController.FigureType.Circle, position);
            circle.transform.SetParent(circlesBox.transform);

            if (circlesOnScene != null) circlesOnScene[i] = circle.GetComponent<FigureController>();
            else i--;

        }
    }

    private GameObject CreateFigure(FigureController.FigureType figureType, Vector3 position)
    {
        GameObject prefab = null; 
        if (figureType == FigureController.FigureType.Square) prefab = PrefabStore.Instance.GetSquarePrefab();
        if (figureType == FigureController.FigureType.Circle) prefab = PrefabStore.Instance.GetCicrlePrefab();

        var figure = Instantiate(prefab, position, Quaternion.identity);

        var figureController = figure.AddComponent<FigureController>();
        figureController.TypeFigure = figureType;
        figureController.HomePosition = position;
        SetNewColor(figureController);

        if (figureType == FigureController.FigureType.Circle)
        {
            var timer = figure.AddComponent<TimerController>();
            SetNewTimerLasting(timer);
        }

        return figure;
    }

    private void DeleteFigure(GameObject figure)
    {
        Destroy(figure, 0.1f);
    }

    public void ReplaceFigure(GameObject figure)
    {
        var figureBox = figure.transform.parent; ;
        var figureController = figure.GetComponent<FigureController>();
        var figureType = figureController.TypeFigure;
        var figureHomePosition = figureController.HomePosition;

        var ListOfFiguresType = (figureType == FigureController.FigureType.Square) ? squaresOnScene : circlesOnScene;
        var indexOfLastFigure = ListOfFiguresType.ToList().FindIndex(fc => fc == figureController);

        DeleteFigure(figure);
        var newFigure = CreateFigure(figureType, figureHomePosition);
        ListOfFiguresType[indexOfLastFigure] = newFigure.GetComponent<FigureController>();
        newFigure.transform.SetParent(figureBox);

    }

    private void SetNewColor(FigureController figure)
    {
        var ListOfFiguresType = (figure.TypeFigure == FigureController.FigureType.Square) ? squaresOnScene : circlesOnScene;
        var colorId = Random.Range(0, settings.ListOfFigureColors.Count - 1);
        var color = settings.ListOfFigureColors.ElementAt(colorId);

        if (ListOfFiguresType.FirstOrDefault(f => f != null && f.Color == color) == null) figure.SetColor(color);
        else SetNewColor(figure);
    }

    private void SetNewTimerLasting(TimerController figureTimer)
    {
        var lasingId = Random.Range(0, settings.ListOfTimerLastings.Count - 1);
        var lasting = settings.ListOfTimerLastings.ElementAt(lasingId);
        figureTimer.SetLasting(lasting);
    }

    private void UpdateFigures(FigureController[] figuresOnScene)
    {
        if (figuresOnScene != null)
            for (int i = 0; i < figuresOnScene.Length; i++)
                UpdateFigure(figuresOnScene[i]);
    }

    private void UpdateFigure(FigureController figureOnScene)
    {
        if (figureOnScene != null)
            if (!figureOnScene.Enabled)
            {
                SetNewColor(figureOnScene);
                if (figureOnScene.TypeFigure == FigureController.FigureType.Circle) SetNewTimerLasting(figureOnScene.GetComponent<TimerController>());
                figureOnScene.transform.position = figureOnScene.HomePosition;
                figureOnScene.On();
            }

    }

    void Update()
    {
        UpdateFigures(squaresOnScene);
        UpdateFigures(circlesOnScene);
    }
}
