using UnityEngine;
using System.Collections;
using TMPro;

public class CoinManager : MonoBehaviour
{

    
    public SoundManager soundManager;

    //Coin Text
    //public int coins = 0;
    //public TMP_Text coinsText;
    //public TMP_Text coinsTextCase;
    private const string CoinsPlayerPrefsKey = "CollectedCoins";

    // private void Start()
    // {
    //     //Coins Code
    //     LoadCoins();

       
    // }



   // Coin Code
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Coin"))
        {

           // coins++;
           // coinsText.text = "" + coins;
            //coinsTextCase.text = "" + coins;
            //SaveCoins();

            soundManager.PlayCoinSound();

           /////////////////////////////////////////////
            SaveManager.instance.money += 1;
            SaveManager.instance.Save();
            
        }
    }

    // public void SaveCoins()
    // {
    //     PlayerPrefs.SetInt(CoinsPlayerPrefsKey, coins);
    //     PlayerPrefs.Save();
    // }

    // private void LoadCoins()
    // {
    //     if (PlayerPrefs.HasKey(CoinsPlayerPrefsKey))
    //     {
    //         coins = PlayerPrefs.GetInt(CoinsPlayerPrefsKey);
    //         coinsText.text = coins.ToString();
    //         coinsTextCase.text = coins.ToString();
            
    //     }
    // }
}
