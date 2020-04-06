using TMPro;
using UnityEngine;

public class CanvasEndGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titlePage;
    [SerializeField] private TextMeshProUGUI levelWon;
    [SerializeField] private TextMeshProUGUI points;
    [SerializeField] private TextMeshProUGUI lives;
    [SerializeField] private TextMeshProUGUI finalScore;

    // Start is called before the first frame update
    void Start()
    {
        this.ShowFinalScore();
    }

    private void ShowFinalScore()
    {
        var (finalPoints, finalLives) = FindObjectOfType<GameSession>().SaveResults();

        points.text = $"Points = {finalPoints}";
        lives.text = $"Lives = {finalLives}";

        if (finalLives <= 0)
        {
            titlePage.text = "Game Over...";
            titlePage.color = Color.red;
        }
        else
        {
            titlePage.text = "Congratulations !";
            titlePage.color = Color.green;
            finalScore.text = $"Final score = {finalPoints * finalLives}";
        }

        var levelPassed = FindObjectOfType<GameSession>().GetLevelPassed();
        levelWon.text = $"Level won : {levelPassed}";
        finalScore.text = $"Final score = {(levelPassed + finalLives) * finalPoints}";
    }
}
