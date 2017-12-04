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
        tagName = GetComponent<TargetTag>().tagName;
    }

    void Update()
    {
        if (machine.currentState == AIStateMachine.State.Flee) return;

        // wander by default, unless fleeing
        machine.currentState = AIStateMachine.State.Wander;

        //var targets = GameObject.FindGameObjectsWithTag(tagName);
        var targets = new List<GameObject>();
        targets.AddRange( GameObject.FindGameObjectsWithTag(tagName) );

        if (this.gameObject.tag == "Enemy")
        {
            targets.Add(GameObject.FindGameObjectWithTag("Player"));
        }

        var target = GameUtils.FindClosestWithinThreshold(this.gameObject, targets.ToArray(), threshold);
        if (target != null)
        {
            // seek only if a target is within the threshold    
            machine.currentState = AIStateMachine.State.Seek;
            closestTargetPosition = target.transform.position;
        }
    }
}