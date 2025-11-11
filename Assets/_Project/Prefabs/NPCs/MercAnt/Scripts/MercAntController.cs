using UnityEngine;
using System.Collections;
public class MercAntController : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private SpriteRenderer imagenDialogo;
    [SerializeField] private Sprite[] spritesDialogo;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private GameObject panelDialogo;
    private bool jugadorEnRango = false;
    private bool haSaludado = false;
    private bool puedeInteractuar = false;
    private Coroutine dialogoCoroutine = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;

            // 1. ¿Ya tiene el mapa? Si es así, no hacer nada.
            if (playerStats.HasMap()) return;

            // 2. Si es la primera vez que entra (y no ha saludado), iniciar saludo.
            if (!haSaludado)
            {
                // Iniciar la corrutina de saludo
                dialogoCoroutine = StartCoroutine(SaludoInicial());
            }
            else
            {
                // 3. Si ya saludó (y no ha comprado), marcar como listo para interactuar
                puedeInteractuar = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
            puedeInteractuar = false; // No se puede interactuar si está fuera de rango

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

    public IEnumerator SaludoInicial()
    {
        haSaludado = true;
        panelDialogo.SetActive(true); // Mostrar la caja de diálogo

        // Iterar sobre todos los sprites en el array
        foreach (Sprite spriteMsg in spritesDialogo)
        {
            // Actualizar la imagen del diálogo
            imagenDialogo.sprite = spriteMsg;
            yield return new WaitForSeconds(4f);
        }

        // Saludo terminado
        puedeInteractuar = true; // Ahora sí puede presionar 'E'
        dialogoCoroutine = null;

    }

    void Update()
    {
        if (GameManager.IsInputLocked()) return;
        // 1. Gestionar el aviso de Interacción ('E')
        // (Usamos un if separado para que el aviso se muestre/oculte en tiempo real)
        bool mostrarAviso = jugadorEnRango && puedeInteractuar && !playerStats.HasMap();
        if (interactPrompt.activeSelf != mostrarAviso)
        {
            interactPrompt.SetActive(mostrarAviso);
        }

        // 2. Comprobar si el jugador presiona 'E'
        if (mostrarAviso && Input.GetKeyDown(KeyCode.E))
        {
            RealizarIntercambio();
        }
    }

    void RealizarIntercambio()
    {
        // Intentar consumir 1 Savia (el método devuelve true si tuvo éxito)
        if (playerStats.ReleaseSap(1))
        {
            // ÉXITO: Dar el mapa
            playerStats.GrantMap();
            puedeInteractuar = false; // Desactivar futuras interacciones
            panelDialogo.SetActive(false);

            
        }
        else
        {
            // FALLO: No tiene savia
            Debug.Log("Merc-Ant: ¡No tienes suficiente savia!");
        }
    }
}
