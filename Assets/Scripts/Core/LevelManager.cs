using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // ❌ PLAYER DIED
    public void OnPlayerDied()
    {
        RestartLevel();
    }

    // ✅ PLAYER OR CLONE WON
    public void OnGoalReached()
    {
        LoadNextLevel();
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;

        if (next >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("GAME COMPLETE");
            return;
        }

        SceneManager.LoadScene(next);
    }
}