using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager = null;

    private bool isMusicOn = true;
    private bool isSfxOn = true;

    private void Start()
    {
        ApplyAudioSettings();
    }

    public void SetMusicState(bool isOn)
    {
        isMusicOn = isOn;
        ApplyAudioSettings();
    }

    public void SetSfxState(bool isOn)
    {
        isSfxOn = isOn;
        ApplyAudioSettings();
    }

    private void ApplyAudioSettings()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.musicSource.mute = !isMusicOn;
            AudioManager.instance.sfxSource.mute = !isSfxOn;
        }

        if (uiManager != null)
        {
            uiManager.UpdateMusicVisual(isMusicOn);
            uiManager.UpdateSfxVisual(isSfxOn);
        }
    }
}