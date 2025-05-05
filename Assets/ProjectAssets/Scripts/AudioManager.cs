using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioSource musicSource;
    public AudioClip[] sceneMusics;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip buttonClick;
    public AudioClip shoot;
    public AudioClip playerHurt;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneMusic(scene.buildIndex);
    }

    public void PlaySceneMusic(int index)
    {
        if (index < sceneMusics.Length)
        {
            musicSource.clip = sceneMusics[index];
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayButtonClick() => PlaySFX(buttonClick);
    public void PlayShoot() => PlaySFX(shoot);
    public void PlayPlayerHurt() => PlaySFX(playerHurt);
}