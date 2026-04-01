using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TournamentController : MonoBehaviour
{
    public static TournamentController Instance { get; private set; }

    [SerializeField] int playerIndex = 0;
    [SerializeField] int levelRange = 2;
    [SerializeField] int targetLevel = 10;
    [SerializeField] float countdownTime = 15f;
    [SerializeField] Sprite[] contestantSprites;

    [Header("References")]
    [SerializeField] Contestant[] contestants;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TournamentScoreboard scoreboard;

    float countdownRemaining;
    bool countdownStarted = false;
    int contestantsFinished = 0;
    bool tournamentFinished = false;

    List<Contestant> finishedContestants = new List<Contestant>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

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
                Sprite contestantSprite = contestantSprites[Random.Range(0, contestantSprites.Length)];

                contestants[i].Set(riding, archery, swordplay, contestantSprite, true); // Set player stats here
            }
            else
            {       
                int minLevel = Mathf.Max(1, targetLevel - levelRange);
                int maxLevel = targetLevel + levelRange;
                Sprite contestantSprite = contestantSprites[Random.Range(0, contestantSprites.Length)];
                
                contestants[i].Set(Random.Range(minLevel, maxLevel), Random.Range(minLevel, maxLevel), Random.Range(minLevel, maxLevel), contestantSprite); // Randomize AI stats
            }
        }
    }

    void Update()
    {
        if (countdownStarted)
        {
            countdownRemaining -= Time.deltaTime;
            countdownText.text = $"Time Remaining: {Mathf.Ceil(countdownRemaining)}s";
            if (countdownRemaining <= 0 && !tournamentFinished)
            {
                if (!tournamentFinished)
                {
                    FinishTournament();
                }
            }
        }
    }

    public void SetTargetLevel(int level)
    {
        targetLevel = level;
    }

    public void ContestantFinished(Contestant contestant)
    {
        contestantsFinished++;
        finishedContestants.Add(contestant);

        if (contestantsFinished == 1)
        {
            StartCountdown();
        }
        if (contestantsFinished >= contestants.Length)
        {
            FinishTournament();
        }
    }

    void StartCountdown()
    {
        countdownRemaining = countdownTime;
        countdownStarted = true;
        countdownText.gameObject.SetActive(true);
        // Implement countdown logic here (e.g., show UI, play sound, etc.)
        Debug.Log("First contestant finished! Starting countdown...");
    }

    void FinishTournament()
    {
        tournamentFinished = true;

        foreach (var contestant in contestants)
        {
            if (!finishedContestants.Contains(contestant))
            {
                contestant.dnf = true; // Mark as DNF
                finishedContestants.Add(contestant); // Add to finished list for scoreboard
            }
        }

        // Update the scoreboard with the final results
        scoreboard.gameObject.SetActive(true);
        scoreboard.SetResults(finishedContestants.ToArray());
    }
}
