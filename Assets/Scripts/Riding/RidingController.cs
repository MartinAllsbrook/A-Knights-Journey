using UnityEngine;
using System.Collections.Generic;
using TMPro;

class RidingController : MinigameController
{
    public static RidingController Instance { get; private set; }

    [Header("References")]
    [SerializeField] Rigidbody2D chunksParent;

    [Header("Prefabs")]
    [SerializeField] Transform initialChunkPrefab;
    [SerializeField] Transform[] chunkPrefabs;

    [Header("Chunk Settings")]
    [SerializeField] int chunkSize = 50;
    [SerializeField] int loadedDistance = 3;

    [Header("Speed Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float moveSpeedIncreaseRate = 0.05f;

    [Header("XP Rates")]
    [SerializeField] float scoreXPRate = 0.25f;

    public float MoveSpeed => moveSpeed;

    readonly Dictionary<int, Transform> loadedChunks = new Dictionary<int, Transform>();

    // Stats
    float distanceTraveled = 0f;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        int currentChunk = Mathf.FloorToInt((-chunksParent.position.y) / chunkSize);
        UpdateLoadedChunks(currentChunk);

        // Update distance traveled
        distanceTraveled = -chunksParent.position.y;
        UpdateScore($"{distanceTraveled:F1}m");
    }

    void FixedUpdate()
    {
        moveSpeed += moveSpeedIncreaseRate * Time.fixedDeltaTime;
        chunksParent.linearVelocity = Vector2.down * moveSpeed;
    }

    void UpdateLoadedChunks(int currentChunk)
    {
        int minChunk = currentChunk - loadedDistance;
        int maxChunk = currentChunk + loadedDistance;

        for (int chunk = minChunk; chunk <= maxChunk; chunk++)
        {
            if (!loadedChunks.ContainsKey(chunk))
                loadedChunks[chunk] = SpawnChunk(chunk);
        }

        List<int> chunksToRemove = new List<int>();
        foreach (int chunk in loadedChunks.Keys)
        {
            if (chunk < minChunk || chunk > maxChunk)
                chunksToRemove.Add(chunk);
        }

        foreach (int chunk in chunksToRemove)
        {
            if (loadedChunks[chunk] != null)
                Destroy(loadedChunks[chunk].gameObject);

            loadedChunks.Remove(chunk);
        }
    }

    Transform SpawnChunk(int chunkIndex)
    {
        Transform prefab = chunkIndex == 0 ? initialChunkPrefab : chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        Transform chunk = Instantiate(prefab, chunksParent.transform);
        chunk.localPosition = new Vector3(0, chunkIndex * chunkSize, 0);
        return chunk;
    }

    public void PlayerHit()
    {
        EndGame();
    }

    void EndGame()
    {
        // Distance
        string distanceText = $"DISTANCE: {distanceTraveled:F1}m";
        int distanceXP = Mathf.RoundToInt(distanceTraveled * scoreXPRate); // XP based on distance
        
        string[] statTexts = new string[] { distanceText };
        int[] statXPs = new int[] { distanceXP };

        EndGame(SkillType.Riding, statTexts, statXPs);
    }
}