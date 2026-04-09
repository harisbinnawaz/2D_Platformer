using UnityEngine;

public static class PrefsManager
{
    private const string MusicToggleKey = "Music_On";
    private const string SFXToggleKey = "SFX_On";
    private const string CoinsKey = "Total_Coins";

    public static void AddCoins(int amount)
    {
        int currentCoins = GetCoins();
        PlayerPrefs.SetInt(CoinsKey, currentCoins + amount);
        PlayerPrefs.Save();
        Debug.Log($"<color=#ff00ff>[Prefs] Added {amount} coins. New Total: {GetCoins()}</color>");
    }

    public static int GetCoins()
    {
        return PlayerPrefs.GetInt(CoinsKey, 0);
    }

    public static void SetMusic(bool isOn)
    {
        // Ternary operator: if true use 1, if false use 0
        PlayerPrefs.SetInt(MusicToggleKey, isOn ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log($"<color=#ff00ff>[Prefs] Music Preference saved as: {isOn}</color>");
    }

    public static bool IsMusicOn() { return PlayerPrefs.GetInt(MusicToggleKey, 1) == 1; }

    public static void SetSFX(bool isOn)
    {
        PlayerPrefs.SetInt(SFXToggleKey, isOn ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log($"<color=#ff00ff>[Prefs] SFX Preference saved as: {isOn}</color>");
    }

    public static bool IsSFXOn() { return PlayerPrefs.GetInt(SFXToggleKey, 1) == 1; }
}