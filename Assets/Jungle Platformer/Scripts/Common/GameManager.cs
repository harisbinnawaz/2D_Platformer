using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject MainMenuBg = null;
    [SerializeField] private GameObject mainMenuPanel = null;
    [SerializeField] private GameObject hudPanel = null;

    [Header("Gameplay Objects")]
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject gridEnvironment = null;
    [SerializeField] private GameObject background = null;
    [SerializeField] private GameObject coins = null;
    [SerializeField] private GameObject chest = null;

    private Vector3 initialPlayerPos;
    public bool isLevelActive { get; private set; } // Tracks if we are actually playing

    private void Awake()
    {
        instance = this;
        if (player != null) initialPlayerPos = player.transform.position;
        Debug.Log($"<color=#00ffff>[GameManager] Awake. Player spawn recorded at: {initialPlayerPos}</color>");
    }

    void Start()
    {
        ResetToMainMenu();
    }

    public void StartGame()
    {
        Debug.Log("<color=#00ffff>[GameFlow] StartGame clicked. Transitioning to Gameplay.</color>");

        isLevelActive = true; // Level is now live

        // --- FIX: Reactivate all collected coins ---
        if (coins != null)
        {
            foreach (Transform child in coins.transform)
            {
                child.gameObject.SetActive(true);
            }
            Debug.Log("<color=#00ffff>[GameFlow] All coins reactivated for new run.</color>");
        }

        MainMenuBg.SetActive(false);
        mainMenuPanel.SetActive(false);
        hudPanel.SetActive(true);
        player.SetActive(true);
        gridEnvironment.SetActive(true);
        background.SetActive(true);
        coins.SetActive(true);
        chest.SetActive(true);
    }

    public void ResetToMainMenu()
    {
        Debug.Log("<color=#00ffff>[GameFlow] Resetting to Main Menu state.</color>");

        isLevelActive = false; // Stop level logic

        mainMenuPanel.SetActive(true);
        MainMenuBg.SetActive(true);
        hudPanel.SetActive(false);

        player.SetActive(false);
        gridEnvironment.SetActive(false);
        background.SetActive(false);
        coins.SetActive(false);
        chest.SetActive(false);

        player.transform.position = initialPlayerPos;
    }
}