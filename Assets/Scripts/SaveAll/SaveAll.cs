using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAll : MonoBehaviour
{
    private SaveManager saveManager;
    void Start()
    {
        saveManager = GameObject.FindWithTag("SaveManager").GetComponent<SaveManager>();
    }

    public void SaveAllButton()
    {
        saveManager.GetDataLeaderboardScores();

        //YanCloud
        saveManager.MySave();
    }

}
