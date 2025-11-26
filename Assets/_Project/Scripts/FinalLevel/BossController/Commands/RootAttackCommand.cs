// 📁 RootAttackCommand.cs
using UnityEngine;
using System.Collections;

public class RootAttackCommand : ICommand
{
    // --- Datos de la Orden ---
    private RootAttack root;       
    private float distance;
    private float speed;
    private float waitTime;       
    private float delayAfterAttack;

    // --- Constructor (Inyección de dependencias) ---
    // Aquí recibimos todos los datos necesarios al crear la orden.
    public RootAttackCommand(RootAttack root, float distance, float speed, float waitTime, float delayAfterAttack)
    {
        this.root = root;
        this.distance = distance;
        this.speed = speed;
        this.waitTime = waitTime;
        this.delayAfterAttack = delayAfterAttack;
    }

    // --- Ejecución ---
    public IEnumerator Execute()
    {
        // 1. Dar la orden a la raíz específica
        root.ActivateAttack(distance, speed, waitTime);

        // 2. Esperar el tiempo definido antes de devolver el control al BossController
        yield return new WaitForSeconds(delayAfterAttack);
    }
}