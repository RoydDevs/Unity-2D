using System;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    //Config parameters
    [Range(0.1f, 15f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsOneBlock = 50;
    [SerializeField] TextMeshProUGUI liveText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerBallText;
    [SerializeField] private int totalLives = 3;

    //State variable
    [SerializeField] public int livesLeft = 3;
    [SerializeField] int currentScore = 0;
    private float timerBallReset = 5.0f;
    private float timerBall = 5.0f;
    private bool timerBallRunning;
    private const float SpeedGame = 1.0f;
    private int levelPassed = 1;

    private void Awake()
    {
        //Singleton : when change level, don't lose the score
        var gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            levelPassed++;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        liveText.text = $"x{livesLeft}";
        scoreText.text = currentScore.ToString();
        timerBallText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        this.ShowSpeedBallTimeLeft();
    }

    public void AddToScore()
    {
        currentScore += pointsOneBlock;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(this.gameObject);
    }

    #region Speed Ball

    //Change speed of the ball
    public void ChangeSpeedGame(float speed)
    {
        this.gameSpeed = speed;
        if (timerBallRunning == false) timerBallRunning = true;
        //If another ball block is touched -> reset timer
        if (timerBallRunning) timerBall = timerBallReset;

        timerBallText.color = speed >= SpeedGame ? Color.red : Color.green;
    }

    //Timer to reset the speed of the ball after X sec
    public void ShowSpeedBallTimeLeft()
    {
        if (LoseCollider.StopTimers) timerBall = 0f;

        if (timerBallRunning)
        {
            timerBall -= Time.deltaTime;
            var timerSecond = Convert.ToInt32(timerBall % 60);
            if (timerSecond == 0)
            {
                this.ChangeSpeedGame(SpeedGame);
                timerBallRunning = false;
                timerBall = timerBallReset;
                timerBallText.text = "";
            }
            else
            {
                timerBallText.text = "Ball speed " + timerSecond;
            }
        }
    }

    #endregion

    #region Lifes

    public bool LoseLive()
    {
        if (livesLeft <= 0) return false;
        livesLeft--;
        liveText.text = $"x{livesLeft}";
        Ball.lostLive = true;
        return true;
    }

    #endregion

    public (int points, int lives, int levelPassed) SaveResults()
    {
        return (currentScore, livesLeft, levelPassed);
    }
}
