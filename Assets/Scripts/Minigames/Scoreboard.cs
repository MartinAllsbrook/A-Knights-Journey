using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI coinsText;

    public void UpdateScore(string score)
    {
        scoreText.text = score;
    }

    public void UpdateCoins(int coins)
    {
        coinsText.text = $"{coins}";
    }
}
