using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerAnim : MonoBehaviour
{

    QuestAIStateMachine machine;
    Animator anim;
    SpriteRenderer sr;

    IAstarAI ai;
    // Use this for initialization
    void Start()
    {
        machine = GetComponent<QuestAIStateMachine>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        ai = GetComponent<IAstarAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (machine.CurrentState == QuestAIStateMachine.State.Idle)
        {
            anim.enabled = false;
        }
        else
        {
            anim.enabled = true;
            if (transform.position.x > ai.destination.x)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
    }
}
