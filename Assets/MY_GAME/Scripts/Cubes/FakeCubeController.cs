using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class FakeCubeController : MonoBehaviour
{
    private GameObject player;
    private CancellationTokenSource _cts;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        _cts = new CancellationTokenSource();
        CheckFakeCubeAsync(_cts.Token).Forget();
    }

    private async UniTaskVoid CheckFakeCubeAsync(CancellationToken token)
    {
        while (true)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(1.0f), cancellationToken: token);
            
            if (player == null || token.IsCancellationRequested) return;

            if (player.transform.position.y - transform.position.y > 6)
            {
                Destroy(gameObject);
                return;
            }
        }
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