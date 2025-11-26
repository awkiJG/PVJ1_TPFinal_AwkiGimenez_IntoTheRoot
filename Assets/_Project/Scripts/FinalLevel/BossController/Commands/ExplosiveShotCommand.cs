// 📁 ExplosiveShotCommand.cs
using UnityEngine;
using System.Collections;

public class ExplosiveShotCommand : ICommand
{
    // --- Datos de la Orden ---
    private BombSpawner spawner;   // El "Soldado" (la fábrica)
    private GameObject bombPrefab; // El plano de lo que vamos a crear
    private int spikeCount;
    private float force;
    private float scale;           // Tamaño de la bomba
    private float delayAfterAttack;// Tiempo que el Boss espera antes de la siguiente orden

    // --- Constructor ---
    public ExplosiveShotCommand(BombSpawner spawner, int spikeCount, float force, float scale, float delayAfterAttack, GameObject bombPrefab)
    {
        this.spawner = spawner;
        this.spikeCount = spikeCount;
        this.force = force;
        this.scale = scale;
        this.delayAfterAttack = delayAfterAttack;
        this.bombPrefab = bombPrefab;
    }

    // --- Ejecución ---
    public IEnumerator Execute()
    {
        // 1. Dar la orden al Spawner para que dispare
        // Le pasamos todos los datos, incluido el prefab y la escala.
        spawner.FireBomb(spikeCount, force, scale, bombPrefab);

        // 2. Esperar el tiempo definido
        yield return new WaitForSeconds(delayAfterAttack);
    }
}