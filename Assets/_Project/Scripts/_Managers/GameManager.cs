// 📁 GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 1. LA INSTANCIA 
    private static GameManager instance;

    // 2. EL ESTADO 
    public bool IsPaused { get; private set; }
    public bool isInputLocked { get; private set; }

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

    private void SetInputLock(bool isLocked)
    {
        isInputLocked = isLocked;
    }
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
    /// Solicita un BLOQUEO DE INPUT (Cinemáticas/Muerte).
    /// El tiempo sigue corriendo.
    /// </summary>
    public static void RequestInputLock(bool isLocked)
    {
        if (instance != null)
        {
            instance.SetInputLock(isLocked);
        }
    }

    public static bool GetIsPaused()
    {
        if (instance != null)
        {
            return instance.IsPaused;
        }
        return false; 
    }
    public static bool IsInputLocked()
    {
        if (instance != null)
        {
            return instance.isInputLocked;
        }
        return false;
    }
}