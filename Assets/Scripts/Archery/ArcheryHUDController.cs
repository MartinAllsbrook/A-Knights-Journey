using TMPro;
using UnityEngine;

public class ArcheryHUDController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    public void UpdateScore(int score)
    {
        scoreText.text = $"{score}";
    }
}
