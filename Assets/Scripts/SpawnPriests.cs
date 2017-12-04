using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpawnPriests : MonoBehaviour
{

    public GameObject characterPrefab;
    public GameObject spawnArea;

    private int numCreatures = 0;
    private Bounds bounds;

    // Use this for initialization
    void Start()
    {
        bounds = spawnArea.GetComponent<Collider2D>().bounds;

        SpawnNewPriest();
    }

    void SpawnNewPriest()
    {
        Vector2 spawnPoint = bounds.GetRandomPoint();
        SpawnPriestAt(spawnPoint);
    }


    void SpawnPriestAt(Vector2 location)
    {
        Instantiate(characterPrefab, location, Quaternion.identity);
    }
}
