using TMPro;
using UnityEngine;

public class ArcheryHUDController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;

    public void UpdateScore(int score)
    {
        scoreText.text = $"{score}";
    }

    public void UpdateTimer(float timeRemaining)
    {
        timerText.text = $"{timeRemaining:F1}s";
    }
}
