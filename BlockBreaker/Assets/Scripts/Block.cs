using UnityEngine;

public class Block : MonoBehaviour
{
    //Config
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private GameObject blockSparklesVFX;
    [SerializeField] private int maxHits;
    [SerializeField] private Sprite[] hitSprites;

    //Cached references
    private Level level;
    private GameSession gameSession;

    //State variables
    [SerializeField] private int timesHit; //Serialize only to debug 

    public void Start()
    {
        //Count breakables blocks
        this.CountBlocks();

        //Get type object GameSession to access to its methods
        gameSession = FindObjectOfType<GameSession>();
    }

    private void CountBlocks()
    {
        //Get type object Level to access to its methods
        level = FindObjectOfType<Level>();
        if (this.tag == "Breakable")
        {
            level.CountBreakableBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.tag == "Breakable")
        {
            timesHit++;
            if(timesHit >= maxHits)
            {
                this.DestroyBlock();
            }
            else
            {
                ShowNextHitSprite();
            }
        }
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

    public void ShowNextHitSprite()
    {
        GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit - 1];
    }
}
