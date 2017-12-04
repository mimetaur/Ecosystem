using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGems : MonoBehaviour
{
    public GameObject gem;
    public GameObject[] spawnAreas;

    // Use this for initialization
    void Start()
    {
        SpawnNewGem();

    }

    public void SpawnNewGem()
    {
        int randomIdx = Random.Range(0, spawnAreas.Length);
        var bounds = spawnAreas[randomIdx].GetComponent<Collider2D>().bounds;
        var spawnPoint = bounds.GetRandomPoint();
    }

    public void SpawnGemAt(Vector2 spawnPoint)
    {
        Instantiate(gem, spawnPoint, Quaternion.identity);
    }
}
