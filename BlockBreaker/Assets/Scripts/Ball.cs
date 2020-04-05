using UnityEngine;

public class Ball : MonoBehaviour
{
    //Config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float xVelocity = 2f;
    [SerializeField] float yVelocity = 14f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    //Cached references
    private AudioSource audioSource;
    private Rigidbody2D rigidbody2D;

    //State
    private Vector2 paddleToBallVector;
    private bool hasStarted = false;
    public static bool lostLive = false;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = this.transform.position - paddle1.transform.position;
        this.audioSource = GetComponent<AudioSource>();
        this.rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }

        if (lostLive)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
            hasStarted = true;
            lostLive = false;
        }
    }

    private void LockBallToPaddle()
    {
        var paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        this.transform.position = paddlePos + paddleToBallVector;
        LoseCollider.StopTimers = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Add random on the ball velocity to prevent boring ball loops
        var velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        if(hasStarted)
        {
            var clip = ballSounds[Random.Range(0, ballSounds.Length - 1)];
            this.audioSource.PlayOneShot(clip);
            this.rigidbody2D.velocity += velocityTweak;
        }
    }
}
 