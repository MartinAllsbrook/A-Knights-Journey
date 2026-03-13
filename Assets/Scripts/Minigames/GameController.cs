using UnityEngine;

/// Base game controller class meant to be extended into specific minigame controllers

class GameController : MonoBehaviour
{
    [SerializeField] GameOverPanel gameOverPanel;
    [SerializeField] protected Scoreboard scoreboard;
    [SerializeField] string gameTitle;

    protected bool gameOver;

    protected int coinsCollected = 0;

    protected virtual void Start()
    {
        gameOver = false;
    }

    protected void EndGame(SkillType skill, string[] statTexts, int[] xpGained)
    {
        StopAllCoroutines();

        gameOver = true;

        Time.timeScale = 0f; // Pause the game

        string titleText = $"{gameTitle} Training Over!";

        int totalXP = 0;
        foreach (int xp in xpGained)
        {
            totalXP += xp;
        }

        PlayerStats.Instance.AddXP(skill, totalXP);

        gameOverPanel.ShowGameOver(titleText, totalXP, statTexts, xpGained, ReturnToVillage);
    }

    public void CollectCoin()
    {
        coinsCollected ++;
        scoreboard.UpdateCoins(coinsCollected);
    }

    void ReturnToVillage()
    {
        Time.timeScale = 1f; // Resume the game
        GameManager.Instance.EnterVillage();
    }
}