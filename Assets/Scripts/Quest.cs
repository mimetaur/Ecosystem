using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public GameObject goal;

    private Vector2 targetPosition;

    private AIStateMachine machine;

    // Use this for initialization
    void Awake()
    {
        machine = GetComponent<AIStateMachine>();
        goal = GameObject.FindGameObjectWithTag("QuestGoal");

        if (goal != null)
        {
            targetPosition = goal.transform.position.AsVector2();
            machine.currentState = AIStateMachine.State.Seek;
        }
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



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "QuestGoal")
        {
            goal.transform.position = GameManager.instance.world.GetRandomUnblockedLocation();
            machine.currentState = AIStateMachine.State.Seek;
        }
    }
}
