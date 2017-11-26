using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(TargetTag))]
public class EatPickup : MonoBehaviour
{
    private string tagName;
    private AIStateMachine machine;

    private void Start()
    {
        machine = GetComponent<AIStateMachine>();
        var targetTag = GetComponent<TargetTag>();
        tagName = targetTag.tagName;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == tagName)
        {
            machine.currentState = AIStateMachine.State.Wander;
            string debug = string.Format("Ate {0} at {1}", tagName, other.transform.position.AsVector2());
            print(debug);
            Destroy(other.gameObject);
        }
    }
}
