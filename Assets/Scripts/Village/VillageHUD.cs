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
        archeryStatSlider.SetStat("Archery", stats.GetSkill(SkillType.Archery));
        swordplayStatSlider.SetStat("Swordplay", stats.GetSkill(SkillType.Swordplay));
        ridingStatSlider.SetStat("Riding", stats.GetSkill(SkillType.Riding));
    }

    public void ShowEnterMinigamePanel(string minigameName, System.Action enterCallback, System.Action cancelCallback)
    {
        enterMinigamePanel.ShowPanel(minigameName, enterCallback, cancelCallback);
    }
}
