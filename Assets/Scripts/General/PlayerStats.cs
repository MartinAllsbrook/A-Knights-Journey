using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    int archeryLevel = 1;
    public int ArcheryLevel { get { return archeryLevel; } }
    int archeryXP = 0;
    public int ArcheryXP { get { return archeryXP; } }

    int swordplayLevel = 1;
    public int SwordplayLevel { get { return swordplayLevel; } }
    int swordplayXP = 0;
    public int SwordplayXP { get { return swordplayXP; } }

    int ridingLevel = 1;
    public int RidingLevel { get { return ridingLevel; } }
    int ridingXP = 0;
    public int RidingXP { get { return ridingXP; } }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddArcheryXP(int xp)
    {
        archeryXP += xp;
        CheckLevelUp(ref archeryLevel, ref archeryXP);
    }

    public void AddSwordplayXP(int xp)
    {
        swordplayXP += xp;
        CheckLevelUp(ref swordplayLevel, ref swordplayXP);
    }

    public void AddRidingXP(int xp)
    {
        ridingXP += xp;
        CheckLevelUp(ref ridingLevel, ref ridingXP);
    }

    private void CheckLevelUp(ref int level, ref int xp)
    {
        int xpForNextLevel = GetXPForNextLevel(level);
        while (xp >= xpForNextLevel)
        {
            xp -= xpForNextLevel;
            level++;
            xpForNextLevel = GetXPForNextLevel(level);
        }
    }

    private int GetXPForNextLevel(int level)
    {
        return 100 * level * level; // Example: XP needed grows quadratically
    }
}
