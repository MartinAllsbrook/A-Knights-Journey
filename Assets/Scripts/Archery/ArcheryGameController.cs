using System.Collections;
using UnityEngine;

public class ArcheryGameController : MonoBehaviour
{
    public static ArcheryGameController Instance { get; private set; }

    [Header("References")]
    [SerializeField] ArcheryHUDController hudController;
    [SerializeField] ArcheryGameOverScreen gameOverScreen;
    [SerializeField] Transform[] spawnPoints;

    [Header("Prefabs")]
    [SerializeField] Target targetPrefab;

    [Header("Game Settings")]
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] float gameDuration = 60f;

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

    void Start()
    {
        timeRemaining = gameDuration;

        SpawnTarget();
        StartCoroutine(SpawnTargetsRoutine());
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        hudController.UpdateTimer(timeRemaining);

        if (timeRemaining <= 0f)
        {
            EndGame();
        }
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
        hudController.UpdateScore(score);
    }

    void EndGame()
    {
        // Handle end game logic here (e.g., show game over screen, stop spawning targets, etc.)
        StopAllCoroutines();
        gameOverScreen.ShowGameOverScreen(score, shotsTaken, shotsHit, totalTargets);
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
