using UnityEngine;
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;

    private string channelId = "game_channel";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeNotifications();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeNotifications()
    {
        // Crear canal de notificaci�n
        var channel = new AndroidNotificationChannel()
        {
            Id = channelId,
            Name = "Game Notifications",
            Importance = Importance.High,
            Description = "Game results notifications",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);

#if UNITY_ANDROID
        // Versi�n alternativa para versiones m�s recientes de Unity
        // No necesitamos solicitar permisos expl�citamente en versiones recientes
        // El sistema Android manejar� los permisos autom�ticamente
#endif
    }

    public void SendGameOverNotification(int score, bool isHighScore)
    {
        string title = isHighScore ? "�Nuevo R�cord!" : "Juego Terminado";
        string text = isHighScore ? $"Nuevo puntaje m�ximo: {score}" : $"Puntaje final: {score}";
        string iconName = isHighScore ? "icon_highscore" : "icon_score";

        var notification = new AndroidNotification
        {
            Title = title,
            Text = text,
            SmallIcon = iconName,
            FireTime = System.DateTime.Now.AddSeconds(1),
            ShouldAutoCancel = true
        };

        AndroidNotificationCenter.SendNotification(notification, channelId);
    }
}