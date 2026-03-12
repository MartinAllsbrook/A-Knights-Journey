using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatSlider : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statNameText;
    [SerializeField] TextMeshProUGUI statLevelText;
    [SerializeField] TextMeshProUGUI statXPText;
    [SerializeField] Slider statProgressSlider;

    public void SetStat(string statName, int currentLevel, int currentXP, int xpForNextLevel)
    {
        statNameText.text = statName;
        statLevelText.text = $"Lv. {currentLevel}";
        statXPText.text = $"{currentXP} / {xpForNextLevel}";
        statProgressSlider.value = (float)currentXP / xpForNextLevel;
    }
}
