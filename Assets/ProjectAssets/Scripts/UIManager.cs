using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private HealthData healthData;
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private GameStateManager gameStateManager;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private void OnEnable()
    {
        healthData.onHealthChanged += UpdateHealthUI;
        scoreData.onScoreChanged += UpdateScoreUI;
        scoreData.onHighScoreChanged += UpdateHighScoreUI;

        if (gameStateManager != null)
        {
            gameStateManager.OnGameOver += HandleGameOver;
        }
    }

    private void OnDisable()
    {
        healthData.onHealthChanged -= UpdateHealthUI;
        scoreData.onScoreChanged -= UpdateScoreUI;
        scoreData.onHighScoreChanged -= UpdateHighScoreUI;

        if (gameStateManager != null)
        {
            gameStateManager.OnGameOver -= HandleGameOver;
        }
    }

    private void Start()
    {
        UpdateHealthUI(healthData.CurrentHealth);
        UpdateScoreUI(scoreData.CurrentScore);
        UpdateHighScoreUI(scoreData.HighScore);

        // Asegurarse de que existe el NotificationManager
        if (NotificationManager.Instance == null)
        {
            GameObject notificationObj = new GameObject("NotificationManager");
            notificationObj.AddComponent<NotificationManager>();
        }
    }

    private void UpdateHealthUI(int newHealth)
    {
        healthText.text = $"Lifes: {newHealth}";
    }

    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = $"Score: {newScore}";
    }

    private void UpdateHighScoreUI(int newHighScore)
    {
        highScoreText.text = $"HighScore: {newHighScore}";
    }

    private void HandleGameOver()
    {
        // Enviar notificación cuando el juego termina
        NotificationManager.Instance.SendGameOverNotification(
            scoreData.CurrentScore,
            scoreData.IsNewHighScore);
    }
}