using UnityEngine;

public class GestorMenu : MonoBehaviour
{
    [SerializeField] private UIPanel[] paneles;
    private UIPanel panelActual;
    void Start()
    {
        OcultarPaneles();
        MostrarPanel(0);
    }

    public void MostrarPanel(int index)
    {
        if (index < 0 || index >= paneles.Length)
        {
            Debug.LogWarning("Índice de panel fuera de rango: " + index);
            return;
        }

        // Ocultar el panel actual si existe
        if (panelActual != null)
        {
            panelActual.Ocultar();
        }

        // Mostrar el nuevo panel
        paneles[index].Mostrar();
        panelActual = paneles[index];
    }

    public void OcultarPaneles()
    {
        for (int i = 0; i < paneles.Length; i++)
        {
            paneles[i].Ocultar();
        }
    }
    
    public void Salir()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}