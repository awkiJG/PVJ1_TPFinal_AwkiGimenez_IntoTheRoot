using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Into The Root/Player Stats", order = 1)]
public class PlayerStats : ScriptableObject
{
    // --- CAMPOS PRIVADOS (Encapsulación) ---
    
    [Header("Configuración y Reset")]
    [Tooltip("Máximo de vidas que el jugador puede tener.")]
    [SerializeField] private int maxLives = 3;
    [SerializeField] private int defaultSap = 0;
    [SerializeField] private bool defaultHasMap = false;

    [Header("Estado Actual del Jugador")]
    [SerializeField] private int currentLives;
    [SerializeField] private int currentSap;
    [SerializeField] private bool hasMap;
    [HideInInspector] private string entryPointID;

    // ---------------------------------------------
    // --- MÉTODOS DE INICIALIZACIÓN Y RESETEO ---
    // ---------------------------------------------

    public void ResetToDefaults()
    {
        currentLives = maxLives;
        currentSap = defaultSap;
        hasMap = defaultHasMap;
        
        Debug.Log("PlayerStats reseteado: Valores de inicio de partida.");
    }
    
    public void ResetLives()
    {
        currentLives = maxLives;
        Debug.Log("Vidas reseteadas a MAX. Savia y ítems preservados.");
    }

    // ---------------------------------------------
    // --- SETTERS/MODIFICADORES ---
    // ---------------------------------------------

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            // Asegura que no baje de 0
            currentLives = Mathf.Max(0, currentLives - damage); 
        }
    }

    public void AddSap(int amount)
    {
        if (amount > 0)
        {
            currentSap += amount;
        }
    }

    public void SetEntryPointID(string id)
    {
        entryPointID = id;
    }

    /// <summary>
    /// Intenta liberar una cantidad de savia del inventario del jugador.
    /// Se usa específicamente para la mecánica de curación del Gran Árbol.
    /// </summary>
    /// <returns>True si la savia fue liberada con éxito, False si no hay suficiente.</returns>
    public bool ReleaseSap(int amount)
    {
        // 1. Validar que la cantidad es positiva y tenemos suficiente savia
        if (amount > 0 && currentSap >= amount)
        {
            // 2. Ejecutar la acción
            currentSap -= amount;
            Debug.Log($"[Mecánica Final] Se liberaron {amount} de savia. Restante: {currentSap}");
            // Aquí se dispararía el evento para curar al árbol o activar la UI
            return true;
        }
        
        // 3. Fallo
        Debug.Log($"Fallo al liberar savia. Necesario: {amount}, Actual: {currentSap}");
        return false;
    }

    public void GrantMap()
    {
        hasMap = true;
    }

    // ---------------------------------------------
    // --- GETTERS ---
    // ---------------------------------------------

    public int GetCurrentLives()
    {
        return currentLives;
    }
    public int GetMaxLives()
    {
        return maxLives;
    }

    public int GetCurrentSap()
    {
        return currentSap;
    }

    public bool HasMap()
    {
        return hasMap;
    }
    public string GetEntryPointID()
    {
        return entryPointID;
    }
}