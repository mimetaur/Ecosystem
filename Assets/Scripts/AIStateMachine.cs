using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine : MonoBehaviour {

    enum State { Idle, Wander, Seek }
    State currentState;

    private void Start()
    {
        currentState = State.Wander;
    }
}
