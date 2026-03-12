using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterMinigamePanel : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Button enterButton;
    [SerializeField] Button cancelButton;
    [SerializeField] TextMeshProUGUI text;

    public void ShowPanel(string minigameName, Action enterCallback, Action cancelCallback)
    {
        text.text = $"Do you want to enter the {minigameName} training grounds?";
        enterButton.onClick.RemoveAllListeners();
        enterButton.onClick.AddListener(() =>
        {
            enterCallback?.Invoke();
            HidePanel();
        });

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(() =>
        {
            cancelCallback?.Invoke();
            HidePanel();
        });

        Debug.Log("Showing enter minigame panel for " + minigameName);
        panel.SetActive(true);
    }

    public void HidePanel()
    {
        enterButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        
        panel.SetActive(false);
    }
}