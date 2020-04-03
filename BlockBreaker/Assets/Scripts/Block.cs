using UnityEngine;

public class Block : MonoBehaviour
{
    //Config
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private GameObject blockSparklesVFX;
    [SerializeField] private Sprite[] hitSprites;

    //Cached references
    private Level level;
    private GameSession gameSession;

    //State variables
    [SerializeField] private int timesHit; //Serialize only to debug 
    int i;

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
        if (this.tag == "Breakable" || this.tag == "SpeedBall" || this.tag == "WidthPaddle")
        {
            level.CountBreakableBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.tag == "Breakable")
        {
           this.DestroyBlockCountingHits();
        }

        if (this.tag == "SpeedBall")
        {
            gameSession.ChangeSpeedGame(Random.Range(0.5f, 2.0f));
            this.DestroyBlockCountingHits();
        }

        if (this.tag == "WidthPaddle")
        {
            this.DestroyBlock();
        }
    }

    private void DestroyBlockCountingHits()
    {
        timesHit++;
        var maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            this.DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
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
        var spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.Log($"GameObject : {gameObject.name} : Block sprite missing from array at place {spriteIndex}");
        }
    }
}
