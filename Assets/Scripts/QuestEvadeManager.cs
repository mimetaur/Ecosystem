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

    private Quest quest;

    // Use this for initialization
    void Start()
    {
        machine = GetComponent<QuestAIStateMachine>();
        evasion = GetComponent<QuestEvadeMovement>();
        quest = GetComponent<Quest>();
    }

    // Update is called once per frame
    void Update()
    {
        predator = CheckForPredators();

        if (predator != null && machine.CurrentState != QuestAIStateMachine.State.Evade)
        {
            bool isClose = Vector2.Distance(predator.transform.position, transform.position) < threshold;
            if (isClose)
            {
                // flee only if creature is within threshold
                machine.CurrentState = QuestAIStateMachine.State.Evade;
                evasion.SetPredator(predator);
            }
            else
            {
                machine.CurrentState = QuestAIStateMachine.State.Wander;
                predator = null;
                evasion.SetPredator(null);
            }

        }
        else if (predator == null && machine.CurrentState == QuestAIStateMachine.State.Evade)
        {
            evasion.SetPredator(null);
            machine.CurrentState = QuestAIStateMachine.State.Wander;
        }
    }

    GameObject CheckForPredators()
    {
        return GameUtils.FindClosestWithinThreshold(this.gameObject, GameObject.FindGameObjectsWithTag(predatorType.tag), threshold);
    }
}
