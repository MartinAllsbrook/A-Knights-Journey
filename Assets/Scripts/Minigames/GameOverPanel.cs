using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class GameOverPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI resultsText;
    [SerializeField] Button retryButton;
    [SerializeField] Button continueButton; 
    [SerializeField] StatSlider statSlider;

    public void ShowGameOver(SkillType skill, int score, int coins, int xp, Action continueCallback, Action retryCallback)
    {
        // Text
        titleText.text = $"{skill} Training Over!";
        resultsText.text = $"Score: {score}\n+{coins} Coins\n+{xp} XP";

        // Buttons
        ClearListeners();

        retryButton.onClick.AddListener(() => {
            ClearListeners();
            retryCallback?.Invoke();
            gameObject.SetActive(false);
        });

        continueButton.onClick.AddListener(() => {
            ClearListeners();
            continueCallback?.Invoke();
            gameObject.SetActive(false);
        });

        statSlider.SetStat(skill.ToString(), PlayerStats.Instance.GetSkill(skill));
        
        // Show the panel
        gameObject.SetActive(true);
    }

    void ClearListeners()
    {
        retryButton.onClick.RemoveAllListeners();
        continueButton.onClick.RemoveAllListeners();
    }
}