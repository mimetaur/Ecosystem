using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Flee : MonoBehaviour
{
    private FleeSounds fleeSounds;
    private AIStateMachine machine;
    private GameObject predator;
    IAstarAI ai;

    private GameObject exclamationMark;

    public float rate;
    // Use this for initialization
    void Start()
    {
        ai = GetComponent<IAstarAI>();
        machine = GetComponent<AIStateMachine>();
        predator = null;
        fleeSounds = GetComponent<FleeSounds>();

        exclamationMark = (GameObject)Instantiate(Resources.Load("Exclamation Mark"));
        exclamationMark.transform.parent = this.transform;
        exclamationMark.SetActive(false);
        InvokeRepeating("CheckIfFleeing", 0.0f, rate);
    }

    void CheckIfFleeing()
    {
        if (machine.currentState != AIStateMachine.State.Flee || predator == null)
        {
            return;
        }
        else
        {
            if (fleeSounds != null) fleeSounds.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (machine.currentState != AIStateMachine.State.Flee || predator == null)
        {
            exclamationMark.SetActive(false);
            //if (fleeSounds != null) fleeSounds.Stop();
            return;
        }

        //if (fleeSounds != null) fleeSounds.Play();

        exclamationMark.SetActive(true);
        exclamationMark.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

        ai.destination = FleePredator();
        ai.SearchPath();

    }

    private Vector2 FleePredator()
    {
        float desiredDistance = 10.0f;
        var point = predator.transform.position + (transform.position - predator.transform.position).normalized * desiredDistance;

        return point;
    }

    public void SetPredator(GameObject newPredator)
    {
        predator = newPredator;
    }
}
