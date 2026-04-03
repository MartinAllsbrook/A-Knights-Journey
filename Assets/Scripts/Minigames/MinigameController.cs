using UnityEngine;

/// Base game controller class meant to be extended into specific minigame controllers

public class MinigameController : MonoBehaviour
{
    [SerializeField] GameOverPanel gameOverPanel;
    [SerializeField] Scoreboard scoreboard;
    [SerializeField] string gameTitle;

    protected bool gameOver;

    protected int coinsCollected = 0;

    protected virtual void Start()
    {
        gameOver = false;
    }

    protected void EndGame(SkillType skill, int score, int coins, int xp)
    {
        StopAllCoroutines();

        gameOver = true;

        Time.timeScale = 0f; // Pause the game

        string titleText = $"{gameTitle} Training Over!";

        PlayerStats.Instance.AddXP(skill, xp);

        gameOverPanel.ShowGameOver(skill, score, coins, xp, ReturnToVillage, RetryGame);
    }

    protected void UpdateScore(string score)
    {
        scoreboard.UpdateScore(score);
    }

    public void CollectCoin()
    {
        coinsCollected++;
        scoreboard.UpdateCoins(coinsCollected);
    }

    void ReturnToVillage()
    {
        Time.timeScale = 1f; // Resume the game
        GameManager.Instance.EnterScene(SceneTag.Village);
    }

    //TODO: Implement retry functionality
    void RetryGame()
    {
        // Time.timeScale = 1f; // Resume the game
        // GameManager.Instance.ReloadCurrentScene();
    }
}