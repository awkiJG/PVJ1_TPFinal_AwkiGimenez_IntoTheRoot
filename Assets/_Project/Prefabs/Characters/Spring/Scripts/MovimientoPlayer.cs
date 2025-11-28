using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    private float velocidad;
    private float direccion;
    private Rigidbody2D rb;
    private bool inputLocked = false;
    [SerializeField] private AudioClip audioPaso;
    [SerializeField] private float stepInterval = 0.25f; // segundos entre pasos
    private float stepTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        velocidad = 10f;
    }

    void FixedUpdate()
    {
        //Verifica si estan abilitados los inputs
        if (!GameManager.IsInputLocked())
        {
            // Capturar input horizontal (-1 a 1)
            direccion = Input.GetAxis("Horizontal");
        }else if(!inputLocked)
        {
            direccion = 0;
            inputLocked = true;
        }

        // Aplicar movimiento horizontal usando Rigidbody2D
        bool isMoving = Mathf.Abs(direccion) > 0.01f;

        if (isMoving)
        {
            // Reproducir sonido de paso con temporizador para que no suene cada frame
        
                stepTimer -= Time.fixedDeltaTime;
                if (stepTimer <= 0f)
                {
                    AudioManager.Instance.PlaySFX(audioPaso);
                    stepTimer = stepInterval;
                }

            rb.linearVelocity = new Vector2(
                direccion * velocidad,
                rb.linearVelocity.y);
        }
        else
        {
            // Resetear temporizador para que el primer paso suene inmediatamente al volver a moverse
            stepTimer = 0f;
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
    public float Direccion { get => direccion; set => direccion = value; }
    

    public float Velocidad { get => velocidad; set => velocidad = value; }
}
