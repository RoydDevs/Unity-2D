using UnityEngine;

public class Level : MonoBehaviour
{
    //Config parameters
    [SerializeField] int breakableBlock; //On met [SerializeField] pour pouvoir voir le nombre calculé dans Unity

    //Cached references
    private SceneLoader sceneLoader;
    private GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountBreakableBlocks()
    {
        breakableBlock++;
    }

    public void BlockDestroyed()
    {
        breakableBlock--;
        if (breakableBlock <= 0)
        {
            sceneLoader.LoadNextScene();
            gameSession.LevelPassed();
        }
    }
}
