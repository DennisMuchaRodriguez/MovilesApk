using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Score Data", menuName = "ScriptableObjects/Player Data/Score Data", order = 1)]
public class ScoreData : ScriptableObject
{
    public Action<int> onScoreChanged;
    public Action<int> onHighScoreChanged;
    [SerializeField] private int currentScore;
    [SerializeField] private int highScore;
    private bool isNewHighScore = false;

    public int CurrentScore
    {
        get => currentScore;
        set
        {
            currentScore = value;
            onScoreChanged?.Invoke(currentScore);

            if (currentScore > highScore)
            {
                HighScore = currentScore;
                isNewHighScore = true;
            }
            else
            {
                isNewHighScore = false;
            }
        }
    }

    public int HighScore
    {
        get => highScore;
        set
        {
            if (value > highScore)
            {
                highScore = value;
                onHighScoreChanged?.Invoke(highScore);
            }
        }
    }

    public bool IsNewHighScore => isNewHighScore;

    public void ResetCurrentScore()
    {
        currentScore = 0;
        isNewHighScore = false;
        onScoreChanged?.Invoke(currentScore);
    }

    public void ResetHighScore()
    {
        highScore = 0;
        onHighScoreChanged?.Invoke(highScore);
    }
}