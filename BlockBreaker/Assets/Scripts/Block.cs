using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound;

    //Cached references
    private Level level;
    private GameSession gameSession;

    public void Start()
    {
        //Get type object Level to access to its methods
        level = FindObjectOfType<Level>();
        level.CountBreakableBlocks();

        //Get type object GameSession to access to its methods
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Play sound
        //Son plus fort : il part de la caméra
        //AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        //Son plus faible -> il part du bloc
        AudioSource.PlayClipAtPoint(breakSound, this.transform.position);

        //ResetGame the block
        Destroy(gameObject);

        //Update the counter
        level.BlockDestroyed();

        //Update the score
        gameSession.AddToScore();
    }
}
