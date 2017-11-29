using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Flee : MonoBehaviour
{
    private AIStateMachine machine;
    private GameObject predator;
    IAstarAI ai;

    private GameObject exclamationMark;

    // Use this for initialization
    void Start()
    {
        ai = GetComponent<IAstarAI>();
        machine = GetComponent<AIStateMachine>();
        predator = null;

        exclamationMark = (GameObject)Instantiate(Resources.Load("Exclamation Mark"));
        exclamationMark.transform.parent = this.transform;
        //exclamationMark.transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
        exclamationMark.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (machine.currentState != AIStateMachine.State.Flee || predator == null)
        {
            //spriteRenderer.color = Color.white;
            exclamationMark.SetActive(false);
            return;
        }
        exclamationMark.SetActive(true);
        exclamationMark.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

        //spriteRenderer.color = Color.red;

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
