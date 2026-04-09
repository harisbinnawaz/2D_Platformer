using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Text References")]
    [SerializeField] private TextMeshProUGUI mainMenuCoinText;
    [SerializeField] private TextMeshProUGUI hudCoinText;

    [Header("Toggle References")]
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;

    [Header("Audio Icon Objects")]
    [SerializeField] private GameObject sfxOnObj = null;
    [SerializeField] private GameObject sfxOffObj = null;
    [SerializeField] private GameObject musicOnObj = null;
    [SerializeField] private GameObject musicOffObj = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        Debug.Log("<color=#ff00ff>[UI] Manager Awake.</color>");
    }

    private void Start()
    {
        SyncAudioVisuals();
        UpdateMainMenuTotal(PrefsManager.GetCoins());
        UpdateHudSessionCoins(0);
        Debug.Log("<color=#ff00ff>[UI] Initial display sync complete.</color>");
    }

    private void SyncAudioVisuals()
    {
        bool musicIsOn = PrefsManager.IsMusicOn();
        bool sfxIsOn = PrefsManager.IsSFXOn();

        // Syncing toggle checkmarks without triggering listeners to avoid feedback loops
        if (musicToggle != null) musicToggle.SetIsOnWithoutNotify(musicIsOn);
        if (sfxToggle != null) sfxToggle.SetIsOnWithoutNotify(sfxIsOn);

        UpdateSfxVisual(sfxIsOn);
        UpdateMusicVisual(musicIsOn);
    }

    public void UpdateMainMenuTotal(int total)
    {
        if (mainMenuCoinText != null) mainMenuCoinText.text = "COINS: " + total;
        Debug.Log($"<color=#ff00ff>[UI] Main Menu Total Updated: {total}</color>");
    }

    public void UpdateHudSessionCoins(int sessionAmount)
    {
        if (hudCoinText != null) hudCoinText.text = "COINS: " + sessionAmount;
        Debug.Log($"<color=#ff00ff>[UI] HUD Session Coins Updated: {sessionAmount}</color>");
    }

    public void ToggleMusic()
    {
        bool newState = !PrefsManager.IsMusicOn();
        Debug.Log($"<color=#ff00ff>[UI] Music Toggle Button Clicked. New state: {newState}</color>");
        PrefsManager.SetMusic(newState);
        UpdateMusicVisual(newState);
        if (AudioManager.instance != null) AudioManager.instance.ToggleMusicState(newState);
    }

    public void ToggleSFX()
    {
        bool newState = !PrefsManager.IsSFXOn();
        Debug.Log($"<color=#ff00ff>[UI] SFX Toggle Button Clicked. New state: {newState}</color>");
        PrefsManager.SetSFX(newState);
        UpdateSfxVisual(newState);
    }

    public void UpdateSfxVisual(bool isOn)
    {
        if (sfxOnObj != null) sfxOnObj.SetActive(isOn);
        if (sfxOffObj != null) sfxOffObj.SetActive(!isOn);
    }

    public void UpdateMusicVisual(bool isOn)
    {
        if (musicOnObj != null) musicOnObj.SetActive(isOn);
        if (musicOffObj != null) musicOffObj.SetActive(!isOn);
    }
}