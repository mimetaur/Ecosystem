using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class SpawnSeekingCreature : MonoBehaviour
{

    [Range(0, 20)]
    public int numCreatures;

    public GameObject creaturePrefab;

    private Tilemap groundTilemap;
    private Tilemap obstaclesTilemap;

    private List<Vector3Int> validCells = new List<Vector3Int>();


    // Use this for initialization
    void Start()
    {
        groundTilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        obstaclesTilemap = GameObject.Find("Obstacles").GetComponent<Tilemap>();

        ScanTilemaps();

        for (int i = 0; i < numCreatures; i++)
        {
            SpawnNewCreature();
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

    private void SpawnNewCreature()
    {
        int i = Random.Range(0, validCells.Count);
        var cell = validCells[i];

        SpawnCreatureAt(groundTilemap.GetCellCenterWorld(cell));
    }

    void SpawnCreatureAt(Vector2 location)
    {
        var newWeeper = Instantiate(creaturePrefab, location, Quaternion.identity) as GameObject;
        var ai = newWeeper.GetComponent<IAstarAI>();
        ai.maxSpeed = Random.Range(1.0f, 3.0f);
    }
}
