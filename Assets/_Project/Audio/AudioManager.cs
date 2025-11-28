using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // --- Singleton ---
    public static AudioManager Instance;

    [Header("Configuración")]
    [SerializeField] private AudioMixer mainMixer;
    
    [Header("Fuentes")]
    [SerializeField] private AudioSource musicSource; // Debe tener 'Loop' activado
    [SerializeField] private AudioSource sfxSource;   // Debe tener 'Loop' desactivado

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // =========================================================
    //              MÉTODOS DE REPRODUCCIÓN
    // =========================================================

    // Llamar esto cuando el jugador salta, golpea, etc.
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // Llamar esto al iniciar cada nivel (en el Start de un script de nivel)
    public void PlayMusic(AudioClip musicClip)
    {
        // Lógica inteligente: Si la canción que pides YA está sonando, no la reinicies.
        // Esto permite que la música continúe suavemente si dos niveles comparten tema.
        if (musicSource.clip == musicClip) 
            return;

        musicSource.Stop(); // Detener la anterior
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    // =========================================================
    //              MÉTODOS DE AJUSTES (ON/OFF)
    // =========================================================

    // Conecta esto al evento 'OnValueChanged' de tu Toggle de Música
    public void ToggleMusic(bool isOn)
    {
        // Si está encendido -> 0 dB (Volumen normal)
        // Si está apagado   -> -80 dB (Silencio absoluto en el Mixer)
        float volumeDb = isOn ? 0f : -80f;
        
        mainMixer.SetFloat("MyExposedParam", volumeDb);
    }

    // Conecta esto al evento 'OnValueChanged' de tu Toggle de SFX
    public void ToggleSFX(bool isOn)
    {
        float volumeDb = isOn ? 0f : -80f;
        
        mainMixer.SetFloat("MyExposedParam 1", volumeDb);
        Debug.Log("AudioManager: SFX volume set to " + volumeDb + " dB");
    }
}