using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        // Установите начальное значение громкости
        volumeSlider.value = audioSource.volume;

        // Добавьте слушатель для изменения значения слайдера
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float value)
    {
        // Измените громкость аудио источника
        audioSource.volume = value;
    }
}
