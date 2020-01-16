using UnityEngine;
using TMPro;

public class NumberWizard : MonoBehaviour
{
    [SerializeField] int min;
    [SerializeField] int max;
    [SerializeField] TextMeshProUGUI guessText;
    int guessNumber;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        NextNumber();
    }

    public void OnPressHigher()
    {
        min = guessNumber + 1;
        NextNumber();
    }

    public void OnPressLower()
    {
        max = guessNumber - 1;
        NextNumber();
    }

    public void NextNumber()
    {
	    guessNumber = Random.Range(min, max + 1);
        guessText.text = guessNumber.ToString();
    }
}
