using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using YG;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    // Что мы хотим сохранить
    public int currentCar;
    public int money;
    public   int highscore;
    public bool[] carsUnlocked = new bool[6] { true, false, false, false, false, false };

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)bf.Deserialize(file);

            money = data.money;
            currentCar = data.currentCar;
            carsUnlocked = data.carsUnlocked;
            highscore = data.highscore;

            if (data.carsUnlocked == null)
                carsUnlocked = new bool[6] { true, false, false, false, false, false };

            file.Close();
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

        data.money = money;
        data.currentCar = currentCar;
        data.carsUnlocked = carsUnlocked;
        data.highscore = highscore;

        bf.Serialize(file, data);
        file.Close();
    }

    public void DeleteSave()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
            Debug.Log("Файл сохранения удален.");
        }
        else
        {
            Debug.Log("Нет файла сохранения для удаления.");
        }
    }

    // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Start()
    {
        // Проверяем запустился ли плагин
        if (YandexGame.SDKEnabled == true)
        {
            // Если запустился, то выполняем ваш метод для загрузки
            GetLoad();
            // Если плагин еще не прогрузился, то метод не выполнится в методе Start,
            // но он запустится при вызове события GetDataEvent, после прогрузки плагина
        }
    }

    // Ваш метод для загрузки, который будет запускаться в старте 
    public void GetLoad()
    {
        // Получаем данные из плагина и делаем с ними что хотим
        money = YandexGame.savesData.coins;
        carsUnlocked = YandexGame.savesData.skinsUnlocked;
        highscore = YandexGame.savesData.highScoreData;
        
    }

    // Допустим, это ваш метод для сохранения
    public void MySave()
    {
        // Записываем данные в плагин
        YandexGame.savesData.coins = money;
        YandexGame.savesData.skinsUnlocked = carsUnlocked;
        YandexGame.savesData.highScoreData = highscore;

        // Теперь остаётся сохранить данные
        YandexGame.SaveProgress();
    }

    public  void GetDataLeaderboardScores()
    {
        // Отправка данных в таблицу лидеров Yandex
        YandexGame.NewLeaderboardScores("LiderBordVoxUpper", highscore);
    }
}

[Serializable]
class PlayerData_Storage
{
    public int currentCar;
    public int money;
    public int highscore;
    public bool[] carsUnlocked;
}
