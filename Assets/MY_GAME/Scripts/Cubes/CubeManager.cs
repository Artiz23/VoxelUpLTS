using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class CubeManager : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FallAndDestroyAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }

    private async UniTaskVoid FallAndDestroyAsync(CancellationToken token)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(0f), cancellationToken: token);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;

        await UniTask.Delay(System.TimeSpan.FromSeconds(0.5f), cancellationToken: token);
        Destroy(gameObject);
    }
}