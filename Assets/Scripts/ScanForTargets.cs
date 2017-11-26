using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TargetTag))]
public class ScanForTargets : MonoBehaviour
{
    public float threshold;

    public Vector2 closestTargetPosition;

    private AIStateMachine machine;

    private string tagName;

    private void Start()
    {
        machine = GetComponent<AIStateMachine>();
        var targetTag = GetComponent<TargetTag>();
        tagName = targetTag.tagName;
    }

    void Update()
    {
        if (machine.currentState == AIStateMachine.State.Flee) return;
        
        // wander by default, unless fleeing
        machine.currentState = AIStateMachine.State.Wander;
        

        var target = GameUtils.FindClosestWithinThreshold(this.gameObject, GameObject.FindGameObjectsWithTag(tagName), threshold);
        if (target != null)
        {
            // seek only if a target is within the threshold    
            machine.currentState = AIStateMachine.State.Seek;
            closestTargetPosition = target.transform.position;
        }
    }
}