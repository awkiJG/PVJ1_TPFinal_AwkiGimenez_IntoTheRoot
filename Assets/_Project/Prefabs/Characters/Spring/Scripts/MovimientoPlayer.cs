using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private float direccion;
    private Rigidbody2D rb;
    
    void Start()
    {
        // Obtener referencia al Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Capturar input horizontal (-1 a 1)
        direccion = Input.GetAxis("Horizontal");

        // Aplicar movimiento horizontal manteniendo velocidad vertical
        rb.linearVelocity = new Vector2(direccion * velocidad, rb.linearVelocity.y);

        // Voltear el sprite según la dirección del movimiento
        if (direccion != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direccion), 1, 1);
        }
    }
    // Método público para que otros scripts puedan consultar la dirección
    public float GetDireccion()
    {
        return direccion;
    }
}
