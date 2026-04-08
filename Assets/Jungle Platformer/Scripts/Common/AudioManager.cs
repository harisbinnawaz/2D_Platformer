using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance makes it easy to call from other scripts (like the player)
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip coinSound;

    private void Awake()
    {
        // --- The Singleton & Persistence Logic ---
        // If an instance already exists, destroy this duplicate
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Otherwise, make this the active instance and tell Unity not to destroy it
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Start the background music as soon as the game opens
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // Call this from your Player script when they hit a coin: AudioManager.instance.PlayCoin();
    public void PlayCoin()
    {
        if (sfxSource != null && coinSound != null)
        {
            sfxSource.PlayOneShot(coinSound);
        }
    }
}