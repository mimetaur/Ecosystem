using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steer : MonoBehaviour {

    Rigidbody2D rb2d;
    Vector2 acceleration;
    float rotationAcceleration;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();

        float lowerLimit = -0.25f;
        float upperLimit = -0.75f;
        float xAccel = Random.Range(lowerLimit, upperLimit);
        acceleration = new Vector2(xAccel, 0.0f);
        rotationAcceleration = GameUtils.Map(xAccel, lowerLimit, upperLimit, 0.05f, 0.15f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb2d.AddForce(acceleration);
        rb2d.AddTorque(rotationAcceleration);
	}
}


