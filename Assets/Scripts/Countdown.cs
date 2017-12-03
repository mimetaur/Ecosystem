using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {

    [System.Serializable]
    public class Range {
        public float minLifetime;
        public float maxLifetime;
    }

    public Range lifetime;

    private float lifecount;
    private float counter;

	// Use this for initialization
	void Awake () {
        lifecount = Random.Range(lifetime.minLifetime, lifetime.maxLifetime);
        counter = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if (counter > lifecount) {
            Destroy(this.gameObject);
        }
	}
}
