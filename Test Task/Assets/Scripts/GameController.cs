using System.Collections;
using System.Linq;
using UnityEngine;

public class GameController : BaseController
{
    public Transform camera2D;
    private Transform canvas;

    private int countOfSquares;
    private int countOfCircles;
    private GameObject squaresBox;
    private GameObject circlesBox;

    private FigureController[] squaresOnScene;
    private FigureController[] circlesOnScene;
    private GameSettings gamesettings; // Потом надо убрать и сосдавать по входу в игру!
    public GameSettings.Settings settings;


    void Awake()
    {
        camera2D = GameObject.FindGameObjectWithTag("MainCamera").transform;
        canvas = GameObject.Find("Canvas").transform;
        countOfSquares = 3;
        countOfCircles = 3;
        GameObject squaresBox = new GameObject();
        GameObject circlesBox = new GameObject();
        squaresOnScene = new FigureController[countOfSquares];
        circlesOnScene = new FigureController[countOfCircles];
        gamesettings = new GameSettings();
        settings = gamesettings.SettingsGame;
    }

    public void StartGame()
    {
        On();
        CreateSquares(countOfSquares);
        CreateCircles(countOfCircles);
    }

    public void EndGame()
    {
        squaresBox.gameObject.SetActive(false);
        circlesBox.gameObject.SetActive(false);
        CreateCircles(countOfCircles);
        Off();
    }

    public void CreateSquares(int count)
    {
        squaresBox = new GameObject();
        squaresBox.transform.parent = canvas;
        squaresBox.name = "Squares";

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(-30, 30 * (i - 1), 10);
            Vector3 position = camera2D.position + offset;
            var square = Instantiate(PrefabStore.Instance.GetSquarePrefab(), position, Quaternion.identity, squaresBox.transform);

            var colorId = Random.Range(0, settings.ListOfFigureColors.Count - 1);
            var color = settings.ListOfFigureColors.ElementAt(colorId);
            if (squaresOnScene != null || squaresOnScene.FirstOrDefault(f => f.Color == color) == null)
            {
                square.AddComponent<FigureController>();
                squaresOnScene[i] = square.GetComponent<FigureController>();
                squaresOnScene[i].SetColor(color);
                squaresOnScene[i].TypeFigure = FigureController.FigureType.Square;

                square.AddComponent<FigureMouseController>();
            }
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
            Vector3 offset = new Vector3(30, 30 * (i - 1), 10);
            Vector3 position = camera2D.position + offset;
            var circle = Instantiate(PrefabStore.Instance.GetCicrlePrefab(), position, Quaternion.identity, circlesBox.transform);

            var colorId = Random.Range(0, settings.ListOfFigureColors.Count - 1);
            var color = settings.ListOfFigureColors.ElementAt(colorId);
            if (squaresOnScene != null || circlesOnScene.FirstOrDefault(f => f.Color == color) == null)
            {
                circle.AddComponent<FigureController>();
                circlesOnScene[i] = circle.GetComponent<FigureController>();
                circlesOnScene[i].SetColor(color);
                circlesOnScene[i].TypeFigure = FigureController.FigureType.Circle;

                var lasingId = Random.Range(0, settings.ListOfTimerLastings.Count - 1);
                var lasting = settings.ListOfTimerLastings.ElementAt(lasingId);
                circle.AddComponent<TimerController>();
                circle.GetComponent<TimerController>().SetLasting(lasting);
            }
            else i--;

        }
    }

    private void SetNewColor(FigureController figure)
    {
        var ListOfFiguresType = (figure.TypeFigure == FigureController.FigureType.Square) ? squaresOnScene : circlesOnScene;
        var colorId = Random.Range(0, settings.ListOfFigureColors.Count - 1);
        var color = settings.ListOfFigureColors.ElementAt(colorId);
        if (ListOfFiguresType.FirstOrDefault(f => f.Color == color) == null) figure.SetColor(color);
        else SetNewColor(figure);
    }

    private void UpdateTimerLasting(TimerController timer)
    {
        var lastingId = Random.Range(0, settings.ListOfTimerLastings.Count - 1);
        var lasing = settings.ListOfTimerLastings.ElementAt(lastingId);
        timer.SetLasting(lasing);
    }

    private void UpdateMouseInteract(FigureMouseController figureMouse)
    {
        figureMouse.UpdateMouse();
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
                if (figureOnScene.TypeFigure == FigureController.FigureType.Circle) UpdateTimerLasting(figureOnScene.gameObject.GetComponent<TimerController>());
                if (figureOnScene.TypeFigure == FigureController.FigureType.Square) UpdateMouseInteract(figureOnScene.gameObject.GetComponent<FigureMouseController>());
                figureOnScene.On();
            }

    }

    void Update()
    {
        UpdateFigures(squaresOnScene);
        UpdateFigures(circlesOnScene);
    }
}
