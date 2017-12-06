using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvadeManager : MonoBehaviour
{

    public GameObject predatorType;
    public float threshold;
    private GameObject predator;

    private QuestEvadeMovement evasion;

    private QuestAIStateMachine machine;

    // Use this for initialization
    void Start()
    {
        machine = GetComponent<QuestAIStateMachine>();
        evasion = GetComponent<QuestEvadeMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        predator = CheckForPredators();

        if (predator != null)
        {
            // flee only if creature is within threshold
            machine.CurrentState = QuestAIStateMachine.State.Evade;
            evasion.SetPredator(predator);
        }
        else
        {
            machine.ReturnToPreviousState();
        }
    }

    GameObject CheckForPredators()
    {
        return GameUtils.FindClosestWithinThreshold(this.gameObject, GameObject.FindGameObjectsWithTag(predatorType.tag), threshold);
    }
}
