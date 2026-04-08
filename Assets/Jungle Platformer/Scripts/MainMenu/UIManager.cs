using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("SFX UI Objects")]
    [SerializeField] private GameObject sfxOnObj = null;
    [SerializeField] private GameObject sfxOffObj = null;

    [Header("Music UI Objects")]
    [SerializeField] private GameObject musicOnObj = null;
    [SerializeField] private GameObject musicOffObj = null;

    // These remain public ONLY because the GameManager needs to call them
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