using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    public AudioSource battleAudioSource;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps the manager across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

    public static void PlayBattleMusic()
    {
        if (Instance && Instance.battleAudioSource && !Instance.battleAudioSource.isPlaying)
        {
            Instance.battleAudioSource.Play();
        }
    }

    public static void StopBattleMusic()
    {
        if (Instance && Instance.battleAudioSource && Instance.battleAudioSource.isPlaying)
        {
            Instance.battleAudioSource.Stop();
        }
    }
}
