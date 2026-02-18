using UnityEngine;
using System.Collections.Generic;

class RidingGame : MonoBehaviour
{
    public static RidingGame Instance { get; private set; }

    [Header("References")]
    [SerializeField] GameObject gameOverScreen;

    [Header("Prefabs")]
    [SerializeField] Transform[] chunkPrefabs;

    [Header("Chunk Settings")]
    [SerializeField] int chunkSize = 50;
    [SerializeField] int loadedDistance = 3;

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

        int currentChunk = Mathf.FloorToInt(RidingPlayer.Instance.transform.position.y / chunkSize);
        UpdateLoadedChunks(currentChunk);
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
        int yPosition = chunkIndex * chunkSize;
        return Instantiate(chunkPrefabs[randomIndex], new Vector3(0, yPosition, 0), Quaternion.identity);
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