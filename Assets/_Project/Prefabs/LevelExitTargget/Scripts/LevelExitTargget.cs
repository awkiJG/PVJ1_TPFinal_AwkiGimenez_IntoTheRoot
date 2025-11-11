using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitTrigger : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private string sceneToLoad;
    private string entryPointID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            entryPointID = SceneManager.GetActiveScene().name.ToString();
            if (entryPointID == "Nivel_1_Arbol")
            {
                playerStats.SetEntryPointID(entryPointID);
                sceneToLoad = "Nivel_2_Raices";
                SceneManager.LoadScene(sceneToLoad);
                PlayerPrefs.SetInt("HighestLevelReached", 2);
            }
            else if (entryPointID == "Nivel_2_Raices")
            {
                playerStats.SetEntryPointID(entryPointID);
                sceneToLoad = "Nivel_3_Mosquitos";
                SceneManager.LoadScene(sceneToLoad);
                PlayerPrefs.SetInt("HighestLevelReached", 3);
            }
            else if (entryPointID == "Nivel_3_Mosquitos")
            {
                playerStats.SetEntryPointID(entryPointID);
                sceneToLoad = "Nivel_4_Purificacion";
                SceneManager.LoadScene(sceneToLoad);
                PlayerPrefs.SetInt("HighestLevelReached", 4);
            }
        }
    }
}