using TMPro;
using UnityEngine;

public class SwordplayHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    public void UpdateLives(int lives)
    {
        livesText.text = $"{lives}";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"{score}";
    }
}
