using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class QuestMovement : MonoBehaviour
{

    public float wanderRadius;

    IAstarAI ai;
    AIStateMachine machine;
    Quest quest;

    private void Start()
    {
        ai = GetComponent<IAstarAI>();
        machine = GetComponent<AIStateMachine>();
        quest = GetComponent<Quest>();

        machine.currentState = AIStateMachine.State.Seek;
        ai.destination = Seek();
    }

    private void Update()
    {
        if (machine.currentState == AIStateMachine.State.Flee) return;

        if (machine.currentState == AIStateMachine.State.Seek)
        {
            ai.destination = Seek();
            print("Moving toward quest object");

            if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
            {
                ai.destination = Seek();
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
        return quest.TargetPosition();
    }

    private Vector2 GetRandomPoint()
    {
        var point = Random.insideUnitCircle * wanderRadius;
        point += transform.position.AsVector2();
        return point;
    }

}
