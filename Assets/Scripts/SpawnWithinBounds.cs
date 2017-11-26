using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpawnWithinBounds : MonoBehaviour
{

    [Range(0, 20)]
    public int maxCreatures;
    public GameObject creaturePrefab;
    public GameObject area;
    public float initialDelay;
    public float rate;

    private int numCreatures = 0;
    private Bounds bounds;

    // Use this for initialization
    void Start()
    {
        bounds = area.GetComponent<BoxCollider2D>().bounds;
        InvokeRepeating("SpawnNewCreature", initialDelay, rate);
    }

    void SpawnNewCreature() {
        if (numCreatures > maxCreatures) return;

        Vector2 spawnPoint = bounds.GetRandomPoint();
        SpawnCreatureAt(spawnPoint);
    }


    void SpawnCreatureAt(Vector2 location)
    {
        numCreatures++;
        var newCreature = Instantiate(creaturePrefab, location, Quaternion.identity) as GameObject;
        newCreature.name = string.Format("{0}{1}", creaturePrefab.name, newCreature.GetInstanceID());

        var ai = newCreature.GetComponent<IAstarAI>();
        ai.maxSpeed = Random.Range(1.0f, 3.0f);
    }
}
