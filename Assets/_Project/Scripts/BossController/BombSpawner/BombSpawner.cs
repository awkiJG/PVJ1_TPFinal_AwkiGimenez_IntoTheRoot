using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public void FireBomb(int spikeCount, float force, float scale, GameObject bombPrefab)
    {
        // 1. Instanciar la bomba en la posición de este Spawner
        GameObject bombGO = Instantiate(bombPrefab, transform.position, transform.rotation);

        // 2. Obtener el script de lógica de la bomba
        ExplosiveBomb bombScript = bombGO.GetComponent<ExplosiveBomb>();

        // 3. Pasar los datos de configuración
        // (La bomba se encargará de aplicarse su propia escala y guardar los datos)
        if (bombScript != null)
        {
            bombScript.Initialize(spikeCount, force, scale);
        }
        else
        {
            Debug.LogWarning("El prefab de la bomba no tiene el script 'ExplosiveBomb' adjunto.");
        }
    }
}
