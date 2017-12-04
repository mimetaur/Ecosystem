using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public GameObject goal;

    private Vector2 targetPosition;

    private AIStateMachine machine;

    // Use this for initialization
    void Start()
    {
        machine = GetComponent<AIStateMachine>();
        goal = GameObject.Find("Gem");
    }

    // Update is called once per frame
    void Update()
    {
        if (machine.currentState == AIStateMachine.State.Flee) return;

        if (goal != null) {
            targetPosition = goal.transform.position.AsVector2();
            machine.currentState = AIStateMachine.State.Seek;
        } else {
            machine.currentState = AIStateMachine.State.Wander;
        }
    }

    public Vector2 TargetPosition()
    {
        return targetPosition;
    }
}
