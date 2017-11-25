using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Wander : MonoBehaviour
{

    public float radius;

    private Seeker seeker;

    //The calculated path
    public Path path;

    //The AI's speed per second
    public float speed = 2;

    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;

    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    private Vector2 targetPosition;

    // Use this for initialization
    void Start()
    {
        seeker = GetComponent<Seeker>();
        targetPosition = PickRandomPoint();
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
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

    public void FixedUpdate()
    {
        if (path == null)
        {
            //We have no path to move after yet
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            Debug.Log("End Of Path Reached");
            targetPosition = PickRandomPoint();
            seeker.StartPath(transform.position, targetPosition, OnPathComplete);
            return;
        }

        Move();

        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }

    private void Move()
    {
        //Direction to the next waypoint
        Vector2 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        dir *= speed * Time.fixedDeltaTime;
        this.gameObject.transform.Translate(dir);
    }


    Vector2 PickRandomPoint()
    {
        var point = Random.insideUnitCircle * radius;
        point += transform.position.AsVector2();
        return point;
    }
}