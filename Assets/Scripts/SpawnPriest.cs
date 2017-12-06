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

    public void SpawnNewPriest()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length < 1)
        {
            Vector2 spawnPoint = bounds.GetRandomPoint();
            SpawnPriestAt(spawnPoint);
        }

    }

    public void RespawnPriest()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            SpawnNewPriest();
        }
        else
        {
            player.transform.position = bounds.GetRandomPoint();
            player.SetActive(true);
        }
    }

    public void SpawnPriestAt(Vector2 location)
    {
        Instantiate(priest, location, Quaternion.identity);
    }
}
