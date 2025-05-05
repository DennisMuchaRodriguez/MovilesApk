using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultsBoardManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private TMP_Text highScoreText;

    [Header("Data References")]
    [SerializeField] private ScoreData scoreData;

    [Header("Scene Settings")]
    [SerializeField] private string mainSceneName = "MainGame";

    private void Start()
    {
        UpdateScoreDisplays();
        SendGameOverNotification();
    }

    private void UpdateScoreDisplays()
    {
        currentScoreText.text = $"Score: {scoreData.CurrentScore}";
        highScoreText.text = $"HighScore: {scoreData.HighScore}";
    }

    private void SendGameOverNotification()
    {
        if (NotificationManager.Instance == null)
        {
            gameObject.AddComponent<NotificationManager>();
        }

        bool isNewHighScore = scoreData.CurrentScore > PlayerPrefs.GetInt("PreviousHighScore", 0);
        NotificationManager.Instance.SendGameOverNotification(scoreData.CurrentScore, isNewHighScore);

        if (isNewHighScore)
        {
            PlayerPrefs.SetInt("PreviousHighScore", scoreData.HighScore);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(mainSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}