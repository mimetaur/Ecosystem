using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MoveWeeper : MonoBehaviour
{

    public float radius;
    public Vector2 target;

    IAstarAI ai;
    AIStateMachine machine;


    private void Start()
    {
        ai = GetComponent<IAstarAI>();
        machine = GetComponent<AIStateMachine>();

        ai.destination = PickRandomPoint();
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
