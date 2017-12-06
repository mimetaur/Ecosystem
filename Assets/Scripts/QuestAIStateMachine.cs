using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestAIStateMachine : MonoBehaviour
{
    public enum State { Idle, Wander, Seek, Flee, ReturnHomeWithGem }

    private State currentState;


    public State CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;
        }
    }

    private void Start()
    {
        currentState = State.Wander;
    }
}
