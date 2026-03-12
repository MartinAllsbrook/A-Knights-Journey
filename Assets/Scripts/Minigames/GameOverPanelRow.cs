using TMPro;
using UnityEngine;

public class GameOverPanelRow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statTextBox;
    [SerializeField] TextMeshProUGUI xpGainedTextBox;

    public void Set(string statText, int xpGained)
    {
        statTextBox.text = statText;
        xpGainedTextBox.text = "+" + xpGained + " XP";
    }
}