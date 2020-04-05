using System;
using TMPro;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float screenMinWidthUnitsReset = 1.5f;
    [SerializeField] float screenMaxWidthUnitsReset = 15f;
    [SerializeField] float screenMinWidthUnits = 1.5f;
    [SerializeField] float screenMaxWidthUnits = 15f;
    [SerializeField] TextMeshProUGUI timerPaddleText;

    private float timerPaddle = 5.0f;
    private float timerPaddleReset = 5.0f;
    [SerializeField] private float lastWidthToReset;
    private bool timerPaddleRunning;

    // Start is called before the first frame update
    void Start()
    {
        timerPaddleRunning = false;
        timerPaddleText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        var currentPosX = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        var paddlePos = new Vector2(this.transform.position.x, this.transform.position.y)
        {
            x = Mathf.Clamp(currentPosX, screenMinWidthUnits, screenMaxWidthUnits)
        };
        this.transform.position = paddlePos;

        if (timerPaddleRunning)
        {
            timerPaddle -= Time.deltaTime;
            var timerSecond = Convert.ToInt32(timerPaddle % 60);
            if (timerSecond == 0)
            {
                Debug.Log($"TIMER END END END GO TO RESET");
                //Reset paddle size
                this.ResetPaddleSize();
                //Clear timer
                timerPaddle = timerPaddleReset;
                timerPaddleText.text = "";
                timerPaddleRunning = false;
            }
            else
            {
                timerPaddleText.text = "Paddle width " + timerSecond;
            }
        }
    }

    public void UpdatePaddleSize(float widthToAdd)
    {
        //Paddle size
        var tempLocalScale = transform.localScale;
        tempLocalScale.x += widthToAdd;
        this.transform.localScale = tempLocalScale;
        //Start timer
        timerPaddleRunning = true;
        //Text color timer
        timerPaddleText.color = widthToAdd >= 0 ? Color.green : Color.red;
        //Paddle position min and max
        if (widthToAdd >= 0)
        {
            screenMinWidthUnits += widthToAdd;
            screenMaxWidthUnits -= widthToAdd;
        }
        else
        {
            screenMinWidthUnits += widthToAdd * 2;
            screenMaxWidthUnits -= widthToAdd * 2;
        }
        //To reset after -> send the opposite
        lastWidthToReset = -widthToAdd;
    }

    public void ResetPaddleSize()
    {
        //Paddle size
        var tempLocalScale = transform.localScale;
        tempLocalScale.x += lastWidthToReset;
        this.transform.localScale = tempLocalScale;
        //Paddle position min and max
        if(lastWidthToReset >= 0)
        {
            screenMinWidthUnits += lastWidthToReset * 2;
            screenMaxWidthUnits -= lastWidthToReset * 2;
        }
        else
        {
            screenMinWidthUnits += lastWidthToReset;
            screenMaxWidthUnits -= lastWidthToReset;
        }
    }
}
