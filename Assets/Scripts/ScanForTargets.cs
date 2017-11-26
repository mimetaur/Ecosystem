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
        FindClosestTargetWithinThreshold();
    }

    private void FindClosestTargetWithinThreshold() {
        // wander by default
        machine.currentState = AIStateMachine.State.Wander;
        var targets = GameObject.FindGameObjectsWithTag(tagName);
        var closestTarget = GetClosestTarget(targets);
        if (closestTarget != null)
        {
            var targetPos = closestTarget.transform.position.AsVector2();
            var myPos = transform.position.AsVector2();

            if (Vector2.Distance(targetPos, myPos) < threshold)
            {
                // seek only if a target is within the threshold
                machine.currentState = AIStateMachine.State.Seek;
                closestTargetPosition = targetPos;
            }
        }
    }

    // Example algorithm sourced from:
    // https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    private GameObject GetClosestTarget(GameObject[] things)
    {
        GameObject closestThing = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject thing in things)
        {
            Vector3 diff = thing.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestThing = thing;
                distance = curDistance;
            }
        }
        return closestThing;
    }
}
