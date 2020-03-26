using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float screenMinWidthUnits = 1f;
    [SerializeField] float screenMaxWidthUnits = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var currentPosX = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        var paddlePos = new Vector2(transform.position.x, transform.position.y)
        {
            x = Mathf.Clamp(currentPosX, screenMinWidthUnits, screenMaxWidthUnits)
        };
        transform.position = paddlePos;
    }
}
