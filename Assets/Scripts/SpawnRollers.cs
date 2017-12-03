using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRollers : MonoBehaviour
{

    public GameObject spawnArea;
    public GameObject roller;

    [Range(0, 40)]
    public int numRollers;

    [Range(0, 10)]
    public float initialDelay;

    [Range(5, 80)]
    public float rate;

    private BoxCollider2D spawnCollider;

    void Start()
    {
        spawnCollider = spawnArea.GetComponent<BoxCollider2D>();
        InvokeRepeating("SpawnNewRollers", 0, 2.0f);
    }

    void SpawnNewRollers()
    {
        print("Spawning new rollers");
        var bounds = spawnCollider.bounds;
        for (int i = 0; i < numRollers; i++)
        {
            Vector2 spawnPoint = bounds.GetRandomPoint();
            SpawnRollerAt(spawnPoint);
        }
    }

    void SpawnRollerAt(Vector2 location)
    {
        Instantiate(roller, location, Quaternion.identity);
    }
}