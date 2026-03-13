using System.Collections;
using TMPro;
using UnityEngine;

class ArcheryController : MinigameController
{
    public static ArcheryController Instance { get; private set; }

    [Header("References")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] TextMeshProUGUI timerText;


    [Header("Prefabs")]
    [SerializeField] Target targetPrefab;

    [Header("Game Settings")]
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] float gameDuration = 60f;

    [SerializeField] float accuracyXPRate = 100f;
    [SerializeField] float scoreXPRate = 4f;

    int score = 0;
    float timeRemaining;
    int shotsTaken = 0;
    int shotsHit = 0;
    int totalTargets = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();

        timeRemaining = gameDuration;

        SpawnTarget();
        StartCoroutine(SpawnTargetsRoutine());
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        UpdateTimer(timeRemaining);

        if (timeRemaining <= 0f && !gameOver)
        {
            EndGame();
        }
    }

    public void UpdateTimer(float timeRemaining)
    {
        timerText.text = $"{timeRemaining:F1}s";
    }

    IEnumerator SpawnTargetsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval); // Adjust spawn rate as needed
            SpawnTarget();
        }
    }

    void SpawnTarget()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(targetPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
    }

    public void AddScore(int points)
    {
        score += points;
        scoreboard.UpdateScore(score.ToString());
    }

    // Overloads parent `EndGame()`
    void EndGame()
    {
        // Score
        int scoreXP = Mathf.RoundToInt(score * scoreXPRate); // 1 XP per point
        string scoreText = $"FINAL SCORE: {score}";

        // Accuracy
        float accuracy = shotsTaken > 0 ? (float)shotsHit / shotsTaken : 0f;
        int accuracyXP = Mathf.RoundToInt(accuracy * accuracyXPRate);
        string accuracyText = $"ACCURACY: {(accuracy * 100f):F1}%";

        string[] statTexts = new string[]
        {
            scoreText,
            accuracyText,
        };
        int[] xpGained = new int[]
        {
            scoreXP,
            accuracyXP,
        };

        EndGame(SkillType.Archery, statTexts, xpGained);
    }

    public void TallyShot()
    {
        shotsTaken++;
    }

    public void TallyHit()
    {
        shotsHit++;
    }

    public void TallyTarget()
    {
        totalTargets++;
    }
}
