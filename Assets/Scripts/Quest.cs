using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public GameObject goal;
    public float searchRadius;
    public float wanderRadius;


    private Vector2 targetPosition;
    private AIStateMachine machine;
    private GameObject gemMode;
    private Vector2 homePosition;

    // Use this for initialization
    void Start()
    {
        machine = GetComponent<AIStateMachine>();

        gemMode = (GameObject)Instantiate(Resources.Load("Gem"));
        gemMode.transform.parent = this.transform;
        gemMode.SetActive(false);

        var homeBounds = GameObject.Find("Priest Spawn").GetComponent<Collider2D>().bounds;
        homePosition = homeBounds.center.AsVector2();
    }

    // Update is called once per frame
    void Update()
    {
        // print("Current player state: " + machine.currentState);
        gemMode.SetActive(false);

        if (machine.currentState == AIStateMachine.State.Flee)
        {
            return;
        }
        else if (machine.currentState == AIStateMachine.State.ReturnHomeWithGem)
        {
            gemMode.SetActive(true);
            gemMode.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            targetPosition = homePosition;
        }
        else if (machine.currentState == AIStateMachine.State.Seek)
        {
            var gems = GameObject.FindGameObjectsWithTag(goal.tag);
            Search(gems);
        }
        else
        {
            targetPosition = GetRandomPoint();
        }
    }

    private void Search(GameObject[] targets)
    {
        if (machine.currentState != AIStateMachine.State.Seek) return;

        if (targets != null)
        {
            var target = GameUtils.FindClosestWithinThreshold(this.gameObject, targets, searchRadius);
            if (target != null)
            {
                targetPosition = target.transform.position.AsVector2();
                machine.currentState = AIStateMachine.State.Seek;
            }
            else
            {
                machine.currentState = AIStateMachine.State.Wander;
            }
        }
        else
        {
            machine.currentState = AIStateMachine.State.Wander;
        }
    }

    public Vector2 TargetPosition()
    {
        return targetPosition;
    }

    private Vector2 GetRandomPoint()
    {
        var point = Random.insideUnitCircle * wanderRadius;
        point += transform.position.AsVector2();
        return point;
    }
    public Vector2 HomePosition()
    {
        return homePosition;
    }

    private void ReturnHome()
    {
        print(machine.currentState);
        machine.currentState = AIStateMachine.State.ReturnHomeWithGem;
        targetPosition = homePosition;
        print("Returning home - " + machine.currentState + " " + homePosition);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        CheckIfQuestGoal(collider.gameObject);
    }

    private void CheckIfQuestGoal(GameObject other)
    {
        if (other != null && other.tag == "QuestGoal")
        {
            print("Got Quest Goal");
            ReturnHome();

            Destroy(other);
        }
    }
}
