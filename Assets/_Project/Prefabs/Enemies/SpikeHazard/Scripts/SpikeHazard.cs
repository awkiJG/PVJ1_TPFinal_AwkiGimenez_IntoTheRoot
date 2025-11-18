using UnityEngine;

public class SpikeHazard : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public void Initialize(Vector2 direction, float force)
    {
        // 1. APLICAR LA FUERZA DE LANZAMIENTO
        // Usamos ForceMode2D.Impulse para aplicar una fuerza instantánea (la explosión)
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener el ángulo de la velocidad actual (en radianes)
        float angleRad = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x);

        // Convertir a grados
        float angleDeg = angleRad * Mathf.Rad2Deg;

        // Aplicar la rotación al transform.
        // Esto asume que tu sprite de espina "apunta hacia la derecha" (eje X positivo)
        transform.rotation = Quaternion.Euler(0, 0, angleDeg);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destruir la espina al colisionar con cualquier cosa
        Destroy(gameObject);
    }
}
