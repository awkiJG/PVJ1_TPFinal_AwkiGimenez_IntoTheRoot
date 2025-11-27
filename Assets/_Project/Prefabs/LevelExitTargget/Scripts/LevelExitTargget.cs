using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitTrigger : MonoBehaviour
{
    private string escenaActual;
    private int indexProximaEscena;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            escenaActual = SceneManager.GetActiveScene().name.ToString();
            indexProximaEscena = SceneManager.GetActiveScene().buildIndex + 1;
            if (escenaActual == "Nivel_1_Arbol")
            {
                
                if (PlayerPrefs.GetInt("HighestLevelReached") < 2)
                {
                PlayerPrefs.SetInt("HighestLevelReached", 2);
                }
            }
            else if (escenaActual == "Nivel_2_Raices")
            {
                if (PlayerPrefs.GetInt("HighestLevelReached") < 3)
                {
                PlayerPrefs.SetInt("HighestLevelReached", 3);
                }
            }
            else if (escenaActual == "Nivel_3_Mosquitos")
            {
                if (PlayerPrefs.GetInt("HighestLevelReached") < 4)
                {   
                PlayerPrefs.SetInt("HighestLevelReached", 4);
                }
            }
            SceneManager.LoadScene(indexProximaEscena);
        }
    }
}