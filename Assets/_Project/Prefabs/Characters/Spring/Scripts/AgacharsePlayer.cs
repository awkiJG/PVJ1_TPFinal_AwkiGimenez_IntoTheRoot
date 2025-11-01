using UnityEngine;

public class AgacharsePlayer : MonoBehaviour
{
    // Variables para el collider cuando está agachado
    private float crouchHeight;
    private Vector2 crounchOffset;
    
    // Variables para el collider en posición normal
    private float originalHeight;
    private Vector2 originalOffset;
    
    // Control de estados y referencias
    private LayerMask obstacleLayer;
    private bool isCrouching = false;
    private bool isGrounded;
    private bool inputAgacharse;
    private CapsuleCollider2D capsuleCollider;

    void Start()
    {
        // Obtener referencia al collider
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        // Configurar valores de posición normal
        originalHeight = 2.3f;
        originalOffset = new Vector2(capsuleCollider.offset.x, -0.18f);

        // Configurar valores para posición agachado
        crouchHeight = 1.5f;
        crounchOffset = new Vector2(capsuleCollider.offset.x, -0.58f);

        // Configurar layer de obstáculos
        obstacleLayer = LayerMask.GetMask("Obstacles");

    }

    void Update()
    {
        // Capturar input del jugador y verificar si está en el suelo
        inputAgacharse = Input.GetKey(KeyCode.LeftControl);
        isGrounded = GetComponent<SaltoPlayer>().IsGrounded();

        // Si presiona agacharse y está en el suelo, agacharse
        if (inputAgacharse && isGrounded)
        {
            Agacharse();
        }
        // Si deja de presionar y está agachado, intentar levantarse
        else if (!inputAgacharse && isCrouching)
        {
            Levantarse();
        }
    }

    // Cambia el collider para posición agachado
    private void Agacharse()
    {
        isCrouching = true;
        capsuleCollider.size = new Vector2(capsuleCollider.size.x, crouchHeight);
        capsuleCollider.offset = crounchOffset;
    }

    // Intenta volver el collider a posición normal
    private void Levantarse()
    {
        if (CanStandUp())
        {
            isCrouching = false;
            capsuleCollider.size = new Vector2(capsuleCollider.size.x, originalHeight);
            capsuleCollider.offset = originalOffset;
        }
    }

    // Verifica si hay espacio para levantarse
    private bool CanStandUp()
    {
        Vector2 standingCenter = (Vector2)transform.position + originalOffset;
        Vector2 standingSize = new Vector2(capsuleCollider.size.x, originalHeight);

        Collider2D obstacleFound = Physics2D.OverlapBox(
            standingCenter,
            standingSize,
            0f,
            obstacleLayer
            );
        return (obstacleFound == null);
    }
    // Método público para que otros scripts puedan consultar si está agachado
    public bool IsCrouching()
    {
        return isCrouching;
    }
}
