using UnityEngine;

public enum SkillType
{
    Archery,
    Swordplay,
    Riding
}

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

    public void AddXP(SkillType skill, int xp)
    {
        switch (skill)
        {
            case SkillType.Archery:
                archeryXP += xp;
                CheckLevelUp(ref archeryLevel, ref archeryXP);
                break;
            case SkillType.Swordplay:
                swordplayXP += xp;
                CheckLevelUp(ref swordplayLevel, ref swordplayXP);
                break;
            case SkillType.Riding:
                ridingXP += xp;
                CheckLevelUp(ref ridingLevel, ref ridingXP);
                break;
        }
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
        return 100; // * level;
    }
}
