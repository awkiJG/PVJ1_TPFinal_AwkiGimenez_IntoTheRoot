using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private float direccion;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direccion = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(direccion * velocidad, rb.linearVelocity.y);

        if (direccion != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direccion), 1, 1);
        }
    }
}
