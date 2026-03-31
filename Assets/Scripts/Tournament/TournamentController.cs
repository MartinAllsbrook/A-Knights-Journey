using UnityEngine;

public class TournamentController : MonoBehaviour
{
    [SerializeField] int playerIndex = 0;
    [SerializeField] Contestant[] contestants;
    [SerializeField] int levelRange = 2;

    int targetLevel = 10;

    void Start()
    {
        for (int i = 0; i < contestants.Length; i++)
        {
            if (i == playerIndex)
            {
                var stats = PlayerStats.Instance;
                float archery = stats.GetLevel(SkillType.Archery);
                float swordplay = stats.GetLevel(SkillType.Swordplay);
                float riding = stats.GetLevel(SkillType.Riding);
                Debug.Log($"Player Stats - Archery: {archery}, Swordplay: {swordplay}, Riding: {riding}");
                contestants[i].Set(archery, swordplay, riding, true); // Set player stats here
            }
            else
            {       
                int minLevel = Mathf.Max(1, targetLevel - levelRange);
                int maxLevel = targetLevel + levelRange;
                contestants[i].Set(Random.Range(minLevel, maxLevel), Random.Range(minLevel, maxLevel), Random.Range(minLevel, maxLevel)); // Randomize AI stats
            }
        }
    }

    public void SetTargetLevel(int level)
    {
        targetLevel = level;
    }
}
