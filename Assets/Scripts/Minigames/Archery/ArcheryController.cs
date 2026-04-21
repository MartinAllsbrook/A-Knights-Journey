using System.Collections;
using TMPro;
using UnityEngine;

class ArcheryController : MinigameController
{
    public static ArcheryController Instance { get; private set; }

    [Header("References")]
    [SerializeField] Target[] staticTargets;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] RailLine railLine;

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
        int count = 0;
        while (true)
        {
            count++;
            if (count % 5 == 0) // Every 5 spawns, spawn a cart
            {
                railLine.SpawnCarts(5);
            }
            else
            {
                SpawnTarget();
            }
            yield return new WaitForSeconds(spawnInterval); // Adjust spawn rate as needed
        }
    }

    void SpawnTarget()
    {
        var available = System.Array.FindAll(staticTargets, t => !t.IsDeployed);
        if (available.Length == 0) return;

        available[Random.Range(0, available.Length)].DeployTarget(10f); // Target stays up for 10 seconds if not hit
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScore(score.ToString());
    }

    // Overloads parent `EndGame()`
    void EndGame()
    {
        // Score
        int xp = Mathf.RoundToInt(score * scoreXPRate);


        EndGame(SkillType.Archery, score, 69, xp);
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
