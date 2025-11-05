using UnityEngine;
using System.Collections;

public class ZonasOcultas : MonoBehaviour
{
    [SerializeField] private float tiempoDesvanecimiento = 1f; // Tiempo para desvanecerse
    private SpriteRenderer spriteRenderer;
    private bool yaActivado = false; // Para evitar múltiples activaciones

    void Start()
    {
        // Obtener referencia al SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Verificar que tenga SpriteRenderer
        if (spriteRenderer == null)
        {
            Debug.LogError("ZonasOcultas: No se encontró SpriteRenderer en " + gameObject.name);
        }
    }
    // Detecta cuando el jugador entra en contacto con el collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si es el jugador y no se ha activado antes
        if (other.CompareTag("Player") && !yaActivado)
        {
            yaActivado = true;
            StartCoroutine(DesvanecerYDestruir());
        }
    }
    // Corrutina que desvanece progresivamente la opacidad
    private IEnumerator DesvanecerYDestruir()
    {
        float tiempoTranscurrido = 0f;
        Color colorInicial = spriteRenderer.color;
        // Reducir opacidad progresivamente durante el tiempo especificado
        while (tiempoTranscurrido < tiempoDesvanecimiento)
        {
            tiempoTranscurrido += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, tiempoTranscurrido / tiempoDesvanecimiento);
            spriteRenderer.color = new Color(colorInicial.r, colorInicial.g, colorInicial.b, alpha);
            yield return null;
        }
        // Asegurar que la opacidad sea 0 al final
        spriteRenderer.color = new Color(colorInicial.r, colorInicial.g, colorInicial.b, 0f);
        // Destruir el GameObject
        Destroy(gameObject);
    }
}
