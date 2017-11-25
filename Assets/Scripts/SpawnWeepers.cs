using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class SpawnWeepers : MonoBehaviour
{

    [Range(0, 20)]
    public int numWeepers;

    public GameObject weeper;

    private Tilemap groundTilemap;
    private Tilemap obstaclesTilemap;

    private List<Vector3Int> validCells = new List<Vector3Int>();


    // Use this for initialization
    void Start()
    {
        groundTilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        obstaclesTilemap = GameObject.Find("Obstacles").GetComponent<Tilemap>();

        ScanTilemaps();

        for (int i = 0; i < numWeepers; i++) {
            SpawnNewWeeper();
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

    private void SpawnNewWeeper()
    {
        int i = Random.Range(0, validCells.Count);
        var cell = validCells[i];

        SpawnWeeperAt(groundTilemap.GetCellCenterWorld(cell));
    }

    void SpawnWeeperAt(Vector2 location)
    {
        var weepersContainer = GameObject.Find("Weepers");
        var newWeeper = Instantiate(weeper, location, Quaternion.identity) as GameObject;
        newWeeper.transform.parent = weepersContainer.transform;

        var ai = newWeeper.GetComponent<IAstarAI>();
        ai.maxSpeed = Random.Range(1.0f, 3.0f);
    }
}
