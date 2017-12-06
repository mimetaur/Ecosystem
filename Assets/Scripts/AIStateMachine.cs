using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine : MonoBehaviour
{

    public enum State { Idle, Wander, Seek, Flee }
    public State currentState;

    private void Start()
    {
        currentState = State.Wander;
    }
}
