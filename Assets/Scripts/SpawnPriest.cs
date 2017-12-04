using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpawnPriest : MonoBehaviour
{
    public GameObject priest;
    public GameObject spawnArea;

    private Bounds bounds;

    // Use this for initialization
    void Awake()
    {
        bounds = spawnArea.GetComponent<Collider2D>().bounds;

        SpawnNewPriest();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length < 1)
        {
            Invoke("SpawnNewPriest", 5.0f);
        }
    }

    void SpawnNewPriest()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length < 1)
        {
            Vector2 spawnPoint = bounds.GetRandomPoint();
            SpawnPriestAt(spawnPoint);
        }

    }

    void SpawnPriestAt(Vector2 location)
    {
        Instantiate(priest, location, Quaternion.identity);
    }
}
