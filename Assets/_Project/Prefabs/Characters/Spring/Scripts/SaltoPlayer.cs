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
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        }
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
}
