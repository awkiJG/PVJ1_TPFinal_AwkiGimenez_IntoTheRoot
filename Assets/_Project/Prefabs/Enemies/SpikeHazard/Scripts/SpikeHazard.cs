using UnityEngine;

public class SpikeHazard : MonoBehaviour
{
    [SerializeField] private GameObject punta;
    [SerializeField] private Rigidbody2D rb;
    private bool callendo = true;
    private float shrinkSpeed = 0.25f; // unidades de escala por segundo

    public void Initialize(Vector2 direction, float force)
    {
        // 1. APLICAR LA FUERZA DE LANZAMIENTO
        // Usamos ForceMode2D.Impulse para aplicar una fuerza instantánea (la explosión)
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (callendo)
        {
        // Obtener el ángulo de la velocidad actual (en radianes)
        float angleRad = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x);

        // Convertir a grados
        float angleDeg = angleRad * Mathf.Rad2Deg;

        // Aplicar la rotación al transform.
        // Esto asume que tu sprite de espina "apunta hacia la derecha" (eje X positivo)
        transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        }
        else
        {
            // Usar MoveTowards hacia 0 para reducir la magnitud de la escala
            transform.localScale = new Vector3(
                Mathf.MoveTowards(transform.localScale.x, 0f, shrinkSpeed * Time.deltaTime),
                Mathf.MoveTowards(transform.localScale.y, 0f, shrinkSpeed * Time.deltaTime),
                Mathf.MoveTowards(transform.localScale.z, 0f, shrinkSpeed * Time.deltaTime)
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destruir la espina al colisionar con cualquier cosa
       // Destroy(gameObject);
        punta.SetActive(false);
        callendo = false;
        Destroy(gameObject, 4f);
    }
}
