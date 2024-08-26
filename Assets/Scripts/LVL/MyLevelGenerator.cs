using UnityEngine;

public class MyLevelGenerator : MonoBehaviour
{
    public Transform player; // Ссылка на трансформ игрока (куба)
    public GameObject[] levelPrefabs; // Массив с префабами объектов уровня
    public float distanceToGenerate = 10f; // Дистанция для генерации нового уровня
    public Vector3 spawnOffset; // Смещение для создания объектов

    private float lastGeneratedPosition = 0f;

    private void Update()
    {
        float playerDistance = player.position.y;

        if (playerDistance - lastGeneratedPosition >= distanceToGenerate)
        {
            GenerateLevel();
            lastGeneratedPosition = playerDistance;
        }
    }

    private void GenerateLevel()
    {
        Vector3 spawnPosition = player.position + spawnOffset;

        // Выбираем случайный префаб из массива и создаем объект на уровне
        GameObject prefab = levelPrefabs[Random.Range(0, levelPrefabs.Length)];
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
