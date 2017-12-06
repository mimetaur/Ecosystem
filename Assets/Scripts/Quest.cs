using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public GameObject goal;

    public float searchRadius;

    private Vector2 targetPosition;

    private AIStateMachine machine;

    // Use this for initialization
    void Start()
    {
        machine = GetComponent<AIStateMachine>();

        var gems = GameObject.FindGameObjectsWithTag(goal.tag);
        Search(gems);
    }

    // Update is called once per frame
    void Update()
    {
        if (machine.currentState == AIStateMachine.State.Flee) return;

        var gems = GameObject.FindGameObjectsWithTag(goal.tag);
        Search(gems);
    }

    private void Search(GameObject[] targets)
    {
        if (targets != null)
        {
            var target = GameUtils.FindClosestWithinThreshold(this.gameObject, targets, searchRadius);
            if (target != null)
            {
                targetPosition = target.transform.position.AsVector2();
                machine.currentState = AIStateMachine.State.Seek;
            }
            else
            {
                machine.currentState = AIStateMachine.State.Wander;
            }
        }
        else
        {
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
