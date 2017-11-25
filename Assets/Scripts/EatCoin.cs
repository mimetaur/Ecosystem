using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EatCoin : MonoBehaviour {

    AIStateMachine machine;

    private void Start()
    {
        machine = GetComponent<AIStateMachine>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pickup")
        {
            machine.currentState = AIStateMachine.State.Wander;
            print("Ate coin at: " + other.transform.position.AsVector2());
            Destroy(other.gameObject);
        }
    }
}
