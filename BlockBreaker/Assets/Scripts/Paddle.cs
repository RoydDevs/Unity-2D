using UnityEngine;
using UnityEngine.UIElements;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float screenMinWidthUnits = 1.5f;
    [SerializeField] float screenMaxWidthUnits = 15f;

    private Ball ball;
    private GameSession gameSession;

    void Start()
    {
        ball = FindObjectOfType<Ball>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        this.PaddlePosition();
    }

    private void PaddlePosition()
    {
        var paddlePos = new Vector2(this.transform.position.x, this.transform.position.y)
        {
            x = Mathf.Clamp(GetXPos(), screenMinWidthUnits, screenMaxWidthUnits)
        };
        this.transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            Debug.Log(ball.transform.position.x);
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }

    public void UpdatePaddleSize(float width)
    {
        var tempLocalScale = this.transform.localScale;
        tempLocalScale.x += width;
        this.transform.localScale = tempLocalScale;
    }
    
    public void UpdateMinMaxScreen(float widthToAdd)
    {
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
    }

    public void ResetMinMaxScreen(float width)
    {
        if (width >= 0)
        {
            screenMinWidthUnits += width * 2;
            screenMaxWidthUnits -= width * 2;
        }
        else
        {
            screenMinWidthUnits += width;
            screenMaxWidthUnits -= width;
        }
    }
}
