using UnityEngine;

public static class FrameRateInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeFrameRate()
    {
        // Отключаем VSync в рантайме, чтобы работал разблокированный или высокий FPS
        QualitySettings.vSyncCount = 0;

        // На мобильных устройствах (iOS/Android) и в WebGL снимаем стандартное ограничение (по умолчанию в Unity часто 30 FPS)
#if UNITY_WEBGL
        // В WebGL -1 позволяет браузеру использовать максимальную частоту развертки экрана (60 / 90 / 120+ Hz)
        Application.targetFrameRate = -1;
#elif UNITY_IOS || UNITY_ANDROID
        // На мобильных устройствах устанавливаем высокую планку (например, до 120 FPS на экранах с ProMotion / High Refresh Rate)
        Application.targetFrameRate = 120;
#else
        Application.targetFrameRate = -1;
#endif

        // Отключаем засыпание экрана во время игры на мобильных платформах
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
