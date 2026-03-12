using UnityEngine;

/// Base game controller class meant to be extended into specific minigame controllers

class GameController : MonoBehaviour
{
    [SerializeField] GameOverPanel gameOverPanel;
    [SerializeField] string gameTitle;

    protected bool gameOver;

    protected virtual void Start()
    {
        gameOver = false;
    }

    protected void EndGame(int totalXP, string[] statTexts, int[] xpGained)
    {
        StopAllCoroutines();

        gameOver = true;

        Time.timeScale = 0f; // Pause the game
        gameOverPanel.ShowGameOver(gameTitle, totalXP, statTexts, xpGained, ReturnToVillage);
    }

    void ReturnToVillage()
    {
        Time.timeScale = 1f; // Resume the game
        GameManager.Instance.EnterVillage();
    }
}