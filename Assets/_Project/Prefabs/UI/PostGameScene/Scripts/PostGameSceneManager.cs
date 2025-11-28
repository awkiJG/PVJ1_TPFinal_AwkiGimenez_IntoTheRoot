using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PostGameSceneManager : MonoBehaviour
{
    [SerializeField] private Animator animatorFader;
    private bool isLoading = false;

    // Update is called once per frame
    void Update()
    {
        if (isLoading) return;
        if (Input.anyKeyDown)
        {
            StartCoroutine(ReturnToMenuCoroutine());
        }
    }
    private IEnumerator ReturnToMenuCoroutine()
    {
        if (isLoading) yield break;
        isLoading = true;

        if (animatorFader != null)
        {
            animatorFader.SetTrigger("FadeIn");
            yield return new WaitForSeconds(1f); // Esperar a que termine la animación de fade out
        }

        SceneManager.LoadScene("MainMenu");
    }
}
