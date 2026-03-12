using UnityEngine;

public class VillageHUD : MonoBehaviour
{
    public static VillageHUD Instance { get; private set; }

    [SerializeField] EnterMinigamePanel enterMinigamePanel;
    
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ShowEnterMinigamePanel(string minigameName, System.Action enterCallback, System.Action cancelCallback)
    {
        enterMinigamePanel.ShowPanel(minigameName, enterCallback, cancelCallback);
    }
}
