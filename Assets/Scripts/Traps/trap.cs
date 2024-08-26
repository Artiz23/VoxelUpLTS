using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class trap : MonoBehaviour
{   
    private PlayerDeath playerDeath;
   
    private CubeJump cubeJump;
    
    void Start()
    {
        
        playerDeath = GetComponent<PlayerDeath>();

        cubeJump = GetComponent<CubeJump>();
    }

    private void OnTriggerEnter(Collider other) 
    {

        if(other.gameObject.CompareTag("Trap"))
        {
          // playerDeath.hp -= 1;
           playerDeath.Die();
          StartCoroutine(DelayedRestartScene());
          cubeJump.isMove = false;
        }
    }
     public void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        
        // NetworkManager.Singleton.Shutdown();
        // Cleanup();
       
    }
    private IEnumerator DelayedRestartScene()
    {
        yield return new WaitForSeconds(0.7f); // Подождать 0.5 секунду

        // Вызвать функцию перезапуска сцены
        RestartScene();

    }
}
