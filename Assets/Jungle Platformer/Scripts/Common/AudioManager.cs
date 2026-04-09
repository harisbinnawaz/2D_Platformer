using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource = null;
    [SerializeField] private AudioSource sfxSource = null;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip backgroundMusic = null;
    [SerializeField] private AudioClip coinSound = null;
    [SerializeField] private AudioClip winSound = null;
    [SerializeField] private AudioClip failSound = null;

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject); // Persistence across scene changes
        Debug.Log("<color=#ffff00>[Audio] Singleton initialized and persisted.</color>");
    }

    private void Start()
    {
        if (musicSource != null && backgroundMusic != null && PrefsManager.IsMusicOn())
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
            Debug.Log("<color=#ffff00>[Audio] Music started playing on startup.</color>");
        }
    }

    public void PlayCoin() { PlaySFX(coinSound, "Coin"); }
    public void PlayWin() { PlaySFX(winSound, "Win"); }
    public void PlayFail() { PlaySFX(failSound, "Fail"); }

    private void PlaySFX(AudioClip clip, string clipName)
    {
        if (PrefsManager.IsSFXOn() && sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
            Debug.Log($"<color=#ffff00>[Audio] Playing SFX: {clipName}</color>");
        }
    }

    public void ToggleMusicState(bool isPlaying)
    {
        if (musicSource == null) return;

        if (isPlaying && !musicSource.isPlaying)
        {
            musicSource.Play();
            Debug.Log("<color=#ffff00>[Audio] Music Unmuted.</color>");
        }
        else if (!isPlaying && musicSource.isPlaying)
        {
            musicSource.Stop();
            Debug.Log("<color=#ffff00>[Audio] Music Muted.</color>");
        }
    }
}