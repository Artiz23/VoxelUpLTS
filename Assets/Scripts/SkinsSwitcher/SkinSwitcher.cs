using UnityEngine;

public class SkinSwitcher : MonoBehaviour
{
    //public GameObject[] skinsOpen; // Массив скинов (3D моделей)
   public GameObject[] skins;
    private int currentSkinIndex = 0;

    void Start()
    {
        LoadSkinStates();
        // Включаем только первый скин при запуске сцены
        //SetActiveSkin(currentSkinIndex);
    }

    public void LeftArrowButton()
    {
        SwitchSkin(-1);
    }

    public void RightArrowButton()
    {
        SwitchSkin(1);
    }

    private void SwitchSkin(int direction)
    {
        // Деактивируем текущий скин
        DeactivateCurrentSkin();

        // Переходим к следующему скину с учетом направления
        currentSkinIndex = (currentSkinIndex + direction) % skins.Length;
        if (currentSkinIndex < 0)
        {
            currentSkinIndex += skins.Length;
        }

        // Активируем новый скин
        SetActiveSkin(currentSkinIndex);
    }

    private void DeactivateCurrentSkin()
    {
        skins[currentSkinIndex].SetActive(false);
        SaveSkinState(currentSkinIndex, false); // Сохраняем деактивацию текущего скина
    }

    public void SetActiveSkin(int index)
    {
        skins[index].SetActive(true);
        SaveSkinState(index, true); // Сохраняем активацию нового скина
    }

    private void SaveSkinState(int index, bool isActive)
    {
        PlayerPrefs.SetInt("Skin_" + index, isActive ? 1 : 0);
        PlayerPrefs.Save();
            Debug.Log("Сохранено состояние скина " + index + ": " + (isActive ? "Активен" : "Неактивен"));

    }

    private void LoadSkinStates()
    {
        for (int i = 0; i < skins.Length; i++)
        {
            bool isActive = PlayerPrefs.GetInt("Skin_" + i, 1) == 1;
            skins[i].SetActive(isActive);
        }
    }
    // public void SetActiveSkinOpen(int index)
    // {
    //     skins[index].SetActive(true);
    // }
}
