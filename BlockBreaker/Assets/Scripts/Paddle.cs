using System;
using TMPro;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float screenMinWidthUnits = 1f;
    [SerializeField] float screenMaxWidthUnits = 15f;
    [SerializeField] TextMeshProUGUI timerPaddleText;

    private float timerPaddle = 5.0f;
    private float widthReceived;
    private bool timerPaddleRunning;
    private Vector3 scaleChange;

    // Start is called before the first frame update
    void Start()
    {
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
                this.ChangePaddleWidth(-widthReceived);
                timerPaddleRunning = false;
                timerPaddleText.text = "";
            }
            else
            {
                timerPaddleText.text = "Paddle width " + timerSecond;
            }
        }
    }

    public void ChangePaddleWidth(float width)
    {
        //To reset the paddle
        widthReceived = width;
        scaleChange = new Vector3(widthReceived, 0f, 0f);
        this.transform.localScale += scaleChange;
        screenMaxWidthUnits -= widthReceived >= 0 ? widthReceived : widthReceived * 2;
        screenMinWidthUnits += widthReceived >= 0 ? widthReceived : widthReceived * 2;
        if (timerPaddleRunning == false) timerPaddleRunning = true;


        timerPaddleText.color = widthReceived >= 0 ? Color.green : Color.red;
    }
}
