using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerStats player;

    public AudioSource normalMusicSource;
    public AudioSource bossMusicSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlayNormalMusic()
    {
        if (!normalMusicSource.isPlaying)
            normalMusicSource.Play();
        bossMusicSource.Stop();
    }

    public void PlayBossMusic()
    {
        if (!bossMusicSource.isPlaying)
            bossMusicSource.Play();
        normalMusicSource.Stop();
    }

    public void StopAllMusic()
    {
        normalMusicSource.Stop();
        bossMusicSource.Stop();
    }
}
