using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralChunk : MonoBehaviour
{
    [SerializeField] int chunkWidth = 50;
    [SerializeField] int chunkHeight = 100;

    [SerializeField] int obstacleDensity = 15; // Percentage chance for an obstacle to spawn in a cell

    [SerializeField] Tilemap obstacleTilemap;
    [SerializeField] TileBase obstacleTile;

    bool[,] obstacleMap;

    void Awake()
    {
        obstacleMap = new bool[chunkWidth, chunkHeight];
        GenerateMap(obstacleDensity);

        // Populate tilemap based on obstacleMap
        for (int x = 0; x < chunkWidth; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                if (obstacleMap[x, y])
                {
                    Vector3Int tilePos = new Vector3Int(x - chunkWidth / 2, y - chunkHeight / 2, 0);
                    obstacleTilemap.SetTile(tilePos, obstacleTile);
                }
            }
        }
    }

    void GenerateMap(int density)
    {
        for (int x = 0; x < chunkWidth; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                obstacleMap[x, y] = Random.Range(0, 100) < density;
            }
        }
    }
}