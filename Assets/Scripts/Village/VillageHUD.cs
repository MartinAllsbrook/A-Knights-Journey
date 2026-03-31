using UnityEngine;

public class VillageHUD : MonoBehaviour
{
    public static VillageHUD Instance { get; private set; }

    [SerializeField] EnterMinigamePanel enterMinigamePanel;

    [SerializeField] StatSlider archeryStatSlider;
    [SerializeField] StatSlider swordplayStatSlider;
    [SerializeField] StatSlider ridingStatSlider;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        UpdateStatSliders();
    }

    public void UpdateStatSliders()
    {
        PlayerStats stats = PlayerStats.Instance;
        archeryStatSlider.SetStat("Archery", stats.GetLevel(SkillType.Archery), stats.GetXP(SkillType.Archery), 100);
        swordplayStatSlider.SetStat("Swordplay", stats.GetLevel(SkillType.Swordplay), stats.GetXP(SkillType.Swordplay), 100);
        ridingStatSlider.SetStat("Riding", stats.GetLevel(SkillType.Riding), stats.GetXP(SkillType.Riding), 100);
    }

    public void ShowEnterMinigamePanel(string minigameName, System.Action enterCallback, System.Action cancelCallback)
    {
        enterMinigamePanel.ShowPanel(minigameName, enterCallback, cancelCallback);
    }
}
