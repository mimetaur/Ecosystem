using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class QuestMovement : MonoBehaviour
{
    private IAstarAI ai;
    private QuestAIStateMachine machine;
    private Quest quest;

    private void Start()
    {
        ai = GetComponent<IAstarAI>();
        machine = GetComponent<QuestAIStateMachine>();
        quest = GetComponent<Quest>();
    }

    private void Update()
    {
        if (machine.CurrentState == QuestAIStateMachine.State.Evade)
        {
            return;
        }
        else if (machine.CurrentState == QuestAIStateMachine.State.Wander)
        {
            Wander();
        }
        else
        {
            // can either seek home or seek gems
            Seek();
        }
    }

    private void Wander()
    {
        // only look for a new destination 
        // once you've reached the old one
        if (isLookingForNewPath())
        {
            ai.destination = quest.TargetPosition;
            ai.SearchPath();
        }
    }

    private void Seek()
    {
        // look for a new destination each frame
        ai.destination = quest.TargetPosition;

        if (isLookingForNewPath())
        {
            ai.destination = quest.TargetPosition;
            ai.SearchPath();
        }
    }

    private bool isLookingForNewPath()
    {
        return !ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath);
    }
}
