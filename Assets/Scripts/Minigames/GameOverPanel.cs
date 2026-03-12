using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class GameOverPanel : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameOverPanelRow statRowPrefab;

    [Header("UI References")]
    [SerializeField] RectTransform statsContainer;
    [SerializeField] TextMeshProUGUI totalXPText;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] Button continueButton; 

    public void ShowGameOver(string title, int totalXP, string[] statTexts, int[] xpGained, Action continueCallback)
    {
        titleText.text = title;
        totalXPText.text = "+" + totalXP + " XP";
        PopulateStats(statTexts, xpGained);

        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() => {
            continueCallback?.Invoke();
            gameObject.SetActive(false);
        });
        
        gameObject.SetActive(true);
    }

    void PopulateStats(string[] statTexts, int[] xpGained)
    {
        for (int i = 0; i < statTexts.Length; i++)
        {
            GameOverPanelRow newRow = Instantiate(statRowPrefab, statsContainer);
            newRow.Set(statTexts[i], xpGained[i]);
        }
    }
}