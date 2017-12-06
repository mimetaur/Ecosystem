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
    public void Initialize()
    {
        groundTilemap = ground.GetComponent<Tilemap>();
        groundTilemap.CompressBounds();
        obstaclesTilemap = obstacles.GetComponent<Tilemap>();
        obstaclesTilemap.CompressBounds();

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

    private Vector3Int GetPaddedRandomCell(int padding)
    {
        var paddedCells = new List<Vector3Int>();
        var cellBounds = groundTilemap.cellBounds;

        for (int x = (cellBounds.min.x + padding); x < (cellBounds.max.x - padding); x++)
        {
            for (int y = (cellBounds.min.y + padding); y < (cellBounds.max.y - padding); y++)
            {
                var v = new Vector3Int(x, y, 0);
                paddedCells.Add(v);
            }
        }

        int i = Random.Range(0, paddedCells.Count);
        return paddedCells[i];
    }

    public Vector2 GetPaddedRandomLocation(int padding)
    {
        return groundTilemap.GetCellCenterWorld(GetPaddedRandomCell(padding));
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
