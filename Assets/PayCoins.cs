using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using UnityEngine.UI;


public class PayCoins : MonoBehaviour
{

  private SaveManager saveManager;

  private void Start()
  {

    saveManager = GameObject.FindWithTag("SaveManager").GetComponent<SaveManager>();
   
  }
  // Подписываемся на ивенты успешной/неуспешной покупки
  private void OnEnable()
  {
    YandexGame.PurchaseSuccessEvent += SuccessPurchased;
    YandexGame.PurchaseFailedEvent += FailedPurchased;
  }

  private void OnDisable()
  {
    YandexGame.PurchaseSuccessEvent -= SuccessPurchased;
    YandexGame.PurchaseFailedEvent -= FailedPurchased;
  }

 


  // Покупка успешно совершена, выдаём товар
  public void SuccessPurchased(string id)
  {
    // Ваш код для обработки покупки. Например:
    if (id == "coin")
    {
      SaveManager.instance.money += 150;

    }

    if (id == "coin2")
    {
      SaveManager.instance.money += 400;

    }

    if (id == "coin3")
    {
      SaveManager.instance.money += 900;

    }

    if (id == "coin4")
    {
      SaveManager.instance.money += 2000;
    }
    SaveManager.instance.Save();
    saveManager.MySave();

    YandexGame.SaveProgress();


  }

  // Покупка не была произведена
  void FailedPurchased(string id)
  {
    // Например, можно открыть уведомление о неуспешности покупки.
  }
}
