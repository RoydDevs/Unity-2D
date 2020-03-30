using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private GameObject blockSparklesVFX;

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
        this.DestroyBlock();
    }

    private void DestroyBlock()
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

        //Animation destroyed
        this.TriggerSparklesVfx();
    }

    private void TriggerSparklesVfx()
    {
        var sparkles = Instantiate(blockSparklesVFX, this.transform.position, this.transform.rotation);
        //Destroy after 2 sec
        Destroy(sparkles, 2f);
    }
}
