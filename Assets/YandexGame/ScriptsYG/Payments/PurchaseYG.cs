using System;
using UnityEngine;
using UnityEngine.UI;
using YG.Utils.Pay;
#if YG_TEXT_MESH_PRO
using TMPro;
#endif

namespace YG
{
    [HelpURL("https://www.notion.so/PluginYG-d457b23eee604b7aa6076116aab647ed#10e7dfffefdc42ec93b39be0c78e77cb")]
    public class PurchaseYG : MonoBehaviour
    {
        [Serializable]
        public struct TextLegasy
        {
            public Text title, description, priceValue;
        }
        public TextLegasy textLegasy;

#if YG_TEXT_MESH_PRO
        [Serializable]
        public struct TextMP
        {
            public TextMeshProUGUI title, description, priceValue;
        }
        public TextMP textMP;
#endif

        public ImageLoadYG imageLoad;
        public ImageLoadYG priceCurrencyImage;

        [Tooltip("Добавить Ян/Yan к строке цены")]
        public bool addYAN_toPrice = true;

        public Purchase data = new Purchase();

        [ContextMenu(nameof(UpdateEntries))]
        public void UpdateEntries()
        {
            if (textLegasy.title) textLegasy.title.text = data.title;
            if (textLegasy.description) textLegasy.description.text = data.description;
            if (textLegasy.priceValue)
            {
                textLegasy.priceValue.text = data.priceValue;
                if (addYAN_toPrice) textLegasy.priceValue.text += Yan();
            }

#if YG_TEXT_MESH_PRO
            if (textMP.title) textMP.title.text = data.title;
            if (textMP.description) textMP.description.text = data.description;
            if (textMP.priceValue)
            {
                textMP.priceValue.text = data.priceValue;
                if (addYAN_toPrice) textMP.priceValue.text += Yan();
            }
#endif
            if (imageLoad) imageLoad.Load(data.imageURI);
            if (priceCurrencyImage) priceCurrencyImage.Load(data.priceCurrencyImage);
        }



        public GameObject panelAuth;
        private YandexGame ygInstance;
        private bool isWaitingForAuth = false;

        public void BuyP()
        {
            YandexGame.BuyPayments(data.id); // data.id должен быть определён в вашем классе
        }

        public void BuyPurchase()
        {
                panelAuth.SetActive(false);
                // Устанавливаем флаг ожидания авторизации
                isWaitingForAuth = true;
                // Игрок не авторизован, вызываем окно авторизации
                ygInstance._OpenAuthDialog(); // Статический метод вызывается через имя класса
        }




        void OnEnable()
        {
            YandexGame.GetDataEvent += OnAuthCompleted;
        }

        void OnDisable()
        {
            YandexGame.GetDataEvent -= OnAuthCompleted;
        }

        public void CloseAuthPanel()
        {
            panelAuth.SetActive(false);
        }

        public void OpenAuthPanel()
        {
            if (YandexGame.auth)
            {
                // Игрок уже авторизован, можно продолжить покупку
                BuyP();
            }
            else

                panelAuth.SetActive(true);
        }


        void OnAuthCompleted()
        {
            if (isWaitingForAuth)
            {
                isWaitingForAuth = false; // Сбрасываем флаг
                if (YandexGame.auth)
                {
                   
                    // Игрок успешно авторизовался, продолжаем покупку
                    BuyP();
                }
                else
                {
                    Debug.LogWarning("Авторизация не удалась");
                }
            }
        }





        private string Yan()
        {
            return $" {data.priceCurrencyCode}";
            // if (YandexGame.langPayments == "ru")
            //     return " Ян";
            // else
            //     return " Yan";
        }
    }
}
