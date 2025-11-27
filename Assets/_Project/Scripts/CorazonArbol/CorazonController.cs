using UnityEngine;
using System.Collections;

public class CorazonController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool abriendo = false;
    private bool cerrando = false;

    [Header("Referencias")]
    [SerializeField] private FinalLevelFSM fsm;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject interactPrompt; // El aviso visual "E"

    [Header("Configuración")]
    [SerializeField] private float cooldownTime = 5.0f;
    // --- Estado Interno ---
    private bool isPlayerInRange = false;
    private bool canInteract = true; // Controla el cooldown
    private int sapDepositedCount = 0;
    [SerializeField] private Transform sapTransform;
    [SerializeField] private float sapRiseDistance = 3f;
    [SerializeField] private float sapReturnDuration = 1.0f;

    void Start()
    {
        InvokeRepeating("EjecutarAnimacion", 1f, 2f);
    }

    private void Update()
    {
        // 1. Validar condiciones básicas
        if (!isPlayerInRange || !canInteract)
        {
            if (interactPrompt != null && interactPrompt.activeSelf) interactPrompt.SetActive(false);
            return;
        }
        // 2. Validar ESTADO de la FSM
        // Solo permitimos interactuar en "ESPERANDO_INTERACCION" o "FASE_2"
        FinalLevelFSM.BossState currentState = fsm.GetCurrentState(); // (Necesitamos este getter en la FSM)
        
        bool isStateValid = (currentState == FinalLevelFSM.BossState.ESPERANDO_INTERACCION || 
                             currentState == FinalLevelFSM.BossState.FASE_2);

        if (!isStateValid)
        {
            if (interactPrompt != null && interactPrompt.activeSelf) interactPrompt.SetActive(false);
            return;
        }

        // 3. Mostrar el aviso si todo es válido
        if (!interactPrompt.activeSelf) interactPrompt.SetActive(true);

        // 4. Detectar Input
        if (Input.GetKeyDown(KeyCode.E))
        {
            AttemptDeposit();
        }
    }

    // --- Lógica de Depósito ---
    private void AttemptDeposit()
    {
        // Iniciar cooldown inmediatamente para evitar spam
        StartCoroutine(CooldownRoutine());

        // Leer cuánta savia tiene el jugador en este momento
        int currentSap = playerStats.GetCurrentSap();

        // Chequear condición de VICTORIA INMEDIATA (el jugador depositó la cantidad necesaria)
        if (sapDepositedCount >= 10)
        {
            fsm.ChangeState(FinalLevelFSM.BossState.FINAL_1); // Final 1
            return;
        }

        // El jugador NO TIENE Savia (Sacrificio)
        if (currentSap <= 0 && sapDepositedCount >= 6)
        {
            // Si intenta interactuar sin savia y deeposito al menos 6, se sacrifica
            fsm.ChangeState(FinalLevelFSM.BossState.FINAL_2); // Final 2
            return;
        }

        // El jugador TIENE Savia -> Consumir y sumar
        playerStats.ReleaseSap(1);
        sapDepositedCount++;

        // Ejecutar la animación de la savia (sube instant y vuelve lentamente)
        StartCoroutine(SapAnimationCoroutine());

       

        // Si no se alcanzó la victoria, si el estado actual era ESPERANDO_INTERACCION,
        // entonces la primera depositación activa la Fase 2.
        if (fsm.GetCurrentState() == FinalLevelFSM.BossState.ESPERANDO_INTERACCION)
        {
            fsm.ChangeState(FinalLevelFSM.BossState.FASE_2);
        }
    }

    // --- Cooldown ---
    private IEnumerator CooldownRoutine()
    {
        canInteract = false;
        interactPrompt.SetActive(false); // Ocultar aviso durante cooldown
        
        // (Opcional: Aquí podrías poner una animación de UI tipo "reloj recargando")
        
        yield return new WaitForSeconds(cooldownTime);
        
        canInteract = true;
    }

    private IEnumerator SapAnimationCoroutine()
    {
        if (sapTransform == null)
            yield break;

        // Usamos localPosition para mover respecto al padre del transform
        Vector3 originalPos = sapTransform.localPosition;
        Vector3 raisedPos = originalPos + Vector3.up * sapRiseDistance;

        // Mover instantáneamente hacia arriba
        sapTransform.localPosition = raisedPos;

        // Volver lentamente a la posición original en sapReturnDuration segundos
        float elapsed = 0f;
        while (elapsed < sapReturnDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / sapReturnDuration);
            sapTransform.localPosition = Vector3.Lerp(raisedPos, originalPos, t);
            yield return null;
        }

        sapTransform.localPosition = originalPos;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void EjecutarAnimacion()
    {
        if (abriendo)
        {
            abriendo = false;
            animator.SetBool("Abierto", true);
            return;
        }
        else if (cerrando)
        {
            cerrando = false;
            animator.SetBool("Abierto", false);
            return; 
        }
            animator.SetTrigger("Latir"); 
    }

    public void Abriendo(bool value)
    {
        abriendo = value;
    }
    public void Cerrando(bool value)
    {
        cerrando = value;
    }
}
