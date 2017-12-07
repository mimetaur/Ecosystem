using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{

    QuestAIStateMachine machine;

    Animator anim;
    // Use this for initialization
    void Start()
    {
        machine = GetComponent<QuestAIStateMachine>();
        anim = GetComponent<Animator>();
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
        }
    }
}
