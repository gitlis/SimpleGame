using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

    public int ScoreNumber { get; set; }
    public int BestScoreNumber { get; set; }
    public int GameNumber { get; set; }

    public delegate void Event();
    public event Event IsScoreChanged;

    void Awake()
    {
        ScoreNumber = 0;
        BestScoreNumber = 0;
        GameNumber = 0;
    }

    public void StartScore()
    {
        GameNumber++;
       // IsScoreChanged.Invoke();
       // if (true) ScoreNumber++;
    }

    public void EndScore()
    {
        if (ScoreNumber > BestScoreNumber) BestScoreNumber = ScoreNumber;
        ScoreNumber = 0;
    }
}
