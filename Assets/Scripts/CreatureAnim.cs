using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CreatureAnim : MonoBehaviour
{

    AIStateMachine machine;
    Animator anim;
    SpriteRenderer sr;

    IAstarAI ai;
    // Use this for initialization
    void Start()
    {
        machine = GetComponent<AIStateMachine>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        ai = GetComponent<IAstarAI>();

        InvokeRepeating("ChangeFacing", 0.0f, 1.5f);
    }

    // Update is called once per frame
    void ChangeFacing()
    {
        if (machine.currentState == AIStateMachine.State.Idle)
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
