using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;
public class FinalLevelFSM : MonoBehaviour
{
    [SerializeField] private GameObject rootSuperior;
    [Header("Camaras")]
    [SerializeField] private CinemachineCamera vCamPlayer;
    [SerializeField] private CinemachineCamera vCamStrokeDialogo;
    [SerializeField] private CinemachineCamera vCamStrokePurificacion;
    
    [SerializeField] private CinemachineCamera vCamCorazon;
    [SerializeField] private CinemachineCamera vCamArena;
    [Header("Corazon")]
    [SerializeField] private CorazonController corazon;
    [SerializeField] private GameObject sueloCorazon;
    [Header("Strong")]
    [SerializeField] private StrokeController stroke;
    [SerializeField] private Animator animatorStrong;
    [Header("Boss Controller")]
    [SerializeField] private BossController boss;
    [Header("Player")]
    [SerializeField] private MovimientoPlayer movPlayer;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator animatorPlayer;
    [Header("Fade Final")]
    [SerializeField] private UIFader uiFader;
    public enum BossState 
    { 
        ESPERA_INICIAL, 
        INTRO, 
        FASE_1, 
        APERTURA_CORAZON, 
        ESPERANDO_INTERACCION, 
        FASE_2, 
        FINAL_1, 
        FINAL_2
    }
    private BossState currentState = BossState.ESPERA_INICIAL;


    public void ChangeState(BossState newState)
    {
        currentState = newState;
        // Lógica para manejar la transición entre estados
        switch (currentState)
        {
            case BossState.INTRO:
                StartCoroutine(InicialCinematicCoroutine());
                break;
            case BossState.FASE_1:
                boss.StartPhase1();
                break;
            case BossState.APERTURA_CORAZON:
                StartCoroutine(CorazonCinematicCoroutine());
                break;
            case BossState.ESPERANDO_INTERACCION:
                // Esperar interacción del jugador
                break;
            case BossState.FASE_2:
                StartCoroutine(Phase2CinematicCoroutine());
                break;
            case BossState.FINAL_1:
                StartCoroutine(Final1CinematicCoroutine());
                // Final bueno
                break;
            case BossState.FINAL_2:
                StartCoroutine(Final2CinematicCoroutine());
                // Final malo
                break;
        }
        Debug.Log("Current State: " + currentState);
    }

    void Start()
    {
        // 1. Espera inicial
        Invoke("StartCinematic", 1f); 
    }
    void StartCinematic()
    {
        ChangeState(BossState.APERTURA_CORAZON);
        rootSuperior.SetActive(true);
    }

    private IEnumerator InicialCinematicCoroutine()
    {
        // Lógica para la cinemática inicial
        // Bloquear input del jugador
        GameManager.RequestInputLock(true);
        yield return new WaitForSeconds(1f);
        // Cambiar a cámara de Stroke
        vCamStrokeDialogo.Priority = 20;
        yield return new WaitForSeconds(2f);
        // Mueve a Stroke hacia adelante
        stroke.SetDireccion(-1);
        yield return new WaitForSeconds(2.5f);
        stroke.SetDireccion(0);
        // Mostrar mensaje de Stroke
        yield return StartCoroutine(stroke.MostrarMensajeCoroutine());
        // Mueve a Stroke hacia atras
        stroke.SetDireccion(1);
        yield return new WaitForSeconds(2.5f);   
        // Cambia a la camara de la arena complenta 
        vCamStrokeDialogo.Priority = 1;
        vCamArena.Priority = 20;
        yield return new WaitForSeconds(2f);
        // Volver el control al jugador
        GameManager.RequestInputLock(false);

        ChangeState(BossState.FASE_1);
    }

    private IEnumerator CorazonCinematicCoroutine()
    {
        // Lógica para la cinemática del corazón
        // Bloquear input del jugador
        GameManager.RequestInputLock(true);
        yield return new WaitForSeconds(1f);
        // Cambiar a cámara del corazón
        vCamArena.Priority = 1;
        vCamCorazon.Priority = 20;
        yield return new WaitForSeconds(2f);
        // Animación de apertura del corazón
        corazon.Abriendo(true);
        sueloCorazon.SetActive(false);
        yield return new WaitForSeconds(3f);
        vCamCorazon.Priority = 1;
        vCamPlayer.Priority = 20;
        yield return new WaitForSeconds(1f);    
        // Volver el control al jugador
        GameManager.RequestInputLock(false);
        ChangeState(BossState.ESPERANDO_INTERACCION);
    }

    private IEnumerator Phase2CinematicCoroutine()
    {
        // Lógica para la cinemática de transición a Fase 2
        yield return new WaitForSeconds(1f);
        vCamPlayer.Priority=1;
        vCamArena.Priority=20;
        yield return new WaitForSeconds(2f);
        boss.StartPhase2Loop();
    }
    
    private IEnumerator Final1CinematicCoroutine()
    {
        // Lógica para la cinemática del final bueno
        // Bloquear input del jugador
        boss.StopBoss();
        GameManager.RequestInputLock(true);
        yield return new WaitForSeconds(1f);
        // Cambiar a cámara del corazón
        vCamArena.Priority=1;
        vCamCorazon.Priority=20;
        yield return new WaitForSeconds(2f);
        // Se cierra el corazón
        corazon.Cerrando(true);
        yield return new WaitForSeconds(2.5f);
        //Cambia a cámara de purificación
        vCamCorazon.Priority=1;
        vCamStrokePurificacion.Priority=20;
        yield return new WaitForSeconds(3f);
        // Animación de purificación
        animatorStrong.SetTrigger("Purificar");
        yield return new WaitForSeconds(5f);
        // Fade out y carga de escena
        uiFader.FadeToBlack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Final_1_Purificado");
    }

    private IEnumerator Final2CinematicCoroutine()
    {
        // Lógica para la cinemática del final malo
        // Bloquear input del jugador
        boss.StopBoss();
        GameManager.RequestInputLock(true);
        yield return new WaitForSeconds(1f);
        // Cambiar a cámara del corazón
        vCamArena.Priority=1;
        vCamCorazon.Priority=20;
        yield return new WaitForSeconds(2f);
        // Mover al jugador hacia el corazon
        while (playerTransform.position.x < corazon.transform.position.x - 0.1f)
        {
            movPlayer.Velocidad = 2f;
            movPlayer.Direccion = 1;
            yield return null;
        }
        movPlayer.Direccion = 0;
        movPlayer.Velocidad = 10f;
        yield return new WaitForSeconds(1f);
        // Animación de muerte del jugador
        animatorPlayer.SetTrigger("sacrificio");
        yield return new WaitForSeconds(4f);
        // Se cierra el corazón
        corazon.Cerrando(true);
        yield return new WaitForSeconds(2.5f);
        //Cambia a cámara de purificación
        vCamCorazon.Priority=1;
        vCamStrokePurificacion.Priority=20;
        yield return new WaitForSeconds(3f);
        // Animación de purificación
        animatorStrong.SetTrigger("Purificar");
        yield return new WaitForSeconds(5f);
        // Fade out y carga de escena
        uiFader.FadeToBlack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Final_2_Sacrificio");
    }
    public BossState GetCurrentState()
    {
        return currentState;
    }

}