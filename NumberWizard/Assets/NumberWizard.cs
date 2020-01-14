using UnityEngine;

public class NumberWizard : MonoBehaviour
{
    int _min= 0;
    int _max = 1000;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Debug.Log("Welcome to number wizard");
        Debug.Log($"Choisi un nombre entre {_min} et {_max}");
        Debug.Log($"C'est {(_max - _min) / 2} ?");
    }

    // Update is called once per frame
    void Update()
    {
        var numberTest = (_min+ _max) / 2;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _min= numberTest;
            numberTest = (_max + _min) / 2;
            Debug.Log($"C'est {numberTest} ?");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _max = numberTest;
            numberTest = (_max + _min) / 2;
            Debug.Log($"C'est {numberTest} ?");
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log($"Trop facile :)");
        }
    }
}
