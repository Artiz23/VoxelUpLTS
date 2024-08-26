using UnityEngine;
using System.Collections; // Добавляем это using для использования IEnumerator


public class StartCubeController : MonoBehaviour
{
  

  

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            StartCoroutine(FallAndDestroyCoroutine());
        }
    }

    

    private IEnumerator FallAndDestroyCoroutine()
    {
        yield return new WaitForSeconds(0f); // Ждем 0 секунды перед падением
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // Разрешаем физику для падения

        yield return new WaitForSeconds(0.5f); // Ждем еще 0,5 секунды перед удалением
        Destroy(gameObject); // Удаляем объект куба
    }

   
     
}
