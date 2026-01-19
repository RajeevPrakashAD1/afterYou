using UnityEngine;

public class WinManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D other)
    {
        // Player wins
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.OnGoalReached();
            return;
        }

        // Clone wins
        if (other.CompareTag("Clone"))
        {
            LevelManager.Instance.OnGoalReached();
        }
    }
}
