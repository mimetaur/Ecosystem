using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRollers : MonoBehaviour {


    public GameObject spawnArea;
    public GameObject roller;
    public int numRollers;


	// Use this for initialization
	void Start () {
        var spawnCollider = spawnArea.GetComponent<BoxCollider2D>();
        var bounds = spawnCollider.bounds;
        for (int i = 0; i < numRollers; i++) {
            Vector2 spawnPoint = bounds.GetRandomPoint();
            SpawnRollerAt(spawnPoint);          
        }
	}

    void SpawnRollerAt(Vector2 location) {
        var rollersContainer = GameObject.Find("Rollers");
        var newRoller = Instantiate(roller, location, Quaternion.identity) as GameObject;
        newRoller.transform.parent = rollersContainer.transform;
    }
}
