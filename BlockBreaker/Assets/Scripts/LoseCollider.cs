using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    public static bool StopTimers;

    private GameSession gameSession;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopTimers = true;
        if (!gameSession.LoseLive())
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
