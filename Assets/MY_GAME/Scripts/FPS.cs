using UnityEngine;
using UnityEngine.UI; // Обязательно для старого Text

public class FPS : MonoBehaviour
{
    [Header("Настройки")]
    public Text fpsText; // Сюда перетащите ваш UI Text в инспекторе
    public float updateInterval = 0.5f; // Как часто обновлять цифры (сек)

    private float accum = 0f;   // Накопленное время
    private int frames = 0;     // Количество кадров
    private float timeLeft;     // Оставшееся время до обновления

    void Start()
    {
        timeLeft = updateInterval;

        // Если не перетащили текст вручную, попробуем найти его на этом же объекте
        if (fpsText == null)
        {
            fpsText = GetComponent<Text>();
        }
    }

    void Update()
    {
        // Вычитаем время, прошедшее с прошлого кадра
        timeLeft -= Time.deltaTime;
        
        // Накапливаем данные (делим 1 на время кадра, получаем мгновенный FPS)
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        // Когда время интервала вышло, обновляем текст
        if (timeLeft <= 0.0f)
        {
            // Считаем средний FPS за прошедший интервал
            float fps = accum / frames;
            
            // Форматируем строку (F0 означает 0 знаков после запятой)
            string format = System.String.Format("{0:F0} FPS", fps);
            
            // Выводим в UI
            if (fpsText != null)
            {
                fpsText.text = format;

                // Опционально: меняем цвет в зависимости от FPS
                if (fps < 30)
                    fpsText.color = Color.red;
                else if (fps < 60)
                    fpsText.color = Color.yellow;
                else
                    fpsText.color = Color.white;
            }

            // Сбрасываем счетчики для следующего интервала
            timeLeft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
}