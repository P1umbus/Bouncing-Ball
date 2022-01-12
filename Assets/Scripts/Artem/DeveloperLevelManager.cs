using UnityEngine;
using UnityEngine.SceneManagement;

public class DeveloperLevelManager : MonoBehaviour
{
    [SerializeField] private Constants.GameLevelList Levels;

    [ContextMenu("ChangeLevel")]
    private void ChangeLevel()
    {
        PlayerPrefs.SetInt(Constants.NumbActiveLevel, ((int)Levels));

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
