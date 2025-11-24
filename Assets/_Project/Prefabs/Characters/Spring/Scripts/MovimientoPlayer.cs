using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    private float velocidad;
    private float direccion;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        velocidad = 10f;
    }

    void FixedUpdate()
    {
        //Verifica si estan abilitados los inputs
        if (GameManager.IsInputLocked())
    {
        return; // Salir inmediatamente
    }
        // Capturar input horizontal (-1 a 1)
        direccion = Input.GetAxis("Horizontal");

        // Aplicar movimiento horizontal usando Rigidbody2D
        if (direccion != 0)
        {
            rb.linearVelocity = new Vector2(
                direccion * velocidad,
                rb.linearVelocity.y);
        } else
        {
            rb.linearVelocity = new Vector2(
                0,
                rb.linearVelocity.y);
        }

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

    public float Velocidad { get => velocidad; set => velocidad = value; }
}
