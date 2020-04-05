using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    public static bool StopTimers;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopTimers = true;
        SceneManager.LoadScene("GameOver");
    }
}
