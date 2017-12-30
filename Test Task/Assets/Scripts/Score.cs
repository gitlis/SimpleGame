using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private UIManager uiManager;

    public int ScoreNumber { get; set; }
    public int BestScoreNumber { get; set; }
    public int GameNumber { get; set; }

    public delegate void Event();
    public event Event ScoreChanged;

    void Awake()
    {
        ScoreNumber = 0;
        BestScoreNumber = 0;
        GameNumber = 0;
        uiManager = GameObject.FindObjectOfType<UIManager>();
        uiManager.mouseInputController.SquareGotTarget += AddScore;
    }

    public void StartScore()
    {
        GameNumber++;
    }

    public void EndScore()
    {
        if (ScoreNumber > BestScoreNumber) BestScoreNumber = ScoreNumber;
        ScoreNumber = 0;
    }

    public void AddScore()
    {
        ScoreNumber++;
        ScoreChanged.Invoke();
    }
}
