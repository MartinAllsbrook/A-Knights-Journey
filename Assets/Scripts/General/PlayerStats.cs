using UnityEngine;
using System.Collections.Generic;

public enum SkillType
{
    Archery,
    Swordplay,
    Riding
}

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    readonly Dictionary<SkillType, SkillData> skills = new Dictionary<SkillType, SkillData>
    {
        { SkillType.Archery,   new SkillData() },
        { SkillType.Swordplay, new SkillData() },
        { SkillType.Riding,    new SkillData() }
    };

    public SkillData GetSkill(SkillType skill) => skills[skill];
    public int GetLevel(SkillType skill) => skills[skill].Level;
    public int GetXP(SkillType skill) => skills[skill].XP;

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
        SkillData data = skills[skill];
        data.XP += xp;
        CheckLevelUp(data);
    }

    void CheckLevelUp(SkillData data)
    {
        int xpForNextLevel = GetXPForNextLevel(data.Level);
        while (data.XP >= xpForNextLevel)
        {
            data.XP -= xpForNextLevel;
            data.Level++;
            xpForNextLevel = GetXPForNextLevel(data.Level);
        }
    }

    int GetXPForNextLevel(int level)
    {
        return 100; // * level;
    }
}

public class SkillData
{
    public int Level = 1;
    public int XP = 0;
}
