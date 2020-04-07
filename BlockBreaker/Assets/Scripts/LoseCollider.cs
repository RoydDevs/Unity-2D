using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class LoseCollider : MonoBehaviour
{
    public static bool StopTimers;

    private GameSession gameSession;

    void Start()
    {
        StopTimers = true;
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
