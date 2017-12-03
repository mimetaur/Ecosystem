using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRollers : MonoBehaviour {

    public GameObject area;
    public GameObject roller;

    [Range(0, 40)]
    public int numRollers;

    [Range(0, 10)]
    public float initialDelay;

    [Range(5, 80)]
    public float rate;

    private Bounds bounds;


	// Use this for initialization
	void Start () {
        bounds = area.GetComponent<Collider2D>().bounds;

        InvokeRepeating("SpawnNewRollers", initialDelay, rate);
	}

    void SpawnNewRollers() {
        for (int i = 0; i < numRollers; i++)
        {
            Vector2 spawnPoint = bounds.GetRandomPoint();
            SpawnRollerAt(spawnPoint);
        }
    }

    void SpawnRollerAt(Vector2 location) {
        Instantiate(roller, location, Quaternion.identity);
    }
}
