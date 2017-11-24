using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Steer : MonoBehaviour
{

    public Vector2 target;

    public float maxSeeAhead = 10.0f;

    private Rigidbody2D rb2d;

    private float maxSpeed;
    private float maxAvoidForce;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();


        // set up a target
        maxSpeed = Random.Range(2.5f, 5.0f);
        float posY = transform.position.y;
        float destY = Random.Range(posY - 4, posY + 4);
        target = new Vector2(-20, destY);
        maxAvoidForce = 4.0f;
    }



    private Vector2 Seek(Vector2 targetPosition)
    {
        var position = transform.position.AsVector2();


        Vector2 desired = targetPosition - position;
        desired.Normalize();
        desired *= maxSpeed;

        Vector2 steer = desired - rb2d.velocity;
        steer.Normalize();

        steer *= maxSpeed;
        return steer;
    }

    private Vector2 Avoid()
    {
        Vector2 position = transform.position.AsVector2();

        //RaycastHit2D
        RaycastHit2D hit = Physics2D.Raycast(position, rb2d.velocity.normalized, maxSeeAhead);

        // avoidance_force = ahead - obstacle_center

        Vector2 ahead = position + rb2d.velocity;

        // by default, no avoidance
        Vector2 force = new Vector2(0, 0);
        if (hit.collider != null)
        {
            print("About to collide" + hit.fraction);
            Vector2 obstacle = hit.collider.transform.position;
            force = ((ahead - obstacle).normalized) * maxAvoidForce;
        }

        return force;
    }

    private void FixedUpdate()
    {
        var seek = Seek(target);
        rb2d.AddForce(seek);

        //var avoid = Avoid();

        //rb2d.AddForce(avoid);

        rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);

        rb2d.angularVelocity = maxSpeed.Map(2.5f, 5.0f, 100.0f, 150.0f);
        //transform.Rotate( new Vector3(0, 0 1.0f) );
    }
}


