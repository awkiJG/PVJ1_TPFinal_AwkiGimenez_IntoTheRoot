using UnityEngine;

public class SaltoPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRadius;

    void Start()
    {
        // Obtener referencia al Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Ejecutar salto si está en el suelo y presiona el botón de salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        }
    }
    
    void FixedUpdate()
    {
        // Verificar si el jugador está tocando el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }
    
    // Método público para que otros scripts puedan consultar si está en el suelo
    public bool IsGrounded()
    {
        return isGrounded;
    }
}
