using UnityEngine;
using UnityEngine.UI;

public class MenuAjustes : UIPanel
{
    [SerializeField] private Button botonMusica;
    [SerializeField] private Button botonEfectos;
    [SerializeField] private Button botonAtras;
    [SerializeField] private GestorMenu gestor;

    public override void Mostrar()
    {
        gameObject.SetActive(true);
        if (gestor == null)
        {
            Debug.LogError("GestorMenu no asignado en MenuAjustes");
            return;
        }
        botonAtras.onClick.RemoveAllListeners();
        botonAtras.onClick.AddListener(() => gestor.MostrarPanel(0));
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
}
