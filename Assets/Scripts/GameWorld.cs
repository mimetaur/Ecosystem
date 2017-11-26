using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameWorld : MonoBehaviour
{

    public GameObject ground;
    public GameObject obstacles;

    private Tilemap groundTilemap;
    private Tilemap obstaclesTilemap;

    private List<Vector3Int> allCells = new List<Vector3Int>();
    private List<Vector3Int> unblockedCells = new List<Vector3Int>();

    // Use this for initialization
    void Start()
    {
        groundTilemap = ground.GetComponent<Tilemap>();
        obstaclesTilemap = obstacles.GetComponent<Tilemap>();

        ScanTilemaps();
    }

    private void ScanTilemaps()
    {
        var cellBounds = groundTilemap.cellBounds;
        for (int x = cellBounds.min.x; x < cellBounds.max.x; x++)
        {
            for (int y = cellBounds.min.y; y < cellBounds.max.y; y++)
            {
                var v = new Vector3Int(x, y, 0);
                allCells.Add(v);
                if (obstaclesTilemap.GetTile(v) == null)
                    unblockedCells.Add(v);
            }
        }
    }

    private Vector3Int GetRandomCell()
    {
        int i = Random.Range(0, allCells.Count);
        return allCells[i];
    }

    private Vector3Int GetRandomUnblockedCell()
    {
        int i = Random.Range(0, unblockedCells.Count);
        return unblockedCells[i];
    }

    public Vector2 GetRandomLocation()
    {
        return groundTilemap.GetCellCenterWorld(GetRandomCell());
    }

    public Vector2 GetRandomUnblockedLocation()
    {

        return groundTilemap.GetCellCenterWorld(GetRandomUnblockedCell());
    }


}
