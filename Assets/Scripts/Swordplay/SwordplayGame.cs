using System.Collections;
using UnityEngine;

class SwordplayGame : MonoBehaviour
{
    public static SwordplayGame Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] int startingLives = 3;
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] float spawnIntervalDecreaseRate = 0.05f;
    [SerializeField] float minSpawnInterval = 0.2f;
    [SerializeField] float enemySpeed = 2f;
    [SerializeField] float enemySpeedIncreaseRate = 0.1f;

    [Header("References")]
    [SerializeField] SwordplayHUD hud;
    [SerializeField] Transform[] spiderSpawnPoints;

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

    void Start()
    {
        currentLives = startingLives;
        currentScore = 0;
        hud.UpdateLives(currentLives);
        hud.UpdateScore(currentScore);
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
        hud.UpdateLives(currentLives);
        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    public void EnemyDefeated()
    {
        currentScore += 1;
        hud.UpdateScore(currentScore);
    }

    void GameOver()
    {
        Debug.Log("Swordplay: Game Over");
        // Implement game over logic (e.g., show game over screen, reset game, etc.)
    }
}