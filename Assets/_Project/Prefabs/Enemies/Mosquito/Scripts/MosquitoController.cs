using UnityEngine;

public class MosquitoController : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float _patrolSpeed;
    [SerializeField] private float _chargeSpeed;
    [Tooltip("Tag de los objetos que marcan el fin de la patrulla (Puntos A/B)")]
    [SerializeField] private Animator animator;

    // --- Propiedades Públicas ---
    public float patrolSpeed => _patrolSpeed;
    public float chargeSpeed => _chargeSpeed;
    public int direccion { get; private set; } = 1;

    // --- Estado Interno ---
    private IMovimientoEstrategia currentStrategy;
    private readonly EstrategiaPatrulla stratPatrulla = new EstrategiaPatrulla();
    private readonly EstrategiaEmbestida stratEmbestida = new EstrategiaEmbestida();

    private void Start()
    {
        SetStrategy(stratPatrulla);
    }

    private void Update()
    {
        // Usamos Update() porque ahora movemos el Transform, no físicas.
        if (GameManager.IsInputLocked()) return;
        currentStrategy?.Move(this);
    }

    // --- Detección de Puntos de Patrulla ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si tocamos un "PatrolPoint"...
        if (other.CompareTag("PatrolPoint"))
        {
            // ...invertimos la dirección.
            direccion *= -1;
            Flip();

            // Si estaba embistiendo, vuelve a patrullar.
            if (currentStrategy == stratEmbestida)
            {
                SetStrategy(stratPatrulla);
            }
        }
    }

    // --- Método Público para el Sensor ---
    public void Embestir(Transform player)
    {
        if (currentStrategy == stratEmbestida) return;

        // Calcular dirección hacia el jugador
        float dirToPlayer = player.position.x - transform.position.x;
        int nuevaDireccion = (dirToPlayer > 0) ? 1 : -1;

        if (nuevaDireccion != direccion)
        {
            direccion = nuevaDireccion;
            Flip();
        }

        SetStrategy(stratEmbestida);
        currentStrategy.OnEnter(this);
        animator.SetBool("embistiendo", true);
    }

    private void SetStrategy(IMovimientoEstrategia newStrategy)
    {
        currentStrategy = newStrategy;
    }

    private void Flip()
    {
        // Invertir el sprite visualmente
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * -direccion;
        transform.localScale = scale;
        animator.SetBool("embistiendo", false);
    }
}