// 📁 GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 1. LA INSTANCIA 
    private static GameManager instance;

    // 2. EL ESTADO 
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        // Configuración del Singleton 
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // --- 3. LA LÓGICA ---
    // Este método solo puede ser llamado por esta misma clase.
    private void SetPauseState(bool isPaused)
    {
        IsPaused = isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }
    // --- 4. LA FACHADA ESTÁTICA ---
    // Otros scripts solo pueden llamar a estos métodos estáticos.
    
    /// <summary>
    /// Solicita al GameManager que cambie el estado de pausa.
    /// </summary>
    public static void RequestPauseState(bool isPaused)
    {
        // El método estático llama al método privado de la instancia
        if (instance != null)
        {
            instance.SetPauseState(isPaused);
        }
    }

    /// <summary>
    /// Devuelve el estado actual de la pausa.
    /// </summary>
    public static bool GetIsPaused()
    {
        if (instance != null)
        {
            return instance.IsPaused;
        }
        return false; // Estado seguro por defecto si no hay instancia
    }
}