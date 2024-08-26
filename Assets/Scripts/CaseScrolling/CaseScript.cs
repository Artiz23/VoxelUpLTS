using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CaseScript : MonoBehaviour
{

    public bool openCase = false;
    public GameObject[] prefabs;
    public GameObject sp;
    public float scrollSpeed = -2000f;
    private float velocity = 3f;
    public WSprites[] ws;
    public Image[] prefabsImages;
    public Image finalDrop;
    public GameObject dropPan;
    private int currentCase;
    private AudioSource _as;
    public AudioClip[] ac;

    private bool wasPlayed = false;
    private bool wasPlayedDrop = false;
    private string Index;
    public GameObject line;
    public GameObject scroll;
    public GameObject scrollPanel;




    [SerializeField] private int priceLowCase = 0;
    [SerializeField] private int priceMiddleCase = 5;
    [SerializeField] private int priceBigCase = 10;

    public GameObject[] inventoryObjects;

    private CarSelection carSelection;
    private SaveManager saveManager;
    void Start()
    {
        carSelection = GameObject.FindWithTag("CarSelection").GetComponent<CarSelection>();
        saveManager = GameObject.FindWithTag("SaveManager").GetComponent<SaveManager>();


        _as = gameObject.GetComponent<AudioSource>();

        gameObject.SetActive(false);
    }

    void Update()
    {
        //Debug.DrawRay(line.transform.position, Vector2.down, Color.red);

        if (openCase)
        {
            scrollSpeed = Mathf.MoveTowards(scrollSpeed, 0, velocity * Time.deltaTime);

            //RaycastHit2D hit = Physics2D.Raycast(Vector2.down, Vector2.up);
            RaycastHit2D hit = Physics2D.Raycast(line.transform.position, Vector2.down);

            if (hit.collider != null)
            {
                if (scrollSpeed == 0)
                {
                    dropPan.SetActive(true);
                    finalDrop.sprite = hit.collider.gameObject.GetComponent<Image>().sprite;


                    // Сохраняем имя спрайта
                    Sprite droppedSprite = finalDrop.sprite;

                    if (!wasPlayedDrop && hit.collider.tag == "Blue")
                    {
                        // Проверяем, какой именно голубой спрайт выпал
                        if (droppedSprite.name == "Blue_Case1_1_")
                        {

                            Debug.Log("1_1B");
                            carSelection.OpenSkin(1);

                            /////////////////////
                            _as.PlayOneShot(ac[1]);
                            wasPlayedDrop = true;


                            inventoryObjects[0].SetActive(true);

                            //YanCloud
                            saveManager.MySave();
                            //YanCloud
                            saveManager.MySave();
                        }
                        else if (droppedSprite.name == "Blue_Case1_2_")
                        {

                            Debug.Log("1_2B");

                            carSelection.OpenSkin(2);

                            /////////////////////
                            _as.PlayOneShot(ac[1]);
                            wasPlayedDrop = true;

                            inventoryObjects[1].SetActive(true);
                            //YanCloud
                            saveManager.MySave();
                        }
                        else if (droppedSprite.name == "Blue_Case1_3_")
                        {

                            Debug.Log("1_3B");

                            carSelection.OpenSkin(3);

                            /////////////////////
                            _as.PlayOneShot(ac[1]);
                            wasPlayedDrop = true;

                            inventoryObjects[2].SetActive(true);
                            //YanCloud
                            saveManager.MySave();
                        }
                        else if (droppedSprite.name == "Blue_Case1_4_")
                        {

                            Debug.Log("1_4B");

                            carSelection.OpenSkin(4);

                            /////////////////////
                            _as.PlayOneShot(ac[1]);
                            wasPlayedDrop = true;
                            inventoryObjects[3].SetActive(true);
                            //YanCloud
                            saveManager.MySave();
                        }
                        else if (droppedSprite.name == "Blue_Case1_5_")
                        {

                            Debug.Log("1_5B");

                            carSelection.OpenSkin(5);

                            /////////////////////
                            _as.PlayOneShot(ac[1]);
                            wasPlayedDrop = true;
                            //YanCloud
                            saveManager.MySave();
                        }


                    }

                    if (!wasPlayedDrop && hit.collider.tag == "Purple")
                    {

                        if (droppedSprite.name == "Purple_Case1_1_")
                        {            //YanCloud
                            saveManager.MySave();

                            Debug.Log("1_1Purple");

                            carSelection.OpenSkin(6);
                            _as.PlayOneShot(ac[1]);
                            wasPlayedDrop = true;
                        }
                        else if (droppedSprite.name == "Purple_Case1_2_")
                        {            //YanCloud
                            saveManager.MySave();

                            Debug.Log("1_2Purple");

                            carSelection.OpenSkin(7);
                            _as.PlayOneShot(ac[1]);
                            wasPlayedDrop = true;
                        }
                        else if (droppedSprite.name == "Purple_Case1_3_")
                        {            //YanCloud
                            saveManager.MySave();

                            Debug.Log("1_3Purple");

                            carSelection.OpenSkin(8);
                            _as.PlayOneShot(ac[1]);
                            wasPlayedDrop = true;
                        }


                    }


                    if (!wasPlayedDrop && hit.collider.tag == "Pink")
                    {
                        //carSelection.OpenSkin(2);

                        if (droppedSprite.name == "Pink_Case1_1_")
                        {
                            //YanCloud
                            saveManager.MySave();
                            Debug.Log("1_1Pink");

                            carSelection.OpenSkin(9);
                            _as.PlayOneShot(ac[1]);
                            wasPlayedDrop = true;
                        }
                        else if (droppedSprite.name == "Pink_Case1_2_")
                        {
                            //YanCloud
                            saveManager.MySave();
                            Debug.Log("1_2Pink");

                            carSelection.OpenSkin(10);
                            _as.PlayOneShot(ac[1]);
                            wasPlayedDrop = true;
                        }

                    }


                    if (!wasPlayedDrop && hit.collider.tag == "Red")
                    {
                        //carSelection.OpenSkin(2);

                        if (droppedSprite.name == "Red_Case1_1_")
                        {
                            //YanCloud
                            saveManager.MySave();
                            Debug.Log("1_1Red");

                            carSelection.OpenSkin(11);
                            _as.PlayOneShot(ac[1]);
                            wasPlayedDrop = true;
                        }
                        else if (droppedSprite.name == "Red_Case1_2_")
                        {
                            //YanCloud
                            saveManager.MySave();
                            Debug.Log("1_2Red");

                            carSelection.OpenSkin(12);
                            _as.PlayOneShot(ac[1]);
                            wasPlayedDrop = true;
                        }
                    }


                    if (!wasPlayedDrop && hit.collider.tag == "Yellow")
                    {
                        if (droppedSprite.name == "Yellow_Case1_1_")
                        {
                            //YanCloud
                            saveManager.MySave();
                            Debug.Log("1_1Yellow");
                            carSelection.OpenSkin(13);


                            _as.PlayOneShot(ac[2]);
                            wasPlayedDrop = true;
                        }
                    }

                    ///////////////////////////////////////////////






                    // if (hit.collider.gameObject.tag != "Yellow")
                    // {
                    //     _as.PlayOneShot(ac[1]);
                    //     wasPlayedDrop = true;
                    // }




                }
                else if (!wasPlayed)
                {
                    _as.PlayOneShot(ac[0]);
                    Index = hit.collider.gameObject.name;
                    wasPlayed = true;
                }
                if (Index != hit.collider.gameObject.name)
                {
                    wasPlayed = false;
                }
            }
            else if (scrollSpeed <= 0)
            {
                scrollSpeed = Mathf.MoveTowards(scrollSpeed, -30f, velocity * Time.deltaTime);
            }
        }
    }
    public void caseBttn(int caseInt)
    {
        int price;

        // Определение цены в зависимости от выбранного кейса
        switch (caseInt)
        {
            case 0: // Маленький кейс
                price = priceLowCase;
                break;
            case 1: // Средний кейс
                price = priceMiddleCase;
                break;
            case 2: // Большой кейс
                price = priceBigCase;
                break;
            default: // По умолчанию используем цену маленького кейса
                price = priceLowCase;
                break;
        }

        if (SaveManager.instance.money >= price || price == 0)
        {
            gameObject.SetActive(true);
            scroll.SetActive(true);

            SaveManager.instance.money -= price;
            SaveManager.instance.Save();
            //YanCloud
            saveManager.MySave();

            openCase = true;

            currentCase = caseInt;
            simulateCases();
            velocity = Random.Range(210, 320.5f);
            _as.PlayOneShot(ac[3]);
        }
        else
        {
            gameObject.SetActive(true);
            _as.PlayOneShot(ac[4]);
        }
    }

  

    public void Close()
    {
        // Очистка спавненных предметов
        foreach (Transform child in sp.transform)
        {
            Destroy(child.gameObject);
        }

        // Сброс всех значений и состояний на начальные
        openCase = false;
        dropPan.SetActive(false);
        wasPlayed = false;
        wasPlayedDrop = false;
        Index = null;
        finalDrop.sprite = null; // Сброс финальной картинки
        gameObject.SetActive(false);
        scroll.SetActive(false);

        scrollSpeed = -2000f;

        scrollPanel.transform.localPosition = new Vector3(4503f, 50f, 0);
    }





    void simulateCases()
    {
        for (int a = 0; a < 90; a++)
        {
            int rand = Random.Range(0, 1000);
            int randWeapon = 0;

            if (rand <= 600)
            {
                randWeapon = 0;
                prefabsImages[randWeapon].sprite = ws[currentCase].blueW[Random.Range(0, ws[currentCase].blueW.Length)];

            }
            else if (rand > 600 && rand <= 830)
            {
                randWeapon = 1;
                prefabsImages[randWeapon].sprite = ws[currentCase].purpleW[Random.Range(0, ws[currentCase].purpleW.Length)];
            }
            else if (rand > 830 && rand <= 930)
            {
                randWeapon = 2;
                prefabsImages[randWeapon].sprite = ws[currentCase].pinkW[Random.Range(0, ws[currentCase].pinkW.Length)];
            }
            else if (rand > 930 && rand <= 990)
            {
                randWeapon = 3;
                prefabsImages[randWeapon].sprite = ws[currentCase].redW[Random.Range(0, ws[currentCase].redW.Length)];
            }
            else if (rand > 990)
            {
                randWeapon = 4;
                prefabsImages[randWeapon].sprite = ws[currentCase].knife[Random.Range(0, ws[currentCase].knife.Length)];
            }
            GameObject obj = Instantiate(prefabs[randWeapon], new Vector2(0, 0), Quaternion.identity) as GameObject;
            obj.transform.SetParent(sp.transform);
            obj.transform.localScale = new Vector2(1, 1);
            obj.name = obj.name + a.ToString();
        }
    }
}