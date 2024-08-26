using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using UnityEngine.UI;

public class RewardAdsManager : MonoBehaviour
{

    private SaveManager saveManager;
    // Подписываемся на событие открытия рекламы в OnEnable

    private void Start()
    {
        saveManager = GameObject.FindWithTag("SaveManager").GetComponent<SaveManager>();
    }

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;

    }

    // Подписанный метод получения награды
    public void Rewarded(int id)
    {
        // Если ID = 1, то выдаём "+100 монет"
        if (id == 1)
            SaveManager.instance.money += 30;
            SaveManager.instance.Save();
            saveManager.MySave();


    }

    // Метод для вызова видео рекламы
    public void ExampleOpenRewardAd(int id)
    {
        // Вызываем метод открытия видео рекламы
        YandexGame.RewVideoShow(id);


    }


}
