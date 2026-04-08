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
                loadingBarFill.fillAmount = elapsedTime / simulatedLoadTime;
            }
            yield return null;
        }

        
        if (loadingBarFill != null) loadingBarFill.fillAmount = 1f;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}