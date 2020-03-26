using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle1;

    private Vector2 paddleToBallVector;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = this.transform.position - paddle1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        this.transform.position = paddlePos + paddleToBallVector;
    }
}
 