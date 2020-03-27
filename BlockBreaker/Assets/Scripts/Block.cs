using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound;

    //Cached references
    private Level level;
    private GameStatus gameStatus;

    public void Start()
    {
        //Get type object Level to access to its methods
        level = FindObjectOfType<Level>();
        level.CountBreakableBlocks();

        //Get type object GameStatus to access to its methods
        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Play sound
        //Son plus fort : il part de la caméra
        //AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        //Son plus faible -> il part du bloc
        AudioSource.PlayClipAtPoint(breakSound, this.transform.position);

        //Destroy the block
        Destroy(gameObject);

        //Update the counter
        level.BlockDestroyed();

        //Update the score
        gameStatus.AddToScore();
    }
}
