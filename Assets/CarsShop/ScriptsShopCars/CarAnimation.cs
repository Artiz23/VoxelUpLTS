using UnityEngine;

public class CarAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 finalPosition;
    private Vector3 initialPosition;
    private float speed = 20.0f; // Скорость анимации, можно настроить в редакторе или коде

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Используйте Time.deltaTime для нормализации скорости анимации
        transform.position = Vector3.Lerp(transform.position, finalPosition, speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        transform.position = initialPosition;
    }
}
