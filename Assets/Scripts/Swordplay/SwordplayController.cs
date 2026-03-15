using System.Collections;
using TMPro;
using UnityEngine;

class SwordplayController : MinigameController
{
    public static SwordplayController Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] int startingLives = 3;
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] float spawnIntervalDecreaseRate = 0.05f;
    [SerializeField] float minSpawnInterval = 0.2f;
    [SerializeField] float enemySpeed = 2f;
    [SerializeField] float enemySpeedIncreaseRate = 0.1f;

    [Header("References")]
    [SerializeField] Transform[] spiderSpawnPoints;
    [SerializeField] HealthBar healthBar;

    [Header("Prefabs")]
    [SerializeField] Spider spiderPrefab;

    int currentLives;
    int currentScore;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    protected override void Start()
    {
        base.Start();
        currentLives = startingLives;
        currentScore = 0;
        UpdateLives(currentLives);
        UpdateScore(currentScore.ToString());
        Debug.Log($"Swordplay: Game Started - Lives: {currentLives}, Score: {currentScore}");
        StartCoroutine(SpawnSpiders());
    }

    void Update()
    {
        spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - spawnIntervalDecreaseRate * Time.deltaTime);
        
        enemySpeed += enemySpeedIncreaseRate * Time.deltaTime;
    }

    IEnumerator SpawnSpiders()
    {
        while (currentLives > 0)
        {
            int spawnIndex = Random.Range(0, spiderSpawnPoints.Length);
            Spider spider = Instantiate(spiderPrefab, spiderSpawnPoints[spawnIndex].position, spiderSpawnPoints[spawnIndex].rotation);
            spider.SetSpeed(enemySpeed);
            yield return new WaitForSeconds(spawnInterval); // Adjust spawn rate as needed
        }
    }

    public void PlayerHit()
    {
        currentLives--;
        UpdateLives(currentLives);
        if (currentLives <= 0)
        {
            EndGame();
        }
    }
    
    protected void UpdateLives(int health)
    {
        healthBar.UpdateHealthBar(health);
    }


    public void EnemyDefeated()
    {
        currentScore += 1;
        UpdateScore(currentScore.ToString());
    }

    void EndGame()
    {
        EndGame(SkillType.Swordplay, new string[0], new int[0]);
    }
}