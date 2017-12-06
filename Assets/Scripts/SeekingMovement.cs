using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SeekingMovement : MonoBehaviour
{

    public float radius;

    IAstarAI ai;
    AIStateMachine machine;
    ScanForTargets scan;

    private void Start()
    {
        ai = GetComponent<IAstarAI>();
        machine = GetComponent<AIStateMachine>();
        scan = GetComponent<ScanForTargets>();

        ai.destination = GetRandomPoint();
    }

    private void Update()
    {
        if (machine.currentState == AIStateMachine.State.Flee) return;

        if (machine.currentState == AIStateMachine.State.Seek)
        {
            ai.destination = Seek();
            if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
            {
                print("End of path");
                ai.SearchPath();
            }
        }
        else if (machine.currentState == AIStateMachine.State.Wander)
        {
            if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
            {
                print("End of path");
                ai.destination = GetRandomPoint();
                ai.SearchPath();
            }
        }

    }

    private Vector2 Seek()
    {
        return scan.closestTargetPosition;
    }

    private Vector2 GetRandomPoint()
    {
        var point = Random.insideUnitCircle * radius;
        point += transform.position.AsVector2();
        return point;
    }

}
