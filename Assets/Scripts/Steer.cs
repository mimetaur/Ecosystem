using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Steer : MonoBehaviour
{

    public Vector2 target;

    //The AI's speed per second
    public float speed = 2;

    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;

    //The calculated path
    public Path path;

    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    private Seeker seeker;

    private FindFood foodFinder;

    private Rigidbody2D rb2d;

    private float maxSpeed;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();


        // set up a target
        maxSpeed = Random.Range(1.5f, 4.0f);
        float posY = transform.position.y;
        float destY = Random.Range(posY - 4, posY + 4);
        target = new Vector2(-20, destY);

        // find target
        FindTarget();


        //float lowerLimit = -0.25f;
        //float upperLimit = -0.75f;
        //float xAccel = Random.Range(lowerLimit, upperLimit);
        //acceleration = new Vector2(xAccel, 0.0f);
        //rotationAcceleration = GameUtils.Map(xAccel, lowerLimit, upperLimit, 0.05f, 0.15f);
    }

    private void FindTarget() {
        seeker.StartPath(transform.position, target, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
        if (!p.error)
        {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
    }

    private Vector2 Seek(Vector2 currentTarget)
    {
        Vector2 position = transform.position.AsVector2();
        Vector2 desired = target - position;
        desired.Normalize();
        desired *= maxSpeed;

        Vector2 steer = desired - rb2d.velocity;
        steer.Normalize();
        //print("Unsnapped" + steer);
        //steer = GameUtils.SnapTo(steer, 45);
        //print("Snapped" + steer);

        steer *= maxSpeed;
        return steer;
    }

    private void FixedUpdate()
    {
        CheckPathExists();
        CheckEndOfPath();
        Move();
        CheckWaypoint();
    }

    private void CheckPathExists() {
        if (path == null)
        {
            //We have no path to move after yet
            FindTarget();
            return;
        }
    }

    private void CheckEndOfPath() {
        if (currentWaypoint >= path.vectorPath.Count)
        {
            Debug.Log("End Of Path Reached");

            // set current position to target
            target = transform.position;
            FindTarget();
            return;
        }
    }

    private void Move() {

        //Direction to the next waypoint
        Vector2 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        //dir *= speed * Time.fixedDeltaTime;
        //this.gameObject.transform.Translate(dir);

        var steer = Seek(dir);
        rb2d.AddForce(steer);
        rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);

        rb2d.angularVelocity = 125.0f;
    }

    private void CheckWaypoint() {
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }

    }
}


