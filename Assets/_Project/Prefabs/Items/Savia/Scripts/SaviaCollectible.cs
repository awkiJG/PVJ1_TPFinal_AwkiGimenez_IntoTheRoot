using UnityEngine;

public class SaviaCollectible : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    // Detectar colisión con el jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Aumentar la cantidad de savia en el jugador
            playerStats.AddSap(1);
            // Destruir el objeto recolectable
            Destroy(gameObject);
        }
    }
}
