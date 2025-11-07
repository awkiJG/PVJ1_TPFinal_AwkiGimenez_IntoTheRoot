using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerKiller : MonoBehaviour
{
    // Referencias asignables desde el Inspector
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Animator animator;
    [SerializeField] private UIFader uiFader;
    private bool isInvincible = false;
    // Llamar cuando el jugador recibe un golpe
    public void TakeHit()
    {
        if (isInvincible) return; // Evitar múltiples muertes simultáneas
        isInvincible = true;
        // aplicar daño
        playerStats.TakeDamage(1);
        GameManager.RequestInputLock(true); 
        // Iniciar corrutinas de muerte
        StartCoroutine(DeathSequence());
    }
    // Corrutina que reproduce la animación de muerte y luego dispara el fade UI
    public IEnumerator DeathSequence()
    {
        animator.SetTrigger("muriendo");
        yield return new WaitForSecondsRealtime(1.2f);
        uiFader.FadeToBlack();
        yield return new WaitForSecondsRealtime(0.5f);
        if (playerStats.GetCurrentLives() > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameManager.RequestInputLock(false);
        }
        else
        {
            SceneManager.LoadScene("GameOverScene");
        }
        isInvincible = false;
    }
}
