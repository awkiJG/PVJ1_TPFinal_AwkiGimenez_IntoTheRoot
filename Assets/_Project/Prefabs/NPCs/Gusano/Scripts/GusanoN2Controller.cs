using UnityEngine;
using System.Collections;

public class GusanoN2Controller : MonoBehaviour
{
    [SerializeField] public GameObject panelDialogo;
    [SerializeField] private SpriteRenderer imagenDialogo;
    [SerializeField] private Sprite[] spritesDialogo;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private GameObject tapaTunel;
    [SerializeField] private Animator animator;
    // --- Variables de Estado (Privadas) ---
    private bool jugadorEnRango = false;
    private bool haSaludado = false;
    private bool puedeInteractuar = false;
    private bool completado = false; // Para que solo se active una vez

    private Coroutine dialogoCoroutine; // Para detener el diálogo si el jugador se va

    // --- Detección del Jugador (Triggers) ---

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si la misión ya se completó o no es el Player, ignorar.
        if (other.CompareTag("Player"))
        {

            if (completado) return;

            jugadorEnRango = true;

            // Si es la primera vez que entra, iniciar saludo.
            if (!haSaludado)
            {
                dialogoCoroutine = StartCoroutine(SaludoCoroutine());
            }
            else
            {
                // Si ya saludó, solo permitir interactuar
                puedeInteractuar = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // El jugador se fue del rango
            jugadorEnRango = false;
            puedeInteractuar = false;

            // Ocultar todos los prompts
            interactPrompt.SetActive(false);
            panelDialogo.SetActive(false);

            // Si el jugador se va a mitad del saludo, detener la corrutina
            if (dialogoCoroutine != null)
            {
                StopCoroutine(dialogoCoroutine);
                dialogoCoroutine = null;
                haSaludado = false; // Permitir que el saludo se repita si se interrumpió
            }
        }
    }
    // --- Lógica de Interacción (Update) ---
    private void Update()
    {
        if (GameManager.IsInputLocked()) return;
        // 1. Gestionar el aviso de Interacción ('E')
        bool mostrarAviso = jugadorEnRango && puedeInteractuar && !completado;
        if (interactPrompt != null)
        {
            if (interactPrompt.activeSelf != mostrarAviso)
                interactPrompt.SetActive(mostrarAviso);
        }
        else if (mostrarAviso)
        {
            Debug.LogWarning("GusanoN2Controller: 'interactPrompt' no asignado pero se intenta mostrar aviso.");
        }
        // 2. Comprobar si el jugador presiona 'E'
        if (mostrarAviso && Input.GetKeyDown(KeyCode.E))
        {
            // Iniciar la excavación
            StartCoroutine(ExcavarTunel());
        }
    }
    // --- Corrutina de Saludo ---
    private IEnumerator SaludoCoroutine()
    {
        haSaludado = true;
        if (panelDialogo != null)
            panelDialogo.SetActive(true); // Mostrar la caja de diálogo
        else
            Debug.LogWarning("GusanoN2Controller: 'panelDialogo' no asignado al intentar mostrar saludo.");

        yield return new WaitForSeconds(1f);
        // Iterar sobre todos los sprites en el array
        foreach (Sprite spriteMsg in spritesDialogo)
        {
            if (imagenDialogo != null)
                imagenDialogo.sprite = spriteMsg;
            else
                Debug.LogWarning("GusanoN2Controller: 'imagenDialogo' no asignado; no se puede mostrar sprite de diálogo.");
            yield return new WaitForSeconds(4f);
        }
        // Saludo terminado
        puedeInteractuar = true; // Ahora sí puede presionar 'E'
        dialogoCoroutine = null;
    }
    // --- Corrutina de Excavación (Lógica Simplificada) ---
    private IEnumerator ExcavarTunel()
    {
        // 1. Setear estados (para que no se pueda interactuar más)
        animator.SetBool("cavando", true);
        completado = true;
        puedeInteractuar = false;
        interactPrompt.SetActive(false);
        panelDialogo.SetActive(false);
        // 2. Definir el destino
        // (Usamos un Vector3 porque transform.position es Vector3)
        Vector3 destinoFinal = new Vector3(
            50f, // Coordenada X fuera de pantalla
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
        // 4. Limpieza: Destruir el Gusano y la Tapa)
        tapaTunel.SetActive(false);
        Destroy(gameObject);
    }
}
