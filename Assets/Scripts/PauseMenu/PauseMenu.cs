using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject restartUI;
    public GameObject playUI;
    public GameObject shopUI;
    public GameObject pauseMenuUI;
    public GameObject pauseButton;
    public GameObject skinsChangeButton;

    public GameObject caseShop;


    public GameObject skinsCamera;
   

    private bool isPaused = false;

    private CubeJump cubeJump;

   
private void Start()
{
    // Найдите объект с тегом "PlayerController" и получите компонент CubeJump из его дочерних объектов
    GameObject playerController = GameObject.FindWithTag("PlayerController");
    if (playerController != null)
    {
        cubeJump = playerController.GetComponentInChildren<CubeJump>();
    }
}
    //public bool pauseOnOff = false;
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         if (isPaused)
    //         {
    //             Resume();
    //         }
    //         else
    //         {
    //             Pause();
    //         }
    //     }
    // }


    public void Resume()
    {
        CubeJump.isShop = true;
        // Hide the pause menu and resume the game
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        skinsChangeButton.SetActive(false);
        //skinsChange.SetActive(false);
       // caseShop.SetActive(false);
        Time.timeScale = 1f; // Resume game time
        isPaused = false;
        StartCoroutine(PauseOff());
        // cubeJump.isMove = true;

        skinsCamera.SetActive(false);
      

      

     
    }



    private IEnumerator PauseOff()
    {
        yield return new WaitForSeconds(0.2f);

        cubeJump.isMove = true;


    }

    public void Pause()
    {
        // Show the pause menu and pause the game
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f; // Pause game time
        isPaused = true;
        // pauseOnOff = true;

        cubeJump.isMove = false;

    }

    public void QuitGame()
    {
        // Quit the game (this might need adjustments in a real project)
        Application.Quit();
    }

    public void SkinChange()
    {
        //Time.timeScale = 0f; // Pause game time
        isPaused = true;
        // pauseOnOff = true;

        cubeJump.isMove = false;
        skinsChangeButton.SetActive(false);
        pauseMenuUI.SetActive(true);
      
        
        CubeJump.isShop = false;

        skinsCamera.SetActive(true);
        

    

    }

    public void CaseShopOpen()
    {
        
        caseShop.SetActive(true);
    }

    public void CaseShopClose()
    {
        
        caseShop.SetActive(false);
    }


    public void ActivePauseMenu()
    {
        pauseButton.SetActive(true);
        restartUI.SetActive(true);
        pauseMenuUI.SetActive(true);

        playUI.SetActive(false);
        
        pauseButton.SetActive(false);
    }


       public void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


}
