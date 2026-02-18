using TMPro;
using UnityEngine;

public class ArcheryGameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI statsText;
    [SerializeField] TextMeshProUGUI xpText;

    public void ShowGameOverScreen(int finalScore, int shotsTaken, int shotsHit, int totalTargets)
    {
        gameOverPanel.SetActive(true);
        float accuracy = shotsTaken > 0 ? (float)shotsHit / shotsTaken * 100f : 0f;
        statsText.text = $"{finalScore} \n {accuracy:F1}% \n {shotsHit} / {totalTargets}";

        int xpEarned = finalScore; // Simple XP calculation based on score
        xpText.text = $"+{xpEarned} XP";
    }

    public void HideGameOverScreen()
    {
        gameOverPanel.SetActive(false);
    }
}
