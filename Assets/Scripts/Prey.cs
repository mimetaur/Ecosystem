using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour
{

    public string predatorTag;
    public float threshold;
    private GameObject predator;
    private Flee flee;
    private AIStateMachine machine;


    // Use this for initialization
    void Start()
    {
        machine = GetComponent<AIStateMachine>();
        flee = GetComponent<Flee>();
    }

    // Update is called once per frame
    void Update()
    {
        predator = CheckForPredators();

        if (predator != null)
        {
            // flee only if creature is within threshold
            machine.currentState = AIStateMachine.State.Flee;
            flee.SetPredator(predator);
        }
        else
        {
            machine.currentState = AIStateMachine.State.Wander;
        }
    }

    GameObject CheckForPredators()
    {
        return GameUtils.FindClosestWithinThreshold(this.gameObject, GameObject.FindGameObjectsWithTag(predatorTag), threshold);
    }
}
