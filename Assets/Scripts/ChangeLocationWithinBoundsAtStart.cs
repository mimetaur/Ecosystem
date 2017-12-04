using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLocationWithinBoundsAtStart : MonoBehaviour {

    public GameObject spawnArea;

	void Awake() {
		var bounds = spawnArea.GetComponent<Collider2D>().bounds;
        var point = bounds.GetRandomPoint();
        transform.position = point;
	}

}
