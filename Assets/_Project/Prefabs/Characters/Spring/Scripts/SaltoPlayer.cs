using UnityEngine;

public class SaltoPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRadius;
    [SerializeField] private AudioClip audioSalto;
    void Start()
    {
        // Obtener referencia al Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Verifica si estan abilitados los inputs
        if (GameManager.IsInputLocked())
    {
        return; // Salir inmediatamente
    }
        // Ejecutar salto si está en el suelo y presiona el botón de salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
            AudioManager.Instance.PlaySFX(audioSalto);
        }
    }
    
    void FixedUpdate()
    {
        // Verificar si el jugador está tocando el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        
        // Limitar la velocidad de caída máxima
        if (rb.linearVelocity.y < -30f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -30f);
        }
    }
    
    // Método público para que otros scripts puedan consultar si está en el suelo
    public bool IsGrounded()
    {
        return isGrounded;
    }
}
