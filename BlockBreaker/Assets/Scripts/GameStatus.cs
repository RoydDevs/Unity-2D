using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [Range(0.1f, 15f)] [SerializeField] float gameSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }
}
