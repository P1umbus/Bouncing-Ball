using UnityEngine;
using UnityEngine.SceneManagement;

public class DeveloperLevelManager : MonoBehaviour
{
    [SerializeField] private GameLevelList Levels;

    [ContextMenu("ChangeLevel")]
    private void ChangeLevel()
    {
        PlayerPrefs.SetInt(Constants.PPname.NumbActiveLevel, ((int)Levels));

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
