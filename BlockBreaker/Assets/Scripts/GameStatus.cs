using TMPro;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    //Config parameters
    [Range(0.1f, 15f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsOneBlock = 50;
    [SerializeField] TextMeshProUGUI scoreText;

    //State variable
    [SerializeField] int currentScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        currentScore += pointsOneBlock;
        scoreText.text = currentScore.ToString();
    }
}
