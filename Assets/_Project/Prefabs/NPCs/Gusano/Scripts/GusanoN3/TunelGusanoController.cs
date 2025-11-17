using UnityEngine;
using System.Collections;

public class TunelGusanoController : MonoBehaviour
{
    public void IniciarMovimiento(int direccion)
    {
        StartCoroutine(MoverCorrutine(direccion));
    }

    private IEnumerator MoverCorrutine(int direccion)
    {
        // 2. Calcular el destino final basado en la dirección  
        Vector3 destinoFinal = new Vector3(
            transform.position.x + direccion * 15f, // Coordenada X fuera de pantalla
            transform.position.y,
            transform.position.z
        );
        // 3. Mover el Gusano (y su hija, la 'tapaTunel')
        while (Vector3.Distance(transform.position, destinoFinal) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                destinoFinal,
                5f * Time.deltaTime
            );
            yield return null; // Esperar al próximo frame
        }
        // Destruir el GameObject una vez completado el movimiento
        Destroy(gameObject);

    }
}
