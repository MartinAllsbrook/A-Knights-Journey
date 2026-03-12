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

    public void ShowEnterGameMessage(GameType gameType)
    {
        string gameName = GetGameName(gameType);
        enterMinigamePanel.ShowPanel(gameName, () => EnterMinigame(gameType), HideEnterGameMessage);

    }

    void HideEnterGameMessage()
    {
        enterMinigamePanel.HidePanel();
    }

    void EnterMinigame(GameType gameType)
    {
        switch (gameType)
        {
            case GameType.Swordplay:
                Debug.Log("Entering Swordplay minigame...");
                GameManager.Instance.EnterSwordplayGame();
                break;
            case GameType.Archery:
                Debug.Log("Entering Archery minigame...");
                GameManager.Instance.EnterArcheryGame();
                break;
            case GameType.Riding:
                Debug.Log("Entering Riding minigame...");
                GameManager.Instance.EnterRidingGame();
                break;
            default:
                Debug.LogWarning("Unknown game type: " + gameType);
                break;
        }
    }

    string GetGameName(GameType gameType)
    {
        switch (gameType)
        {
            case GameType.Swordplay:
                return "Swordplay";
            case GameType.Archery:
                return "Archery";
            case GameType.Riding:
                return "Riding";
            default:
                return "Unknown";
        }
    }
}