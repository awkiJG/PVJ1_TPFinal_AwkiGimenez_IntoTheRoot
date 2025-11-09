using UnityEngine;

public class EstrategiaEmbestida : IMovimientoEstrategia
{
    // Estado interno de la estrategia
    private float currentSpeed;
    private float maxSpeed;

    // Qué tan rápido acelera 
    private float accelerationRate = 20f;

    /// <summary>
    /// Se llama cuando el mosquito decide embestir.
    /// Reinicia la velocidad para que empiece desde la velocidad de patrulla.
    /// </summary>
    public override void OnEnter(MosquitoController controller)
    {
        currentSpeed = controller.patrolSpeed; // ¡Empieza lento!
        maxSpeed = controller.chargeSpeed;     // Define el tope
    }

    /// <summary>
    /// Se llama cada frame (FixedUpdate).
    /// Acelera y luego se mueve.
    /// </summary>
    public override void Move(MosquitoController controller)
    {
        // 1. ACELERAR
        // Aumenta 'currentSpeed' hacia 'maxSpeed', 
        // sin pasarse, usando 'accelerationRate'.
        currentSpeed = Mathf.MoveTowards(
            currentSpeed, 
            maxSpeed, 
            accelerationRate * Time.fixedDeltaTime
        );

        // 2. OBTENER DATOS
        int direccion = controller.direccion;
        
        // 3. CALCULAR MOVIMIENTO
        // Usa la 'currentSpeed' (que está cambiando)
        Vector2 movimiento = new Vector2(direccion * currentSpeed * Time.fixedDeltaTime, 0f);

        // 4. APLICAR MOVIMIENTO
        controller.transform.position += (Vector3)movimiento;
    }
}