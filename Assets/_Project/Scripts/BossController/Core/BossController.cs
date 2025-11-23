using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BossController : MonoBehaviour
{
    // --- 1. REFERENCIAS (Los Soldados) ---
    [Header("Asignaciones de Escena")]
    [Tooltip("Arrastra aquí todas las raíces en el orden que prefieras (0, 1, 2...)")]
    [SerializeField] private List<RootAttack> roots;

    [Tooltip("Arrastra el Spawner que está en el techo")]
    [SerializeField] private BombSpawner bombSpawner;

    [Header("Prefabs")]
    [Tooltip("El prefab de la bomba explosiva")]
    [SerializeField] private GameObject bombPrefab;

    // --- 2. ALMACÉN DE PATRONES (Las Partituras) ---

    // FASE 1: Una única lista larga (o bloques concatenados) que se ejecuta UNA vez.
    private List<List<ICommand>> phase1_FullSequence;

    // FASE 2: Una colección de listas pequeñas que se ejecutan en BUCLE infinito.
    // (Lista 1 -> Lista 2 -> Lista 3 -> Volver a Lista 1...)
    private List<List<ICommand>> phase2_LoopCollection;

    // --- 3. EVENTOS ---
    // Para avisar a la Máquina de Estados (FinalLevelFSM) cuando termine la Fase 1
    public event Action OnPhase1Finished;

    private void Start()
    {
        BuildAttackPatterns();
        Invoke("StartPhase2Loop", 1f);
    }

    private void BuildAttackPatterns()
    {
        // ---------------------------------------------------------
        // CONFIGURACIÓN FASE 1 (Secuencia Lineal)
        // ---------------------------------------------------------
        phase1_FullSequence = new List<List<ICommand>>();

        List<ICommand> phase1_PartA = new List<ICommand>();

        phase1_PartA.Add(new RootAttackCommand(roots[19], -4.2f, 5f, 105f, 4f));

        phase1_PartA.Add(new RootAttackCommand(roots[4], -25f, 15f, 6f, 3f));
        phase1_PartA.Add(new RootAttackCommand(roots[9], -17f, 15f, 7f, 2f));
        phase1_PartA.Add(new RootAttackCommand(roots[12], -10.15f, 15f, 7f, 0f));
        phase1_PartA.Add(new RootAttackCommand(roots[13], -7.8f, 15f, 7f, 0f));
        phase1_PartA.Add(new RootAttackCommand(roots[8], -17.5f, 15f, 7f, 2f));
        phase1_PartA.Add(new RootAttackCommand(roots[7], -18f, 15f, 8.5f, 2f));
        phase1_PartA.Add(new RootAttackCommand(roots[6], -22f, 15f, 5f, 2f));
        phase1_PartA.Add(new RootAttackCommand(roots[5], -24f, 15f, 2f, 5f));
        phase1_PartA.Add(new RootAttackCommand(roots[0], -16f, 15f, 2f, 0.5f));
        phase1_PartA.Add(new RootAttackCommand(roots[1], -16f, 15f, 2f, 0.5f));
        phase1_PartA.Add(new RootAttackCommand(roots[2], -16f, 15f, 2f, 0.5f));
        phase1_PartA.Add(new RootAttackCommand(roots[3], -16f, 15f, 2f, 5f));

        List<ICommand> phase1_PartB = new List<ICommand>();

        phase1_PartB.Add(new ExplosiveShotCommand(bombSpawner, 6, 10f, 0.3f, 2f, bombPrefab));
        phase1_PartB.Add(new ExplosiveShotCommand(bombSpawner, 9, 10f, 0.4f, 2f, bombPrefab));
        phase1_PartB.Add(new ExplosiveShotCommand(bombSpawner, 11, 8f, 0.7f, 4f, bombPrefab));

        List<ICommand> phase1_PartC = new List<ICommand>();

        phase1_PartC.Add(new RootAttackCommand(roots[10], -27f, 20f, 23f, 2f));
        phase1_PartC.Add(new RootAttackCommand(roots[9], -14f, 20f, 0f, 1f));
        phase1_PartC.Add(new RootAttackCommand(roots[6], -19f, 20f, 0f, 1f));
        phase1_PartC.Add(new RootAttackCommand(roots[8], -14.5f, 20f, 0f, 1f));
        phase1_PartC.Add(new RootAttackCommand(roots[4], -22f, 20f, 0f, 2f));
        phase1_PartC.Add(new RootAttackCommand(roots[13], -27.3f, 20f, 16f, 3f));
        phase1_PartC.Add(new RootAttackCommand(roots[9], -12.4f, 20f, 0f, 0f));
        phase1_PartC.Add(new RootAttackCommand(roots[4], -20.4f, 20f, 0f, 1f));
        phase1_PartC.Add(new RootAttackCommand(roots[6], -17.4f, 20f, 0f, 0f));
        phase1_PartC.Add(new RootAttackCommand(roots[7], -13.4f, 20f, 0f, 2f));
        phase1_PartC.Add(new ExplosiveShotCommand(bombSpawner, 12, 10f, 0.5f, 2f, bombPrefab));
        phase1_PartC.Add(new RootAttackCommand(roots[15], -27.8f, 25f, 0f, 2f));
        phase1_PartC.Add(new ExplosiveShotCommand(bombSpawner, 8, 15f, 0.3f, 1f, bombPrefab));
        phase1_PartC.Add(new ExplosiveShotCommand(bombSpawner, 12, 12f, 0.4f, 1f, bombPrefab));
        phase1_PartC.Add(new ExplosiveShotCommand(bombSpawner, 7, 17f, 0.3f, 2.5f, bombPrefab));
        phase1_PartC.Add(new ExplosiveShotCommand(bombSpawner, 12, 10f, 0.5f, 7f, bombPrefab));

        List<ICommand> phase1_PartD = new List<ICommand>();

        phase1_PartD.Add(new RootAttackCommand(roots[13], -27.3f, 25f, 2.5f, 1f));
        phase1_PartD.Add(new RootAttackCommand(roots[12], -26.7f, 15f, 0f, 4f));
        phase1_PartD.Add(new RootAttackCommand(roots[10], -27f, 20f, 30f, 1f));
        phase1_PartD.Add(new RootAttackCommand(roots[13], -27.3f, 20f, 15f, 1f));
        phase1_PartD.Add(new ExplosiveShotCommand(bombSpawner, 10, 10f, 0.6f, 3f, bombPrefab));
        phase1_PartD.Add(new RootAttackCommand(roots[15], -27.8f, 20f, 10f, 0f));
        phase1_PartD.Add(new RootAttackCommand(roots[14], -26.7f, 20f, 10f, 3f));

        phase1_PartD.Add(new RootAttackCommand(roots[17], -11f, 15f, 0f, 0f));
        phase1_PartD.Add(new RootAttackCommand(roots[18], -11f, 15f, 0f, 3f));

        phase1_PartD.Add(new RootAttackCommand(roots[18], -6f, 15f, 1f, 0.5f));
        phase1_PartD.Add(new RootAttackCommand(roots[17], -16f, 20f, 0f, 3f));

        phase1_PartD.Add(new RootAttackCommand(roots[17], -6f, 15f, 1f, 0.5f));
        phase1_PartD.Add(new RootAttackCommand(roots[18], -16f, 20f, 0f, 3f));

        phase1_PartD.Add(new ExplosiveShotCommand(bombSpawner, 3, 12f, 0.4f, 1f, bombPrefab));
        phase1_PartD.Add(new ExplosiveShotCommand(bombSpawner, 4, 12f, 0.4f, 1f, bombPrefab));
        phase1_PartD.Add(new ExplosiveShotCommand(bombSpawner, 5, 12f, 0.4f, 1f, bombPrefab));
        phase1_PartD.Add(new ExplosiveShotCommand(bombSpawner, 6, 12f, 0.4f, 1f, bombPrefab));
        phase1_PartD.Add(new ExplosiveShotCommand(bombSpawner, 7, 12f, 0.4f, 1f, bombPrefab));
        phase1_PartD.Add(new ExplosiveShotCommand(bombSpawner, 8, 12f, 0.4f, 1f, bombPrefab));
        phase1_PartD.Add(new ExplosiveShotCommand(bombSpawner, 9, 12f, 0.4f, 1f, bombPrefab));
        phase1_PartD.Add(new ExplosiveShotCommand(bombSpawner, 10, 12f, 0.4f, 1f, bombPrefab));
        phase1_PartD.Add(new ExplosiveShotCommand(bombSpawner, 11, 12f, 0.4f, 1f, bombPrefab));
        phase1_PartD.Add(new ExplosiveShotCommand(bombSpawner, 12, 12f, 0.4f, 1f, bombPrefab));
        phase1_PartD.Add(new ExplosiveShotCommand(bombSpawner, 13, 12f, 0.4f, 1f, bombPrefab));

        phase1_FullSequence.Add(phase1_PartA);
        phase1_FullSequence.Add(phase1_PartB);
        phase1_FullSequence.Add(phase1_PartC);
        phase1_FullSequence.Add(phase1_PartD);
        // ---------------------------------------------------------
        // CONFIGURACIÓN FASE 2 (Bucle Infinito de Listas)
        // ---------------------------------------------------------
        phase2_LoopCollection = new List<List<ICommand>>();

        List<ICommand> loopA = new List<ICommand>();

        loopA.Add(new RootAttackCommand(roots[10], -27f, 30f, 8f, 1f));
        loopA.Add(new RootAttackCommand(roots[13], -27.3f, 30f, 7f, 1f));
        loopA.Add(new RootAttackCommand(roots[17], -17f, 30f, 6.55f, 0f));
        loopA.Add(new RootAttackCommand(roots[18], -17f, 30f, 6.55f, 1f));
        loopA.Add(new RootAttackCommand(roots[15], -27.8f, 30f, 5f, 1f));
        loopA.Add(new RootAttackCommand(roots[14], -26.7f, 30f, 4f, 1f));

        loopA.Add(new RootAttackCommand(roots[9], -7, 30f, 0f, 0.2f));
        loopA.Add(new RootAttackCommand(roots[8], -7, 30f, 0f, 0.2f));
        loopA.Add(new RootAttackCommand(roots[7], -8, 30f, 0f, 0.2f));
        loopA.Add(new RootAttackCommand(roots[6], -12, 30f, 0f, 0.2f));
        loopA.Add(new RootAttackCommand(roots[5], -14, 30f, 0f, 0.2f));
        loopA.Add(new RootAttackCommand(roots[4], -15, 30f, 0f, 0f));
        loopA.Add(new RootAttackCommand(roots[11], -30, 40f, 0f, 1f));

        loopA.Add(new RootAttackCommand(roots[0], -9, 25f, 0f, 0.2f));
        loopA.Add(new RootAttackCommand(roots[1], -9, 25f, 0f, 0.2f));
        loopA.Add(new RootAttackCommand(roots[2], -9, 25f, 0f, 0.2f));
        loopA.Add(new RootAttackCommand(roots[3], -9, 25f, 0f, 2f));

        List<ICommand> loopB = new List<ICommand>();

        loopB.Add(new RootAttackCommand(roots[10], -27f, 30f, 22f, 1f));
        loopB.Add(new RootAttackCommand(roots[13], -30f, 30f, 21f, 0.5f));
        loopB.Add(new RootAttackCommand(roots[9], -12f, 30f, 1f, 0.2f));
        loopB.Add(new RootAttackCommand(roots[18], -8.32f, 50f, 0f, 0f));
        loopB.Add(new RootAttackCommand(roots[8], -12f, 30f, 1f, 0.2f));
        loopB.Add(new RootAttackCommand(roots[7], -12.5f, 30f, 1f, 0.2f));
        loopB.Add(new RootAttackCommand(roots[6], -16.7f, 30f, 1f, 0.2f));
        loopB.Add(new RootAttackCommand(roots[5], -19.6f, 30f, 1f, 1.5f));
        loopB.Add(new RootAttackCommand(roots[16], -35f, 50f, 17.8f, 1.2f));
        loopB.Add(new RootAttackCommand(roots[18], -14f, 50f, 0f, 0f));

        loopB.Add(new RootAttackCommand(roots[0], -13f, 50f, 0f, 1f));
        loopB.Add(new RootAttackCommand(roots[3], -13f, 50f, 0f, 1f));
        loopB.Add(new RootAttackCommand(roots[2], -13f, 50f, 0f, 1f));
        loopB.Add(new RootAttackCommand(roots[0], -13f, 50f, 0f, 1f));

        loopB.Add(new RootAttackCommand(roots[1], -13f, 50f, 0f, 0f));
        loopB.Add(new RootAttackCommand(roots[3], -13f, 50f, 0f, 1f));

        loopB.Add(new RootAttackCommand(roots[0], -13f, 50f, 0f, 0f));
        loopB.Add(new RootAttackCommand(roots[2], -13f, 50f, 0f, 2f));

        loopB.Add(new RootAttackCommand(roots[0], -13f, 50f, 0f, 0f));
        loopB.Add(new RootAttackCommand(roots[1], -13f, 50f, 0f, 0f));
        loopB.Add(new RootAttackCommand(roots[2], -13f, 50f, 0f, 2f));

        loopB.Add(new RootAttackCommand(roots[1], -13f, 50f, 0f, 0f));
        loopB.Add(new RootAttackCommand(roots[2], -13f, 50f, 0f, 0f));
        loopB.Add(new RootAttackCommand(roots[3], -13f, 50f, 0f, 2f));

        loopB.Add(new RootAttackCommand(roots[0], -13f, 50f, 0f, 0f));
        loopB.Add(new RootAttackCommand(roots[1], -13f, 50f, 0f, 0f));
        loopB.Add(new RootAttackCommand(roots[3], -13f, 50f, 0f, 2f));

        loopB.Add(new RootAttackCommand(roots[3], -13f, 50f, 0f, 0.6f));
        loopB.Add(new RootAttackCommand(roots[2], -13f, 50f, 0f, 0.6f));
        loopB.Add(new RootAttackCommand(roots[0], -13f, 50f, 0f, 0.6f));
        loopB.Add(new RootAttackCommand(roots[3], -13f, 50f, 0f, 0.6f));
        loopB.Add(new RootAttackCommand(roots[1], -13f, 50f, 0f, 0.6f));
        loopB.Add(new RootAttackCommand(roots[0], -13f, 50f, 0f, 0.6f));
        loopB.Add(new RootAttackCommand(roots[3], -13f, 50f, 0f, 2f));

        List<ICommand> loopC = new List<ICommand>();

        loopC.Add(new RootAttackCommand(roots[10], -29f, 30f, 17f, 2f));

        loopC.Add(new RootAttackCommand(roots[0], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[1], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[2], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[3], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[4], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[5], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[6], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[7], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[8], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[9], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[12], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[13], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[14], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[15], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[17], 6f, 10f, 16f, 0f));
        loopC.Add(new RootAttackCommand(roots[18], 6f, 10f, 16f, 1f));

        loopC.Add(new ExplosiveShotCommand(bombSpawner, 15, 10f, 0.6f, 3f, bombPrefab));
        loopC.Add(new ExplosiveShotCommand(bombSpawner, 4, 12f, 0.4f, 0.3f, bombPrefab));
        loopC.Add(new ExplosiveShotCommand(bombSpawner, 7, 9f, 0.35f, 0.3f, bombPrefab));
        loopC.Add(new ExplosiveShotCommand(bombSpawner, 3, 10f, 0.4f, 0.3f, bombPrefab));
        loopC.Add(new ExplosiveShotCommand(bombSpawner, 9, 11f, 0.45f, 0.3f, bombPrefab));
        loopC.Add(new ExplosiveShotCommand(bombSpawner, 5, 8f, 0.4f, 0.3f, bombPrefab));
        loopC.Add(new ExplosiveShotCommand(bombSpawner, 4, 13f, 0.35f, 3f, bombPrefab));
        loopC.Add(new ExplosiveShotCommand(bombSpawner, 15, 10f, 0.8f, 4f, bombPrefab));
        loopC.Add(new ExplosiveShotCommand(bombSpawner, 17, 10f, 0.2f, 4f, bombPrefab));

        List<ICommand> loopD = new List<ICommand>();

        loopD.Add(new RootAttackCommand(roots[10], -28f, 30f, 13f, 2f));

        loopD.Add(new RootAttackCommand(roots[13], -11.5f, 30f, 0.1f, 0f));
        loopD.Add(new RootAttackCommand(roots[18], -12.5f, 30f, 0.1f, 0f));
        loopD.Add(new RootAttackCommand(roots[17], -12f, 30f, 0.1f, 0f));
        loopD.Add(new RootAttackCommand(roots[15], -11.5f, 30f, 0.1f, 0f));
        loopD.Add(new RootAttackCommand(roots[14], -10f, 30f, 0.1f, 1f));

        loopD.Add(new ExplosiveShotCommand(bombSpawner, 15, 12f, 0.6f, 1.5f, bombPrefab));

        loopD.Add(new RootAttackCommand(roots[9], -14.5f, 40f, 0f, 0.2f));
        loopD.Add(new RootAttackCommand(roots[8], -14.5f, 40f, 0f, 0.2f));
        loopD.Add(new RootAttackCommand(roots[7], -15f, 40f, 0f, 0.2f));
        loopD.Add(new RootAttackCommand(roots[6], -19.2f, 40f, 0f, 0.2f));
        loopD.Add(new RootAttackCommand(roots[5], -22.2f, 40f, 0f, 0.2f));
        loopD.Add(new RootAttackCommand(roots[4], -23f, 40f, 0f, 0.8f));

        loopD.Add(new RootAttackCommand(roots[0], -14.5f, 40f, 0f, 0.2f));
        loopD.Add(new RootAttackCommand(roots[1], -14.5f, 40f, 0f, 0.2f));
        loopD.Add(new RootAttackCommand(roots[2], -14.5f, 40f, 0f, 0.2f));
        loopD.Add(new RootAttackCommand(roots[3], -14.5f, 40f, 0f, 2f));

        loopD.Add(new ExplosiveShotCommand(bombSpawner, 10, 15f, 0.4f, 1f, bombPrefab));
        loopD.Add(new ExplosiveShotCommand(bombSpawner, 8, 10f, 0.8f, 1f, bombPrefab));
        loopD.Add(new ExplosiveShotCommand(bombSpawner, 15, 12f, 0.5f, 4f, bombPrefab));

        phase2_LoopCollection.Add(loopA);
        phase2_LoopCollection.Add(loopC);
        phase2_LoopCollection.Add(loopB);
        phase2_LoopCollection.Add(loopD);


        // (Puedes añadir tantos Loops como quieras a la colección)
    }
    // ========================================================================
    //              MÉTODOS DE CONTROL (Llamados por la FSM)
    // ========================================================================

    public void StartPhase1()
    {
        StopBoss(); // Seguridad: detener cualquier ataque previo
        StartCoroutine(ExecutionRoutine_Phase1());
        Debug.Log("BossController: Iniciando Fase 1...");
    }

    public void StartPhase2Loop()
    {
        StopBoss();
        StartCoroutine(ExecutionRoutine_Phase2());
    }

    public void StopBoss()
    {
        StopAllCoroutines();
    }

    // ========================================================================
    //              MOTORES DE EJECUCIÓN (Lógica Interna)
    // ========================================================================

    // --- Motor FASE 1 (Finita) ---
    private IEnumerator ExecutionRoutine_Phase1()
    {
        int currentIndex = 0;

        // Bucle infinito (hasta que se llame a StopBoss)
        while (phase1_FullSequence.Count > currentIndex)
        {
            // 1. Obtener la lista de ataques actual (Ciclo: A -> B -> C -> A...)
            // Usamos el operador módulo (%) para volver a 0 cuando llegamos al final.
            if (phase1_FullSequence.Count > 0)
            {
                List<ICommand> currentList = phase1_FullSequence[currentIndex];

                // 2. Ejecutar esa lista completa
                yield return StartCoroutine(ExecuteCommandList(currentList));

                // 3. Pasar al siguiente índice
                currentIndex++;
            }
            else
            {
                yield return null; // Evitar cuelgue si está vacía
            }

        }

        // Notificar a la FSM que ya terminamos
        OnPhase1Finished?.Invoke();
    }

    // --- Motor FASE 2 (Infinita) ---
    private IEnumerator ExecutionRoutine_Phase2()
    {

        int currentIndex = 0;

        // Bucle infinito (hasta que se llame a StopBoss)
        while (true)
        {
            // 1. Obtener la lista de ataques actual 
            // Usamos el operador módulo (%) para volver a 0 cuando llegamos al final.
            if (phase2_LoopCollection.Count > 0)
            {
                List<ICommand> currentList = phase2_LoopCollection[currentIndex];

                // 2. Ejecutar esa lista completa
                yield return StartCoroutine(ExecuteCommandList(currentList));

                // 3. Elegir aleatoriamente la siguiente lista a ejecutar
                // Asegurarnos de que no sea el mismo índice que el actual
                if (phase2_LoopCollection.Count > 1)
                {
                    int newIndex;
                    do
                    {
                        newIndex = UnityEngine.Random.Range(0, phase2_LoopCollection.Count);
                    }
                    while (newIndex == currentIndex);

                    currentIndex = newIndex;
                }
                // Si solo hay 1 elemento, se mantiene el índice actual (no se puede cambiar)
            }
            // 4. Pausa entre bucles
            yield return new WaitForSeconds(2.0f);
        }
    }

    // --- Ayudante Genérico ---
    // Recorre y ejecuta cualquier lista de comandos ICommand
    private IEnumerator ExecuteCommandList(List<ICommand> pattern)
    {
        foreach (ICommand command in pattern)
        {
            // Ejecuta el comando y ESPERA a que termine (gracias a IEnumerator)
            yield return StartCoroutine(command.Execute());
        }
    }

}
