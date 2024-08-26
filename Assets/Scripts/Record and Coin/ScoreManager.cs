using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text HighScoreText; // Используется TextMeshPro
    [SerializeField] Text ScoreText; // Используется TextMeshPro

    public static float score;
    //public static int highscore;

    private SaveManager saveManager;

    void Start()
    {
        saveManager = GameObject.FindWithTag("SaveManager").GetComponent<SaveManager>();

        score = 0; // Установка начального значения для score

        // Загрузка сохраненного рекорда из PlayerPrefs
       // SaveManager.highscore = PlayerPrefs.GetInt("score", 0);
        UpdateHighScoreText();
    }

    void Update()
    {
        if (ScoreText != null)
        {
            ScoreText.text = ((int)score).ToString(); // Обновление текста ScoreText текущим счетом
        }

        // Проверка и обновление рекорда в PlayerPrefs
        if ((int)score > saveManager.highscore)
        {
            saveManager.highscore = (int)score;
            PlayerPrefs.SetInt("score", saveManager.highscore);
            UpdateHighScoreText(); // Обновление текста HighScoreText
        }
    }

    void UpdateHighScoreText()
    {
        if (HighScoreText != null)
        {
            HighScoreText.text = saveManager.highscore.ToString();
        }
    }

  
}
