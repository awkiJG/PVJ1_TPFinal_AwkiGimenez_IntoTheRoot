using UnityEngine;

public class GusanoFSM : MonoBehaviour
{
    // Estado del Gusano: público para que otros scripts puedan usar el tipo si es necesario
    public enum GusanoState
    {
        ESPERANDO_JUGADOR, // (Estado inicial)
        DIALOGO_INICIAL,
        ESPERANDO_ACEPTAR_MISION,
        EXCAVANDO_TUNEL_1,
        ESPERANDO_HUEVO,
        DIALOGO_HUEVO,
        ESPERANDO_ENTREGAR_HUEVO,
        EXCAVANDO_TUNEL_2,
        COMPLETADO
    }
    private GusanoState currentState;
    public GusanoState CurrentState => currentState;
    private bool jugadorEnRango = false;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GusanoDialogo dialogoController;
    [SerializeField] private GusanoExcavador excavadarController;
    [SerializeField] private TunelGusanoController tunel_1;
    [SerializeField] private TunelGusanoController tunel_2;
    [SerializeField] private GameObject tapaTunel_1;
    [SerializeField] private GameObject tapaTunel_2;
    [SerializeField] private GameObject interactPrompt;

    void Start()
    {
        currentState = GusanoState.ESPERANDO_JUGADOR;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
        }
    }
    public void ChangeState(GusanoState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case GusanoState.DIALOGO_INICIAL:
                dialogoController.MostrarDialogo();
                break;
            case GusanoState.ESPERANDO_ACEPTAR_MISION:
                interactPrompt.SetActive(true);
                break;
            case GusanoState.EXCAVANDO_TUNEL_1:
                excavadarController.IniciarMovimiento(-1, 15f);
                Invoke("moverTunel1", 0.7f);
                break;
            case GusanoState.ESPERANDO_HUEVO:
                tapaTunel_1.SetActive(false);
                break;
            case GusanoState.ESPERANDO_ENTREGAR_HUEVO:
                tapaTunel_1.SetActive(false);
                break;
            case GusanoState.DIALOGO_HUEVO:
                dialogoController.MostrarDialogo();
                break;
            case GusanoState.EXCAVANDO_TUNEL_2:
                excavadarController.IniciarMovimiento(1, 20f);
                Invoke("moverTunel2", 1.55f);
                break;
            case GusanoState.COMPLETADO:
                tapaTunel_2.SetActive(false);
                Destroy(gameObject);
                break;
        }
    }
    void Update()
    {
        if (jugadorEnRango)
        {
            switch (currentState)
            {
                case GusanoState.ESPERANDO_JUGADOR:
                    ChangeState(GusanoState.DIALOGO_INICIAL);
                    break;
                case GusanoState.ESPERANDO_ACEPTAR_MISION:
                    interactPrompt.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        ChangeState(GusanoState.EXCAVANDO_TUNEL_1);
                        interactPrompt.SetActive(false);
                        dialogoController.GetDialogoObject().SetActive(false);
                    }
                    break;
                case GusanoState.ESPERANDO_HUEVO:
                    if (playerStats.HasMosquitoEgg())
                    {
                        ChangeState(GusanoState.DIALOGO_HUEVO);
                    }
                    break;
                case GusanoState.ESPERANDO_ENTREGAR_HUEVO:
                        interactPrompt.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            playerStats.SetHasMosquitoEgg(false);
                            ChangeState(GusanoState.EXCAVANDO_TUNEL_2);
                            interactPrompt.SetActive(false);
                            dialogoController.GetDialogoObject().SetActive(false);
                        }
                    break;
            }
        }
        else
        {
            interactPrompt.SetActive(false);
        }

    }

    void moverTunel1()
    {
        tunel_1.IniciarMovimiento(-1);
    }

    void moverTunel2()
    {
        tunel_2.IniciarMovimiento(1);
    }
}
