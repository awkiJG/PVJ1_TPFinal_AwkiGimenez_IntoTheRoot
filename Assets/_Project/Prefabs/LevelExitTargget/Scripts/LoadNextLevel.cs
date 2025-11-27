using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    private int indexProximaEscena;
    private AsyncOperation preloadOp;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           indexProximaEscena = SceneManager.GetActiveScene().buildIndex + 1;
           preloadOp = SceneManager.LoadSceneAsync(indexProximaEscena);
           if (preloadOp != null) preloadOp.allowSceneActivation = false;
        }
    }
}
