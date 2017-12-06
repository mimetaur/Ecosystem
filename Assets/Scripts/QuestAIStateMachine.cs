using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestAIStateMachine : MonoBehaviour
{
    public enum State { Idle, Wander, Seek, Evade, ReturnHomeWithGem }

    private State currentState;
    private State previousState;


    public State CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            previousState = currentState;
            currentState = value;
        }
    }

    public State PreviousState
    {
        get
        {
            return previousState;
        }
    }

    public void ReturnToPreviousState()
    {
        currentState = previousState;
    }

    private void Start()
    {
        currentState = State.Wander;
        previousState = State.Wander;
    }
}
