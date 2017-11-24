using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnMushrooms : MonoBehaviour
{
    [Range(1, 100)]
    public int numMushrooms = 12;

    public Transform mushroom;

    private Tilemap groundTilemap;
    private Tilemap obstaclesTilemap;

    private List<Vector3Int> validCells = new List<Vector3Int>();


    void Start()
    {
        groundTilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        obstaclesTilemap = GameObject.Find("Obstacles").GetComponent<Tilemap>();

        ScanTilemaps();

        for (int i = 0; i < numMushrooms; i++)
        {
            AddMushroom();
        }
    }

    public void ScanTilemaps()
    {
        var groundTilemapBounds = groundTilemap.cellBounds;
        for (int x = groundTilemapBounds.min.x; x < groundTilemapBounds.max.x; x++)
        {
            for (int y = groundTilemapBounds.min.y; y < groundTilemapBounds.max.y; y++)
            {
                var v = new Vector3Int(x, y, 0);
                if (obstaclesTilemap.GetTile(v) == null)
                {
                    validCells.Add(v);
                }
            }
        }
    }

    private void AddMushroom()
    {
        int i = Random.Range(0, validCells.Count);
        var cell = validCells[i];
        var location = groundTilemap.GetCellCenterWorld(cell);
        Instantiate(mushroom, location, Quaternion.identity);
    }
}

