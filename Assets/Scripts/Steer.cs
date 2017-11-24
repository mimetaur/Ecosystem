using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steer : MonoBehaviour
{

    public Vector2 target;
    private Rigidbody2D rb2d;
    private float maxSpeed;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        maxSpeed = Random.Range(1.5f, 4.0f);
        target = new Vector2(-20, transform.position.y);

        //float lowerLimit = -0.25f;
        //float upperLimit = -0.75f;
        //float xAccel = Random.Range(lowerLimit, upperLimit);
        //acceleration = new Vector2(xAccel, 0.0f);
        //rotationAcceleration = GameUtils.Map(xAccel, lowerLimit, upperLimit, 0.05f, 0.15f);
    }

    private Vector2 Seek()
    {
        Vector2 position = transform.position.AsVector2();
        Vector2 desired = target - position;
        desired.Normalize();
        desired *= maxSpeed;

        Vector2 steer = desired - rb2d.velocity;
        steer.Normalize();
        steer *= maxSpeed;
        return steer;
    }

    private void FixedUpdate()
    {
        var steer = Seek();
        rb2d.AddForce(steer);
        rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);

        rb2d.angularVelocity = 125.0f;

        //rb2d.AddForce(acceleration);
        //rb2d.AddTorque(rotationAcceleration);
    }
}


