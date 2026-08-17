using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    private Text txt;
    private int lastMoney = -1;

    private void Awake()
    {
        txt = GetComponent<Text>();
    }
    private void Update()
    {
        if (SaveManager.instance != null && txt != null)
        {
            int currentMoney = SaveManager.instance.money;
            if (currentMoney != lastMoney)
            {
                lastMoney = currentMoney;
                txt.text = currentMoney.ToString();
            }
        }
    }
}