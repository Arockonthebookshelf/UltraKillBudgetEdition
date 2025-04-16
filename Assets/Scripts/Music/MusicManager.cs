using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource battleAudioSource;

    public void PlayBattleMusic()
    {
        if (battleAudioSource && !battleAudioSource.isPlaying)
        {
            battleAudioSource.Play();
        }
    }

    public void StopBattleMusic()
    {
        if (battleAudioSource && battleAudioSource.isPlaying)
        {
            battleAudioSource.Stop();
        }
    }

    public void PauseMusic()
    {
        if (battleAudioSource && battleAudioSource.isPlaying)
        {
            battleAudioSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (battleAudioSource)
        {
            battleAudioSource.UnPause();
        }
    }

    public void OnEnable()
    {
        Wave.OnwaveStart += PlayBattleMusic;
        Wave.OnwaveStop += StopBattleMusic;
    }

    public void OnDisable()
    {
        Wave.OnwaveStart -= PlayBattleMusic;
        Wave.OnwaveStop -= StopBattleMusic;
    }
}
