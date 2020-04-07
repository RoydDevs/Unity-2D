using System.Collections;
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

        if (finalLives <= 0)
        {
            titlePage.text = "Game Over...";
            titlePage.color = Color.red;
        }
        else
        {
            titlePage.text = "Congratulations !";
            titlePage.color = Color.green;
            StartCoroutine(Wait(0.5f));
            finalScore.text = $"Final score = {finalPoints * finalLives}";
        }

        var levelPassed = FindObjectOfType<GameSession>().GetLevelPassed();
        StartCoroutine(Wait(0.5f));
        levelWon.text = $"Level won = {levelPassed}";
        StartCoroutine(Wait(0.5f));
        points.text = $"Points = {finalPoints}";
        StartCoroutine(Wait(0.5f));
        lives.text = $"Lives = {finalLives}";

        StartCoroutine(Wait(1.0f));
        finalScore.text = $"Final score = {(levelPassed + finalLives) * finalPoints}";
    }

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);   //Wait
    }
}
