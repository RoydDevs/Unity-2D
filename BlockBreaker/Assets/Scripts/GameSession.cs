using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    //Config parameters
    [Range(0.1f, 15f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] TextMeshProUGUI timerBallText;
    private float timerBallReset = 5.0f;
    [SerializeField] TextMeshProUGUI timerPaddleText;
    [SerializeField] int pointsOneBlock = 50;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] private int totalLives = 3;
    [SerializeField] TextMeshProUGUI liveText;
    [SerializeField] private TextMeshProUGUI titleLevel;
    [SerializeField] bool isAutoPlayEnabled;

    //State variable
    [SerializeField] public int livesLeft = 3;
    [SerializeField] int currentScore = 0;
    private float timerBall = 5.0f;
    private bool timerBallRunning;
    private const float SpeedGame = 1.0f;
    private float timerPaddle = 5.0f;
    private float timerPaddleReset = 5.0f;
    private float lastWidthToReset;
    private bool timerPaddleRunning;

    private int levelPassed = 0;

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
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        levelPassed = 0;
        liveText.text = $"x{livesLeft}";
        scoreText.text = currentScore.ToString();
        timerBallText.text = "";
        timerPaddleText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        this.ShowSpeedBallTimeLeft();
        this.ShowPaddleWidthTimeLeft();
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

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
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

    #region Paddle management

    public void ShowPaddleWidthTimeLeft()
    {
        if (timerPaddleRunning)
        {
            Debug.Log($"YES timer run");
            timerPaddle -= Time.deltaTime;
            var timerSecond = Convert.ToInt32(timerPaddle % 60);
            Debug.Log($"Seconde = {timerSecond}");
            if (timerSecond == 0)
            {
                //Reset paddle size
                this.ResetPaddleSize();
                //Clear timer
                timerPaddleRunning = false;
                timerPaddle = timerPaddleReset;
                timerPaddleText.text = "";
            }
            else
            {
                timerPaddleText.text = "Paddle width " + timerSecond;
            }
        }
    }


    public void UpdatePaddleSize(float widthToAdd)
    {
        //If another paddle block is touched -> reset timer and paddle
        if (timerPaddleRunning)
        {
            timerPaddle = timerPaddleReset;
            this.ResetPaddleSize();
        }
        else
        {
            Debug.Log($"Timer paddle can run");
            timerPaddleRunning = true;
        }

        //Paddle size
        var paddle = FindObjectOfType<Paddle>();
        paddle.UpdatePaddleSize(widthToAdd);
        //Text color timer
        timerPaddleText.color = widthToAdd >= 0 ? Color.green : Color.red;
        //Paddle position min and max
        paddle.UpdateMinMaxScreen(widthToAdd);
        //To reset after -> send the opposite
        lastWidthToReset = -widthToAdd;
    }

    public void ResetPaddleSize()
    {
        //Paddle size
        var paddle = FindObjectOfType<Paddle>();
        paddle.UpdatePaddleSize(lastWidthToReset);
        paddle.ResetMinMaxScreen(lastWidthToReset);
    }

    #endregion

    public void StopTimers()
    {
        if (timerBallRunning) timerBall = 0f;
        if (timerPaddleRunning) timerPaddle = 0f;
    }

    #region Lifes

    public bool LoseLive()
    {
        if (livesLeft <= 0) return false;
        livesLeft--;
        liveText.text = $"x{livesLeft}";
        Ball.lostLive = true;
        return true;
    }

    public void WinLive()
    {
        livesLeft++;
        liveText.text = $"x{livesLeft}";
    }

    #endregion

    public (int points, int lives) SaveResults()
    {
        return (currentScore, livesLeft);
    }

    public void LevelPassed()
    {
        //Reset animation level title
        this.titleLevel.GetComponent<Animator>().Rebind();
        //Reset text for each level
        this.titleLevel.text = $"Level {SceneManager.GetActiveScene().buildIndex + 1}";
        levelPassed++;
    }

    public int GetLevelPassed()
    {
        return levelPassed;
    }
}
