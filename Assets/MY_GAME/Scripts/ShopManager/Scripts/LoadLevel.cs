using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;

public class LoadLevel : MonoBehaviour
{
    public PauseMenu pauseMenu;  
    public GameObject loadingScreen;
    public float fakeLoadingTime = 5f;

    public void LoadLevelNumber(int sceneIndex)
    {
        LoadSceneAsync(sceneIndex, this.GetCancellationTokenOnDestroy()).Forget();
        if (pauseMenu != null)
            pauseMenu.Resume();
    }

    private async UniTaskVoid LoadSceneAsync(int sceneIndex, CancellationToken token)
    {
        CubeJump.gameStarted = false;
        
        loadingScreen.SetActive(true);

        var asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        

        asyncOperation.allowSceneActivation = false;
        float startTime = Time.realtimeSinceStartup;

        while (!asyncOperation.isDone)
        {
            float elapsed = Time.realtimeSinceStartup - startTime;

            if (elapsed >= fakeLoadingTime && asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true; 
                
                break;
            }

            await UniTask.Yield(token);
        }

        await UniTask.WaitUntil(() => asyncOperation.isDone, cancellationToken: token);

        loadingScreen.SetActive(false);
    }
}