
using UnityEngine;
using System.Collections;

public class RootAttack : MonoBehaviour
{
    private Vector3 startPosition;

    /// <summary>
    /// Guarda la posición "de hogar" al iniciar.
    /// </summary>
    private void Start()
    {
        startPosition = transform.position;
    }

    // --- 1. El Método de Activación (Llamado por el Controller) ---

    /// <summary>
    /// Inicia la secuencia de ataque.
    /// El Controller llama a esto.
    /// </summary>
    /// <param name="distance">Distancia (en unidades) que se extenderá la raíz.</param>
    /// <param name="speed">Velocidad de movimiento (unidades por segundo).</param>
    /// <param name="waitTime">Tiempo (en segundos) que esperará extendida.</param>
    public void ActivateAttack(float distance, float speed, float waitTime)
    {
        StartCoroutine(AttackSequence(distance, speed, waitTime));
    }

    // --- 2. La Secuencia de Ataque (La Corrutina) ---

    /// <summary>
    /// Corrutina que maneja la secuencia completa: Extender, Esperar, Retraer.
    /// </summary>
    private IEnumerator AttackSequence(float distance, float speed, float waitTime)
    {

        float tiempoInicio = Time.time;

        while (Time.time < tiempoInicio + 1f)
        {
            // Calcular un offset aleatorio en X e Y
            // (Usamos Vector3 para que sea compatible con startPosition)
            Vector3 randomOffset = (Vector3)Random.insideUnitCircle * 0.3f;
            
            // Aplicar el offset a la posición inicial (para que no se "vaya" del sitio)
            transform.position = startPosition + randomOffset;
            
            yield return null; // Esperar al próximo frame
        }

        // Asegurarse de que termine en la posición inicial exacta antes de moverse
        transform.position = startPosition;

        // 2. Calcular posiciones
        // (Usamos 'transform.right' asumiendo que la raíz apunta hacia la derecha.
        // Cambia esto a transform.up, .forward, etc., según la rotación de tu prefab)
        Vector3 targetPosition = startPosition + (transform.right * distance); 

        // --- 3. FASE DE EXTENSIÓN ---
        // Moverse desde 'startPosition' hasta 'targetPosition'
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime // Usar Time.deltaTime para un movimiento suave
            );
            yield return null; // Esperar al próximo frame
        }
        // Asegurarse de que quede exacto al final
        transform.position = targetPosition; 

        // --- 4. FASE DE ESPERA ---
        yield return new WaitForSeconds(waitTime);

        // --- 5. FASE DE RETRACCIÓN ---
        // Moverse desde 'targetPosition' de vuelta a 'startPosition'
        while (Vector3.Distance(transform.position, startPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                startPosition,
                speed * Time.deltaTime // Usar la misma velocidad para volver
            );
            yield return null; // Esperar al próximo frame
        }
        // Asegurarse de que quede exacto al final
        transform.position = startPosition;
    }
}