using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var currentPosX = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        var paddlePos = new Vector2(currentPosX, transform.position.y);
        transform.position = paddlePos;
    }
}
