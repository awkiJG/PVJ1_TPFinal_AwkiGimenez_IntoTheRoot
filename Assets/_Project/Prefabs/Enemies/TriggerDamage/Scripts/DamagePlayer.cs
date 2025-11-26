using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Verificar si el objeto que entró es el jugador
        if (other.CompareTag("Player"))
        {
            // 2. Buscar el script "PlayerKiller" EN EL OBJETO QUE COLISIONÓ (other)
            PlayerKiller player = other.GetComponent<PlayerKiller>();

            // 3. Verificar si se encontró y usarlo
            if (player != null)
            {
                // 4. Llamar al método en el script que encontramos
                //player.TakeHit();
            }
        }
    }
}