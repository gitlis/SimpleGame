using System;
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
        uiMenuPanel = gameObject.GetComponent<RectTransform>();

        var inscriptions = uiMenuPanel.gameObject.GetComponentsInChildren<Text>();
        time = inscriptions[0];
        textScore = inscriptions[1];
        textGame = inscriptions[2];
        score = inscriptions[3];
        bestScore = inscriptions[4];
        gameNumber = inscriptions[5];
      
        timeLevel = uiMenuPanel.gameObject.AddComponent<TimeLevel>();
        timeLevel.SecondsLevelLasting = (secondsLevelLasting != 0) ? secondsLevelLasting : 30f;
        timeLevel.IsEnded += EndLevel;
        timeLevel.IsTimeChanged += ShowTime;

        scoreLevel = uiMenuPanel.gameObject.GetComponentInParent<Score>();
        scoreLevel.IsScoreChanged += ShowScore;

        if (uiMenuPanel != null) uiMenuPanel.gameObject.SetActive(false);
    }

    public void StartLevel()
    {
        On();
        ShowTime();
        ShowScore();
        bestScore.text = scoreLevel.BestScoreNumber.ToString();
        gameNumber.text = scoreLevel.GameNumber.ToString();
    }

    public void EndLevel()
    {
        uiManager.uiMenu.On();
        Off();
    }


    public override void On()
    {
        base.On();
        timeLevel.StartTime();
        scoreLevel.StartScore();
        if (uiMenuPanel != null) uiMenuPanel.gameObject.SetActive(true);
    }

    public override void Off()
    {
        base.Off();
        timeLevel.EndTime();
        scoreLevel.EndScore();
        if (uiMenuPanel != null) uiMenuPanel.gameObject.SetActive(false);
    }

    private void ShowTime()
    {
        time.text = timeLevel.RemainedTime;
    }

    private void ShowScore()
    {
        score.text = scoreLevel.ScoreNumber.ToString();
    }
}
