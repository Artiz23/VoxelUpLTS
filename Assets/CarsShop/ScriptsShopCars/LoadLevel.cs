using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadLevel : MonoBehaviour
{   
    public PauseMenu pauseMenu;  
    public GameObject loadingScreen; // Ссылка на UI загрузочного экрана
    public Slider loadingBar; // Ссылка на элемент UI индикатора загрузки
    public Text loadingText; // Ссылка на элемент UI текста загрузки (TextMeshPro)
    public float fakeLoadingTime = 5f; // Время для искусственной загрузки

    public void LoadLevelNumber(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
        if(pauseMenu != null)
        pauseMenu.Resume();
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        // Активируем загрузочный экран
        loadingScreen.SetActive(true);

        // Начинаем асинхронную загрузку сцены
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = false;

        // Фальшивая загрузка
        float elapsed = 0f;

        while (!asyncOperation.isDone)
        {
            // Обновляем индикатор загрузки и текст
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            if (elapsed < fakeLoadingTime)
            {
                elapsed += Time.deltaTime;
                float fakeProgress = Mathf.Clamp01(elapsed / fakeLoadingTime);
                int displayProgress = Mathf.RoundToInt(fakeProgress * 100);
                loadingBar.value = fakeProgress;
                loadingText.text = $"Loading... {displayProgress}%";
            }
            else
            {
                int displayProgress = Mathf.RoundToInt(progress * 100);
                loadingBar.value = progress;
                loadingText.text = $"Loading... {displayProgress}%";

                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }

        // Деактивируем загрузочный экран после загрузки сцены
        loadingScreen.SetActive(false);
    }
}
