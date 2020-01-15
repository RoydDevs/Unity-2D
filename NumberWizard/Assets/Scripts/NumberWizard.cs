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
        guessNumber = (min + max) / 2;
        guessText.text = guessNumber.ToString();
    }

    public void OnPressHigher()
    {
        min = guessNumber;
        NextNumber();
    }

    public void OnPressLower()
    {
        max = guessNumber;
        NextNumber();
    }

    public void NextNumber()
    {
        guessNumber = (max + min) / 2;
        guessText.text = guessNumber.ToString();
    }
}
