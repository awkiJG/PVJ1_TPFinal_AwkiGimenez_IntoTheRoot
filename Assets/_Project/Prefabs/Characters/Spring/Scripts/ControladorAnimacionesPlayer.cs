using UnityEngine;

public class ControladorAnimacionesPlayer : MonoBehaviour
{
    // Referencias a componentes
    private Animator animator;
    private MovimientoPlayer movPlayer;
    private SaltoPlayer saltoPlayer;
    private AgacharsePlayer agacharsePlayer;
    private Rigidbody2D rb;
    // Estados de animación
    private bool caminando;
    private bool saltando;
    private bool agachado;
    private bool cayendo;

    void Start()
    {
        // Obtener referencias a todos los componentes necesarios
        animator = GetComponent<Animator>();
        movPlayer = GetComponent<MovimientoPlayer>();
        saltoPlayer = GetComponent<SaltoPlayer>();
        agacharsePlayer = GetComponent<AgacharsePlayer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Actualizar estados basados en los componentes de movimiento
        caminando = movPlayer.Direccion != 0;
        saltando = !saltoPlayer.IsGrounded() && rb.linearVelocity.y > 0;
        cayendo = !saltoPlayer.IsGrounded() && rb.linearVelocity.y < 0;
        agachado = agacharsePlayer.IsCrouching();
        // Enviar los estados al Animator
        UpdateAnimatorParameters();
    }
    // Actualiza todos los parámetros del Animator
    private void UpdateAnimatorParameters()
    {
        animator.SetBool("caminando", caminando);
        animator.SetBool("saltando", saltando);
        animator.SetBool("cayendo", cayendo);
        animator.SetBool("agachado", agachado);
    }
}
