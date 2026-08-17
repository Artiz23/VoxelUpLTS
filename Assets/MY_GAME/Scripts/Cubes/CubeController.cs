using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class CubeController : MonoBehaviour
{
    [Header("Основные объекты")]
    public GameObject coin;
    public Animation anim;
    public ParticleSystem particleSystems;
    public GameObject[] thornsTrap;
    public GameObject fireObject;
    public GameObject chinaCube;
    public AudioSource audioSource;

    [Header("Смещения спавна")]
    private Vector3 offset = new Vector3(0, 1.3f, 2);
    private Vector3 offsetThorns = new Vector3(0, 0.475f, 2);

    [Header("Настройки таймера")]
    public float countFall = 5.0f;

    [Header("Настройки статичного облака (не падает)")]
    public GameObject staticCloudPrefab;
    [Range(0, 100)] public int spawnChancePercent = 30;
    public float yOffsetMin = -0.5f;
    public float yOffsetMax = 0.5f;

    [Header("Шансы")]
    private int fireScore = 95;
    private int mineScore = 90;
    private int coinScore = 95;

    private CancellationTokenSource _cts;

    private void Start()
    {
        _cts = new CancellationTokenSource();

        if (ScoreManager.score > 50)
        {
            coinScore = 85; mineScore = 85; fireScore = 90;
        }
        else if (ScoreManager.score > 300)
        {
            coinScore = 75; mineScore = 80; fireScore = 85;
        }
        else if (ScoreManager.score > 800)
        {
            coinScore = 65; mineScore = 75; fireScore = 80;
        }

        int valueRandom = Random.Range(0, 100);

        if (valueRandom > coinScore && coin != null)
            Instantiate(coin, transform.position + offset, coin.transform.rotation);

        if (thornsTrap != null && thornsTrap.Length > 0 && thornsTrap[0] != null && valueRandom > mineScore)
            Instantiate(thornsTrap[0], transform.position + offsetThorns, thornsTrap[0].transform.rotation);

        // =============================================================
        if (staticCloudPrefab != null)
        {
            int cloudRandom = Random.Range(0, 100);
            if (cloudRandom < spawnChancePercent)
            {
                float randomY = Random.Range(yOffsetMin, yOffsetMax);
                Vector3 cloudPosition = new Vector3(transform.position.x, transform.position.y + randomY, transform.position.z);
                
                GameObject cloud = Instantiate(staticCloudPrefab, cloudPosition, Quaternion.Euler(0, -90, 0));
                ParticleSystem ps = cloud.GetComponent<ParticleSystem>();
                ps.Play();

                // Запускаем асинхронное ожидание без мусора
                DestroyCloudAfterFinishAsync(ps, cloud, _cts.Token).Forget();
            }
        }
    }

    private async UniTaskVoid DestroyCloudAfterFinishAsync(ParticleSystem ps, GameObject cloudObject, CancellationToken token)
    {
        // Ждём пока частицы живут
        await UniTask.WaitUntil(() => !ps.IsAlive(), cancellationToken: token);
        
        if (cloudObject != null) Destroy(cloudObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (anim != null) anim.Play();
            FallCoroutineHoldingAsync(_cts.Token).Forget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FallAndDestroyCoroutineAsync(_cts.Token).Forget();
        }
    }

    private async UniTaskVoid FallCoroutineHoldingAsync(CancellationToken token)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(countFall), cancellationToken: token);

        if (particleSystems != null) particleSystems.Play();
        if (audioSource != null) audioSource.Play();
        if (chinaCube != null) Destroy(chinaCube);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;

        await UniTask.Delay(System.TimeSpan.FromSeconds(1.0f), cancellationToken: token);
        Destroy(gameObject);
    }

    private async UniTaskVoid FallAndDestroyCoroutineAsync(CancellationToken token)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(0f), cancellationToken: token);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;

        await UniTask.Delay(System.TimeSpan.FromSeconds(1f), cancellationToken: token);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}