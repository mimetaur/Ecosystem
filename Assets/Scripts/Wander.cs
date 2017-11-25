using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Wander : MonoBehaviour
{

    public float radius;
    IAstarAI ai;


    private void Start()
    {
        ai = GetComponent<IAstarAI>();
    }

    private void Update()
    {
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
        }
    }


    private Vector2 PickRandomPoint()
    {
        var point = Random.insideUnitCircle * radius;
        point += transform.position.AsVector2();
        return point;
    }
}