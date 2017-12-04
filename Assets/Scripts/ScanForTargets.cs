using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TargetTag))]
public class ScanForTargets : MonoBehaviour
{
    public float threshold;

    public Vector2 closestTargetPosition;

    private AIStateMachine machine;

    private TargetTag targets;

    private void Start()
    {
        machine = GetComponent<AIStateMachine>();
        targets = GetComponent<TargetTag>();
    }

    void Update()
    {
        if (machine.currentState == AIStateMachine.State.Flee) return;
        
        // wander by default, unless fleeing
        machine.currentState = AIStateMachine.State.Wander;
        

        var target = GameUtils.FindClosestWithinThreshold(this.gameObject, targets.AllTargets(), threshold);
        if (target != null)
        {
            // seek only if a target is within the threshold    
            machine.currentState = AIStateMachine.State.Seek;
            closestTargetPosition = target.transform.position;
        }
    }
}