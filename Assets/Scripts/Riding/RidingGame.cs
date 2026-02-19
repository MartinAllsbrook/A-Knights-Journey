using UnityEngine;
using System.Collections.Generic;

class RidingGame : MonoBehaviour
{
    public static RidingGame Instance { get; private set; }

    [Header("References")]
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Rigidbody2D chunksParent;
    [Header("Prefabs")]
    [SerializeField] Transform[] chunkPrefabs;

    [Header("Chunk Settings")]
    [SerializeField] int chunkSize = 50;
    [SerializeField] int loadedDistance = 3;

    [Header("Speed Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float moveSpeedIncreaseRate = 0.05f;

    public float MoveSpeed => moveSpeed;

    readonly Dictionary<int, Transform> loadedChunks = new Dictionary<int, Transform>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (RidingPlayer.Instance == null)
            return;

        moveSpeed += moveSpeedIncreaseRate * Time.deltaTime;

        float playerY = RidingPlayer.Instance.transform.position.y;
        int currentChunk = Mathf.FloorToInt((playerY - chunksParent.position.y) / chunkSize);
        UpdateLoadedChunks(currentChunk);
    }

    void FixedUpdate()
    {
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
        int randomIndex = Random.Range(0, chunkPrefabs.Length);
        Transform chunk = Instantiate(chunkPrefabs[randomIndex], chunksParent.transform);
        chunk.localPosition = new Vector3(0, chunkIndex * chunkSize, 0);
        return chunk;
    }

    public void PlayerHit()
    {
        GameOver();
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }
}