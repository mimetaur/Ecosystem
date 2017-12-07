using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class QuestEvadeMovement : MonoBehaviour
{
    private FleeSounds fleeSounds;
    private QuestAIStateMachine machine;
    private GameObject predator;
    IAstarAI ai;

    private GameObject exclamationMark;

    public float rate;

    [Range(0.5f, 2.0f)]
    public float fleeingMultiplier;

    private Vector2 homePosition;

    void Start()
    {
        ai = GetComponent<IAstarAI>();
        machine = GetComponent<QuestAIStateMachine>();
        predator = null;
        fleeSounds = GetComponent<FleeSounds>();

        homePosition = GameObject.Find("Priest Spawn").GetComponent<Collider2D>().bounds.center.AsVector2();

        exclamationMark = (GameObject)Instantiate(Resources.Load("Exclamation Mark"));
        exclamationMark.transform.parent = this.transform;
        exclamationMark.SetActive(false);
        exclamationMark.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);
        InvokeRepeating("CheckIfEvading", 0.0f, rate);
    }

    void CheckIfEvading()
    {
        if (IsNotEvading())
        {
            return;
        }
        else
        {
            if (fleeSounds != null) fleeSounds.Play();
        }
    }

    void Update()
    {
        if (IsNotEvading())
        {
            DisableMark();
            return;
        }

        EnableMark();

        ai.destination = FleePredator();
        ai.SearchPath();

    }

    private void EnableMark()
    {
        exclamationMark.SetActive(true);
        exclamationMark.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        exclamationMark.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);
    }

    private void DisableMark()
    {
        exclamationMark.SetActive(false);
    }

    private bool IsNotEvading()
    {
        return machine.CurrentState != QuestAIStateMachine.State.Evade || predator == null;
    }

    private Vector2 FleePredator()
    {
        float desiredDistance = 10.0f;
        var enemyPoint = predator.transform.position + (transform.position - predator.transform.position).normalized * desiredDistance;
        var point = enemyPoint;
        if (Vector2.Distance(transform.position.AsVector2(), homePosition) < desiredDistance) point = homePosition;
        return point;
    }

    public void SetPredator(GameObject newPredator)
    {
        predator = newPredator;
    }
}
