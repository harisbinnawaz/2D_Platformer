using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image loadingBarFill = null;

    private string sceneToLoad = "GamePlay";
    private float simulatedLoadTime = 2.0f;

    private void Start()
    {
        Debug.Log("<color=#00ffff>[Splash] Sequence Started. Loading GamePlay scene...</color>");
        StartCoroutine(LoadGameCoroutine());
    }

    private IEnumerator LoadGameCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < simulatedLoadTime)
        {
            elapsedTime += Time.deltaTime;

            if (loadingBarFill != null)
            {
                // Normalizing time into a 0.0 to 1.0 range for the fill amount
                loadingBarFill.fillAmount = elapsedTime / simulatedLoadTime;
            }
            yield return null;
        }

        if (loadingBarFill != null) loadingBarFill.fillAmount = 1f;

        Debug.Log("<color=#00ffff>[Splash] Simulated load complete. Switching scenes now.</color>");

        // Asynchronously loading the scene to prevent frame drops
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}