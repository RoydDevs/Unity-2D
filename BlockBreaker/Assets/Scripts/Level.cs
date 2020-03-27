using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlock; //Serialize for debugging purposes

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountBreakableBlocks()
    {
        breakableBlock++;
    }
}
