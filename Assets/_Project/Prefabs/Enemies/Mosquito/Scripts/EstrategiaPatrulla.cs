using UnityEngine;

// Implementa la interfaz con la lógica de patrulla lenta.
// Mueve el objeto modificando su 'transform.position'.
public class EstrategiaPatrulla : IMovimientoEstrategia
{
    public override void Move(MosquitoController controller)
    {
        // 1. Obtener los datos del controlador
        float speed = controller.patrolSpeed;
        int direccion = controller.direccion;

        // 2. Calcular el vector de movimiento
        Vector2 movimiento = new Vector2(direccion * speed * Time.fixedDeltaTime, 0f);

        // 3. Aplicar el movimiento al transform
        // (Usamos fixedDeltaTime porque el movimiento se llama desde FixedUpdate)
        controller.transform.position += (Vector3)movimiento;
    }
    
    public override void OnEnter(MosquitoController controller)
    {
        // No se necesita lógica especial al entrar en patrulla
    }
}