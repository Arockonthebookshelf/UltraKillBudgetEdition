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
}
