using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CubeJump : MonoBehaviour
{
    public GameObject hatObject;
    public ParticleSystem particleSystemHat;

    public GameObject[] objectsHats; // Массив объектов
    private int currentIndex = -1; // Индекс текущего активного объекта


    public Transform childToRotateBottle;
    public float rotationSpeedBottle = 1.0f;
    private Quaternion targetRotation;
    private bool isRotating = false;
    private Vector3 rotationAxis;
    private float rotationAmount;
    public float rotationProgress = 0f;





    public bool isUp = true;
















    ////////////////////////////////////////////////////////////////////////////////


    //from PlayerController////////////////////////////////
    private RandomCube _randomCube;
    // private PlayerDeath playerDeath;

    //Scene



    private bool isFallingTriggered = false;

    private float lastYPosition;
    private int previousYPosition; // Здесь хранится предыдущая позиция Y




    private StartMenu startMenu;




    /////////////////////////////////////////////////////////

    // [SerializeField] private CinemachineVirtualCamera vc;
    // [SerializeField] private AudioListener listener;

    public bool canMove = true;

    public PlayerDeath playerDeath;
    //Sound
    private SoundManager soundManager;

    private Vector3 targetPosition; // Целевая позиция для перемещения
    public float moveSpeed = 5.0f; // Скорость перемещения

    private Animator animator;
    public Animator animatorHat;

    private float timeSinceLastCube = 0f;


    public float rotationSpeed = 5.0f; // Скорость сглаживания поворотов

    private bool shouldRotate = false;
    private Vector3 desiredRotation = Vector3.zero; // Желаемый поворот в углах Эйлера
    public Transform childToRotate;




    private Vector2 touchStartPos; // Начальная позиция касания
    private Vector2 touchEndPos; // Конечная позиция касания
    private float swipeThreshold = 50f; // Порог свайпа

    public float fallSpeed = 23.0f; // Скорость падения
    public bool isFalling;
    private bool isSwiping = false;


    public bool isMove = true;
    // bool newIsOnCube = true;
    public Transform rayPosition;
    public Transform rayPositionRight;
    public Transform rayPositionLeft;


    // Определяем слой, который мы хотим учитывать
    public LayerMask layerToInclude;

    // Маска слоев, для использования в raycast
    private int layerMask;

    public bool canJumpDownR = false;
    public bool canJumpDownL = false;


    public bool gameStarted = false;
    private bool touchDown = false;
    public static bool isShop = false;




    private bool isBottleWater = false;
    private bool isHat = false;


    private PauseMenu pauseMenu;
    


    //public PauseMenu pauseMenu;
    void Start()
    {
        ////////////////////////////////////////////////////////////////////////////////////////

        targetRotation = childToRotate.rotation;

        if (gameObject.name == "Default(Clone)")
        {
            isBottleWater = true;
        }
        else
        {
            isBottleWater = false;
        }


        if (gameObject.name == "Case1_Y1(Clone)")
        {
            isHat = true;
        }
        else
        {
            isHat = false;
        }


        //////////////////////////////////////////////////////
        isShop = false;
        StartCoroutine(WaitGameStart());
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
        startMenu = GameObject.Find("StartGame").GetComponent<StartMenu>();
        _randomCube = GameObject.Find("GenerateRandomCube").GetComponent<RandomCube>();
        playerDeath = GetComponent<PlayerDeath>();

        // Инициализация начальной позиции Y персонажа
        lastYPosition = transform.position.y;
        previousYPosition = Mathf.RoundToInt(transform.position.y); // Инициализация предыдущей позиции Y

        ///////////////////////////////////////////////

        // Создаем маску, включая только выбранный слой
        layerMask = 1 << layerToInclude;

        // Инвертируем маску, чтобы игнорировать все слои, кроме выбранного
        layerMask = ~layerMask;




        targetPosition = transform.position;
        animator = GetComponent<Animator>();

        // Получаем компонент SoundManager на текущем игровом объекте
        soundManager = GetComponent<SoundManager>();








    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("thornsTrap"))
        {
            playerDeath.Die();
            //StartCoroutine(DelayedRestartScene());

            StartCoroutine(DelayedPauseMenuActivation());
        }
    }

     IEnumerator DelayedPauseMenuActivation()
    {
        yield return new WaitForSeconds(1f); // Задержка в 1 секунду
        pauseMenu.ActivePauseMenu(); // Вызов метода после задержки
    }
 


    public void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public IEnumerator DelayedRestartScene()
    {
        yield return new WaitForSeconds(1.0f); // Подождать 1 секунду
        RestartScene();
    }

    private IEnumerator EffectDead()
    {
        yield return new WaitForSeconds(0.3f); // Подождать 0.3 секунды
        playerDeath.Die(); // Вызов метода смерти игрока
    }


    private IEnumerator WaitGameStart()
    {
        yield return new WaitForSeconds(0.6f);
        isMove = true;
    }






    void SetNewTargetRotation()
    {
        if (isRotating)
        {
            // Обновляем текущий поворот перед изменением цели
            childToRotate.rotation = Quaternion.Slerp(childToRotate.rotation, targetRotation, rotationProgress);

            if (!Mathf.Approximately(childToRotate.rotation.eulerAngles.x, 180f) && isUp == true)
            {
                isUp = false;
                childToRotate.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (!Mathf.Approximately(childToRotate.rotation.eulerAngles.x, 0f) && isUp == false)
            {
                childToRotate.rotation = Quaternion.Euler(180f, 0f, 0f);
                isUp = true;
            }
        }

        rotationProgress = 0f;
        targetRotation = Quaternion.AngleAxis(rotationAmount, rotationAxis) * childToRotate.rotation;
        isRotating = true;
    }

    void Update()
    {
        if (isRotating)
        {
            // Прогресс анимации вращения
            rotationProgress += Time.deltaTime * rotationSpeedBottle;

            if (rotationProgress >= 1f)
            {
                rotationProgress = 1f;
                isRotating = false;
            }

            // Плавно вращаем объект
            childToRotate.rotation = Quaternion.Slerp(childToRotate.rotation, targetRotation, rotationProgress);
        }








        //SCORE////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        int currentYPosition = Mathf.RoundToInt(transform.position.y);
        int deltaY = currentYPosition - previousYPosition;

        if (deltaY != 0)
        {
            ScoreManager.score += deltaY;
        }

        previousYPosition = currentYPosition;
        ///////////////////////////////////////////////


        // Вызываем функцию перезапуска сцены из контроллера сцены
        if (isFalling && !isFallingTriggered)
        {
            isFallingTriggered = true;
            // StartCoroutine(DelayedRestartScene());
            StartCoroutine(EffectDead());
            StartCoroutine(DelayedPauseMenuActivation());
        }


        /////////////////////////////////////////////////////////////////


        // Проверяем, стоит ли игрок на кубе
        bool newIsOnCube = IsOnCube();

        // Если луч не видит куб вправо, можно прыгнуть вниз справа
        canJumpDownR = !Physics.Raycast(rayPositionRight.position, Vector3.right, 2.0f, ~(1 << LayerMask.NameToLayer("ignoreRayCast")));

        // Если луч не видит куб влево, можно прыгнуть вниз слева
        canJumpDownL = !Physics.Raycast(rayPositionLeft.position, Vector3.left, 2.0f, ~(1 << LayerMask.NameToLayer("ignoreRayCast")));





        // Рисуем луч только для визуализации, он не влияет на фактическое столкновение с объектами
        // Debug.DrawRay(rayPosition.position, Vector3.back * 1.0f, Color.red);

        // Debug.DrawRay(rayPositionRight.position, Vector3.right * 2.0f, Color.red);
        // Debug.DrawRay(rayPositionLeft.position, Vector3.left * 2.0f, Color.red);






        // по нажатию на экран или левой кнопкой мыши
        if (isMove == true && newIsOnCube && canMove)
        {

            timeSinceLastCube = 0f;
            // Обработка свайпа мышью
            // if (Input.GetMouseButtonDown(0))
            // {
            //     touchStartPos = Input.mousePosition;
            //     isSwiping = true; // Свайп начался
            // }
            // else if (Input.GetMouseButtonUp(0))
            // {
            //     touchEndPos = Input.mousePosition;
            //     Vector2 swipeVector = touchEndPos - touchStartPos;

            //     if (isSwiping && swipeVector.magnitude > swipeThreshold)
            //     {
            //         // Код обработки свайпа здесь
            //         // ... (предыдущий код для свайпа)
            //         isSwiping = false; // Свайп завершился
            //     }
            //     else  
            //     {


            //         // Обработка обычного клика здесь
            //         targetPosition += new Vector3(0, 1.0f, 2.0f);
            //         shouldRotate = true;
            //         if (isBottleWater == true)
            //         {
            //             rotationAxis = Vector3.right; // Вращение вокруг оси X
            //             rotationAmount = -180f;
            //             SetNewTargetRotation();
            //         }
            //         else
            //         {
            //             desiredRotation = new Vector3(0, 0, 0);
            //         }

            if (isBottleWater == false)
            {
                animator.SetTrigger("Jump");
            }


            // if (isHat == true)
            // {
            //     StartCoroutine(ToggleActive());
            // }









            //     }
            // }
        }









        // if (isMove == true)
        // {
        //     if (Input.GetMouseButtonDown(0))
        //     {

        //         _randomCube.CreateRandomCube();

        //         if (gameStarted == false)
        //         {
        //             startMenu.StartGame();
        //             gameStarted = true;
        //         }
        //     }
        //     else if (Input.GetMouseButtonUp(0))
        //     {


        //         if (gameStarted == false)
        //         {
        //             startMenu.StartGame();
        //             gameStarted = true;
        //         }
        //     }
        // }




        if (isMove == true && newIsOnCube && canMove /*&& pauseMenu.pauseOnOff == false*/)
        {
            timeSinceLastCube = 0f;
            // Обработка свайпов мыши
            // if (Input.GetMouseButtonDown(0))
            // {

            //     //_randomCube.CreateRandomCube();

            //     touchStartPos = Input.mousePosition;

            //     touchDown = true;
            // }
            // else if (Input.GetMouseButtonUp(0) && touchDown == true)
            // {
            //     touchDown = false;
            //     // _randomCube.CreateRandomCube();
            //     touchEndPos = Input.mousePosition;
            //     Vector2 swipeVector = touchEndPos - touchStartPos;

            //     if (swipeVector.magnitude > swipeThreshold)
            //     {
            //         // Определите направление свайпа и совершите соответствующее действие
            //         if (Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y))
            //         {
            //             if (swipeVector.x > 0)
            //             {
            //                 //_randomCube.CreateRandomCube();
            //                 if (gameStarted == false)
            //                 {

            //                     gameStarted = true;
            //                 }

            //                 // Свайп вправо
            //                 if (canJumpDownR == true)
            //                 {
            //                     targetPosition += new Vector3(2.0f, -1.0f, 0);
            //                 }
            //                 else
            //                 {
            //                     targetPosition += new Vector3(2.0f, 1.0f, 0);
            //                 }






            //                 shouldRotate = true;
            //                 if (isBottleWater == true)
            //                 {
            //                     rotationAxis = Vector3.forward; // Вращение вокруг оси X
            //                     rotationAmount = 180f;
            //                     SetNewTargetRotation();

            //                 }
            //                 else
            //                 {
            //                     desiredRotation = new Vector3(0, 90, 0);
            //                 }

            //                 if (isBottleWater == false)
            //                 {
            //                     animator.SetTrigger("Jump");
            //                 }


            //                 if (isHat == true)
            //                 {
            //                     StartCoroutine(ToggleActive());
            //                 }

            //                 // soundManager.PlayJumpSound();



            //             }
            //             else
            //             {
            //                 //_randomCube.CreateRandomCube();
            //                 if (gameStarted == false)
            //                 {

            //                     gameStarted = true;
            //                 }

            //                 // Свайп влево
            //                 if (canJumpDownL == true)
            //                 {
            //                     targetPosition += new Vector3(-2.0f, -1.0f, 0);
            //                 }
            //                 else
            //                 {
            //                     targetPosition += new Vector3(-2.0f, 1.0f, 0);
            //                 }





            //                 shouldRotate = true;
            //                 if (isBottleWater == true)
            //                 {
            //                     rotationAxis = Vector3.forward; // Вращение вокруг оси X
            //                     rotationAmount = -180f;
            //                     SetNewTargetRotation();

            //                 }
            //                 else
            //                 {
            //                     desiredRotation = new Vector3(0, -90f, 0);
            //                 }


            //                 if (isBottleWater == false)
            //                 {
            //                     animator.SetTrigger("Jump");
            //                 }


            //                 if (isHat == true)
            //                 {
            //                     StartCoroutine(ToggleActive());
            //                 }
            //                 //soundManager.PlayJumpSound();


            //             }
            //         }
            //         else
            //         {
            //             if (swipeVector.y > 0)
            //             {
            //                 //_randomCube.CreateRandomCube();
            //                 if (gameStarted == false)
            //                 {

            //                     gameStarted = true;
            //                 }

            //                 // Свайп вверх
            //                 targetPosition += new Vector3(0, 1.0f, 2.0f);
            //                 shouldRotate = true;
            //                 if (isBottleWater == true)
            //                 {
            //                     rotationAxis = Vector3.right; // Вращение вокруг оси X
            //                     rotationAmount = -180f;
            //                     SetNewTargetRotation();
            //                 }
            //                 else
            //                 {
            //                     desiredRotation = new Vector3(0, 0, 0);
            //                 }
            //                 if (isBottleWater == false)
            //                 {
            //                     animator.SetTrigger("Jump");
            //                 }


            //                 if (isHat == true)
            //                 {
            //                     StartCoroutine(ToggleActive());
            //                 }



            //             }

            //         }
            //     }
            // }
        }


        //AWSD/////////////////////////////
        if (isMove == true)
        {
            // Если игрок находится на кубе, сбрасываем таймер и разрешаем перемещение
            if (newIsOnCube && canMove /*&& pauseMenu.pauseOnOff == false*/)
            {
                timeSinceLastCube = 0f;

                // Перемещение влево
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (gameStarted == false)
                    {
                        startMenu.StartGame();
                        gameStarted = true;
                    }




                    _randomCube.CreateRandomCube();

                    if (canJumpDownL == true)
                    {
                        targetPosition += new Vector3(-2.0f, -1.0f, 0);
                    }
                    else
                    {
                        targetPosition += new Vector3(-2.0f, 1.0f, 0);
                    }



                    shouldRotate = true;
                    //desiredRotation = new Vector3(0, -90.0f, 0); // Поворот влево

                    if (isBottleWater == true)
                    {
                        rotationAxis = Vector3.forward; // Вращение вокруг оси X
                        rotationAmount = -180f;
                        SetNewTargetRotation();

                    }
                    else
                    {
                        desiredRotation = new Vector3(0, -90, 0);
                    }
                    if (isBottleWater == false)
                    {
                        animator.SetTrigger("Jump");
                    }


                    if (isHat == true)
                    {
                        StartCoroutine(ToggleActive());
                    }



                }
                // Перемещение вправо
                else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (gameStarted == false)
                    {
                        startMenu.StartGame();
                        gameStarted = true;
                    }



                    _randomCube.CreateRandomCube();
                    if (canJumpDownR == true)
                    {
                        targetPosition += new Vector3(2.0f, -1.0f, 0);
                    }
                    else
                    {
                        targetPosition += new Vector3(2.0f, 1.0f, 0);
                    }




                    shouldRotate = true;
                    // desiredRotation = new Vector3(0, 90.0f, 0); // Поворот вправо

                    if (isBottleWater == true)
                    {
                        rotationAxis = Vector3.forward; // Вращение вокруг оси X
                        rotationAmount = 180f;
                        SetNewTargetRotation();

                    }
                    else
                    {
                        desiredRotation = new Vector3(0, 90, 0);
                    }
                    if (isBottleWater == false)
                    {
                        animator.SetTrigger("Jump");
                    }

                    //  if(isDrone == true)
                    // {
                    //     animatorHat.SetTrigger("hatTp");
                    // }
                    //soundManager.PlayJumpSound();

                    if (isHat == true)
                    {
                        StartCoroutine(ToggleActive());
                    }


                }
                // Перемещение назад
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {

                    if (gameStarted == false)
                    {
                        startMenu.StartGame();
                        gameStarted = true;
                    }

                    targetPosition += new Vector3(0, -1.0f, -2.0f);
                    shouldRotate = true;

                    // desiredRotation = new Vector3(0, 180.0f, 0); // Поворот назад


                    if (isBottleWater == true)
                    {
                        rotationAxis = Vector3.right; // Вращение вокруг оси X
                        rotationAmount = 180f;
                        SetNewTargetRotation();

                    }
                    else
                    {
                        desiredRotation = new Vector3(0, 180, 0);
                    }


                    if (isBottleWater == false)
                    {
                        animator.SetTrigger("Jump");
                    }

                    //  if(isDrone == true)
                    // {
                    //     animatorHat.SetTrigger("hatTp");
                    // }
                    //soundManager.PlayJumpSound();

                    if (isHat == true)
                    {
                        StartCoroutine(ToggleActive());
                    }


                }


                // Перемещение вперед
                else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {

                    _randomCube.CreateRandomCube();

                    if (gameStarted == false)
                    {
                        startMenu.StartGame();
                        gameStarted = true;
                    }


                    targetPosition += new Vector3(0, 1.0f, 2.0f);
                    shouldRotate = true;


                    /////////////////////////////////////////////////////////
                    //desiredRotation = new Vector3(0, 0, 0);
                    if (isBottleWater == true)
                    {
                        rotationAxis = Vector3.right; // Вращение вокруг оси X
                        rotationAmount = -180f;
                        SetNewTargetRotation();
                    }
                    else
                    {
                        desiredRotation = new Vector3(0, 0, 0);
                    }
                    ///////////////////////////////////////////////////////

                    if (isBottleWater == false)
                    {
                        animator.SetTrigger("Jump");
                    }

                    if (isHat == true)
                    {
                        StartCoroutine(ToggleActive());
                    }






                    //soundManager.PlayJumpSound();

                }





            }
            else
            {
                // Если игрок не находится на кубе, увеличиваем время с момента последнего нахождения на кубе
                timeSinceLastCube += Time.deltaTime;





                // Если прошла 0.1 секунда, персонаж падает вниз
                if (timeSinceLastCube >= 0.1f && playerDeath.isDead == false)
                {

                    Fall();

                }
            }

            // Применяем измененную позицию
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }



        // Если должны поворачивать персонажа

        // Глобальный поворот с сглаживанием
        if (isBottleWater == false)
        {
            childToRotate.localRotation = Quaternion.Slerp(childToRotate.localRotation, Quaternion.Euler(desiredRotation), Time.deltaTime * rotationSpeed);
        }

    }

    //метод падения персонажа
    public void Fall()
    {
        // Имитация падения персонажа
        targetPosition += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
        isFalling = true;
    }


  bool IsOnCube()
{
    // Определение маски слоя для игнорирования лучей
    int ignoreRaycastLayer = 1 << LayerMask.NameToLayer("ignoreRayCast");

    // Проверка направления лучей и хранение результатов
    bool hitBack = Physics.Raycast(rayPosition.position, Vector3.back, out RaycastHit hitBackResult, 1.0f);
    bool hitRight = Physics.Raycast(rayPositionRight.position, Vector3.right, out RaycastHit hitRightResult, 2.0f, ~ignoreRaycastLayer);
    bool hitLeft = Physics.Raycast(rayPositionLeft.position, Vector3.left, out RaycastHit hitLeftResult, 2.0f, ~ignoreRaycastLayer);

    // Проверка на столкновение с кубом
    if (hitBack && hitBackResult.collider.CompareTag("Cube"))
    {
        canJumpDownR = false;
        canJumpDownL = false;
        return true; // Мы стоим на кубе, возвращаем true
    }

    if (hitRight && hitRightResult.collider.CompareTag("Cube"))
    {
        canJumpDownR = false;
        canJumpDownL = true; // Можем прыгнуть влево
    }

    if (hitLeft && hitLeftResult.collider.CompareTag("Cube"))
    {
        canJumpDownR = true; // Можем прыгнуть вправо
        canJumpDownL = false;
    }

    // Ни один из лучей не попал на куб, возвращаем false
    if (!hitBack && !hitRight && !hitLeft)
    {
        canJumpDownR = true; // Можем прыгнуть вправо
        canJumpDownL = true; // Можем прыгнуть влево
    }

    return false;
}




    private IEnumerator ToggleActive()
    {

        hatObject.SetActive(false); // Деактивировать объект
        ActivateRandomObject();
        yield return new WaitForSeconds(0.3f); // Ждать 1 секунду
        particleSystemHat.Play();
        hatObject.SetActive(true); // Активировать объект

    }

    void ActivateRandomObject()
    {
        // Деактивируем все объекты
        foreach (GameObject obj in objectsHats)
        {
            obj.SetActive(false);
        }

        // Выбираем случайный индекс и активируем соответствующий объект
        currentIndex = Random.Range(0, objectsHats.Length);
        objectsHats[currentIndex].SetActive(true);
    }
}
