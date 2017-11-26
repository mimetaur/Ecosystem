using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Flee : MonoBehaviour
{
    private AIStateMachine machine;
    private GameObject predator;
    IAstarAI ai;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        ai = GetComponent<IAstarAI>();
        machine = GetComponent<AIStateMachine>();
        predator = null;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (machine.currentState != AIStateMachine.State.Flee || predator == null) {
            spriteRenderer.color = Color.white;
            return;  
        } 
        spriteRenderer.color = Color.red;


        ai.destination = FleePredator();
        ai.SearchPath();

        //if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        //{
        //    ai.destination = FleePredator();
        //    ai.SearchPath();
        //}

    }

    private Vector2 FleePredator()
    {
        //var pos = predator.transform.position.AsVector2();
        //var dir = pos.normalized;
        //var oppDir = pos *= -1;
        float desiredDistance = 10.0f;
        var point = predator.transform.position + (transform.position - predator.transform.position).normalized * desiredDistance;

        //var point = oppDir * 12.0f;
        //point += transform.position.AsVector2();

        return point;
    }

    public void SetPredator(GameObject newPredator)
    {
        predator = newPredator;
    }
}
