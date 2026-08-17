using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text HighScoreText;
    [SerializeField] Text ScoreText;

    public static float score;
    private SaveManager saveManager;
    private int lastDisplayedScore = -1;
    private int lastDisplayedHighScore = -1;

    void Start()
    {
        GameObject saveManagerObj = GameObject.FindWithTag("SaveManager");
        if (saveManagerObj != null)
        {
            saveManager = saveManagerObj.GetComponent<SaveManager>();
        }

        score = 0;
        lastDisplayedScore = -1;
        lastDisplayedHighScore = -1;

        UpdateHighScoreText();
    }

    void Update()
    {
        int currentScoreInt = (int)score;
        if (currentScoreInt < 0)
        {
            currentScoreInt = 0;
        }

        if (currentScoreInt != lastDisplayedScore)
        {
            lastDisplayedScore = currentScoreInt;
            if (ScoreText != null)
            {
                ScoreText.text = currentScoreInt.ToString();
            }
        }

        if (saveManager != null && currentScoreInt > saveManager.highscore)
        {
            saveManager.highscore = currentScoreInt;
            UpdateHighScoreText();
        }
    }

    void UpdateHighScoreText()
    {
        if (saveManager != null && HighScoreText != null && saveManager.highscore != lastDisplayedHighScore)
        {
            lastDisplayedHighScore = saveManager.highscore;
            HighScoreText.text = lastDisplayedHighScore.ToString();
        }
    }
}
