using UnityEngine;

class VillageManager : MonoBehaviour
{
    public static VillageManager Instance { get; private set; }

    [SerializeField] EnterMinigamePanel enterMinigamePanel;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void ShowEnterMessage(SceneTag sceneTag)
    {
        enterMinigamePanel.ShowPanel(GetActionText(sceneTag), () => EnterCallback(sceneTag), CancelCallback);

        Time.timeScale = 0f; // Pause the game while the panel is active
    }

    void EnterCallback(SceneTag sceneTag)
    {
        enterMinigamePanel.HidePanel();
        GameManager.Instance.EnterScene(sceneTag);

        Time.timeScale = 1f; // Resume the game when the panel is closed
    }

    void CancelCallback()
    {
        enterMinigamePanel.HidePanel();

        Time.timeScale = 1f; // Resume the game when the panel is closed
    }

    string GetActionText(SceneTag sceneTag)
    {
        switch (sceneTag)
        {
            case SceneTag.Swordplay:
                return "Do you want to enter Swordplay training?";
            case SceneTag.Archery:
                return "Do you want to enter Archery training?";
            case SceneTag.Riding:
                return "Do you want to enter Riding training?";
            case SceneTag.Tournament:
                return "Are you ready to enter the Tournament?";
            default:
                return "Unknown";
        }
    }


}