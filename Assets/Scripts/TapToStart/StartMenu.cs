using UnityEngine;

public class StartMenu : MonoBehaviour
{


    public GameObject startMenuUI;
    public GameObject shopButton;
    public GameObject pauseButton;

    private void Start()
    {
        // Show the start menu initially
        startMenuUI.SetActive(true);
    }

    public void StartGame()
    {
        // Hide the start menu and start the game
        startMenuUI.SetActive(false);
        // Your game initialization code goes here
        Invoke("ActivatePauseButton", 0.3f);

    }
    void ActivatePauseButton()
    {
        shopButton.SetActive(false); // Активируем кнопку паузы
        pauseButton.SetActive(true);
    }
}