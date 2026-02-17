using System.Collections;
using UnityEngine;

public class ArcheryGameController : MonoBehaviour
{
    public static ArcheryGameController Instance { get; private set; }

    [SerializeField] ArcheryHUDController hudController;
    [SerializeField] Target targetPrefab;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float spawnInterval = 2f;

    private int score = 0;

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
        SpawnTarget();
        StartCoroutine(SpawnTargetsRoutine());
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
}
