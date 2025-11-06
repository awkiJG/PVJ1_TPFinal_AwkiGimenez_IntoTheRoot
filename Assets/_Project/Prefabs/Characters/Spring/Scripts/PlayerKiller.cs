using UnityEngine;
using System.Collections;

public class PlayerKiller : MonoBehaviour
{
    // Estado de invencibilidad después de recibir daño
    private bool isInvincible = false;
    // Referencias asignables desde el Inspector
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Animator animator;
    [SerializeField] private UIFader uiFader;
    // Duración de la invencibilidad tras recibir daño
    private float invincibilityDuration = 4f;
    // Llamar cuando el jugador recibe un golpe
    public void TakeHit()
    {
        // Si ya está invencible, no hacer nada
        if (isInvincible)
        {
            return;
        }
        // Marcar invencible y aplicar daño
        isInvincible = true;
        playerStats.TakeDamage(1);
        GameManager.RequestInputLock(true); 
        // Iniciar corrutinas de muerte e invencibilidad (forma recomendada)
        StartCoroutine(DeathSequence());
        StartCoroutine(InvincibilityCooldown());
    }
    // Corrutina que reproduce la animación de muerte y luego dispara el fade UI
    public IEnumerator DeathSequence()
    {
        animator.SetTrigger("muriendo");
        yield return new WaitForSecondsRealtime(1.2f);
        uiFader.FadeToBlack();
    }
    // Corrutina que espera el tiempo de invencibilidad y la desactiva
    public IEnumerator InvincibilityCooldown()
    {
        yield return new WaitForSecondsRealtime(invincibilityDuration);
        isInvincible = false;
    }
}
