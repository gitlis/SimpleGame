using UnityEngine;
using UnityEngine.UI;

public class UIMenuController : BaseController
{
    private RectTransform uiMenuPanel;
    private UIManager uiManager;

    [SerializeField]
    float secondsLevelLasting;
    private TimeLevel timeLevel;

    private Score scoreLevel;

    private Text time;
    private Text textScore;
    private Text textGame;
    private Text score;
    private Text bestScore;
    private Text gameNumber;

    private void Awake()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
        uiMenuPanel = GetComponent<RectTransform>();

        var inscriptions = uiMenuPanel.GetComponentsInChildren<Text>();
        time = inscriptions[0];
        textScore = inscriptions[1];
        textGame = inscriptions[2];
        score = inscriptions[3];
        bestScore = inscriptions[4];
        gameNumber = inscriptions[5];
      
        timeLevel = uiMenuPanel.gameObject.AddComponent<TimeLevel>();
        timeLevel.SecondsLevelLasting = (secondsLevelLasting != 0) ? secondsLevelLasting : secondsLevelLasting = 30f;
        timeLevel.Ended += EndLevel;
        timeLevel.TimeChanged += ShowTime;

        scoreLevel = uiMenuPanel.gameObject.GetComponent<Score>();
        scoreLevel.ScoreChanged += ShowScore;

        textScore.text = "Score / Best Score";
        textGame.text = "Played games";

        if (uiMenuPanel != null) uiMenuPanel.gameObject.SetActive(false);
    }

    public void StartLevel()
    {
        On();
        uiManager.gameController.On();
        uiManager.mouseInputController.On();
        uiManager.mainMenu.Off();

    }

    public void EndLevel()
    {
        uiManager.mainMenu.On();
        uiManager.mouseInputController.Off();
        uiManager.gameController.Off();
        Off();
    }

    public override void On()
    {
        base.On();
        if (uiMenuPanel != null) uiMenuPanel.gameObject.SetActive(true);
        timeLevel.StartTime();
        scoreLevel.StartScore();

        ShowTime();
        ShowScore();
        ShowBestScore();
        ShowGameNumber();
    }

    public override void Off()
    {
        base.Off();
        if (uiMenuPanel != null) uiMenuPanel.gameObject.SetActive(false);
        timeLevel.EndTime();
        scoreLevel.EndScore();
    }

    private void ShowTime()
    {
        time.text = timeLevel.RemainedTime;
    }

    private void ShowScore()
    {
        score.text = scoreLevel.ScoreNumber.ToString();
    }

    private void ShowBestScore()
    {
        bestScore.text = scoreLevel.BestScoreNumber.ToString();
    }

    private void ShowGameNumber()
    {
        gameNumber.text = scoreLevel.GameNumber.ToString();
    }
}
