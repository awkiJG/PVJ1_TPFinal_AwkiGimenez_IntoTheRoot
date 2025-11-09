using UnityEngine;

// Define el "contrato" para todas las estrategias de movimiento.
// Su única tarea es mover el Transform del mosquito.
public abstract class IMovimientoEstrategia
{
    /// <summary>
    /// Ejecuta la lógica de movimiento usando el Transform.
    /// </summary>
    /// <param name="controller">El mosquito que se está moviendo (para leer sus datos)</param>
    public abstract void Move(MosquitoController controller);
    public abstract void OnEnter(MosquitoController controller);
}