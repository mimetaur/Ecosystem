using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Quest : MonoBehaviour
{
    public GameObject goalType;
    public float searchRadius;
    public float wanderRadius;
    public float timeBetweenMissions = 8.0f;

    private QuestAIStateMachine machine;
    private GameObject gemMode;
    private SpriteRenderer gemModeSr;

    private Bounds homeBounds;
    private Vector2 homePosition;
    private GameObject target;

    // Use this for initialization
    void Start()
    {
        machine = GetComponent<QuestAIStateMachine>();

        gemMode = (GameObject)Instantiate(Resources.Load("Gem"));
        gemMode.transform.parent = this.transform;
        gemMode.SetActive(false);

        homeBounds = GameObject.Find("Priest Spawn").GetComponent<Collider2D>().bounds;
        homePosition = homeBounds.center.AsVector2();
    }

    // Update is called once per frame
    void Update()
    {

        if (machine.CurrentState == QuestAIStateMachine.State.Evade)
        {
            return;
        }
        else if (machine.CurrentState == QuestAIStateMachine.State.ReturnHomeWithGem)
        {
            gemMode.SetActive(true);
            gemMode.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

            if (homeBounds.Contains(transform.position.AsVector2()))
            {
                print("Player is back inside house");
                machine.CurrentState = QuestAIStateMachine.State.Idle;
                // gemModeSr.DOFade(0, timeBetweenMissions).OnComplete(WaitToSeek);
                gemMode.transform.DOScale(0, timeBetweenMissions).OnComplete(WaitToSeek);

                // Invoke("WaitToSeek", timeBetweenMissions);
            }
        }
        else if (machine.CurrentState == QuestAIStateMachine.State.Seek || machine.CurrentState == QuestAIStateMachine.State.Wander)
        {
            SearchForQuestItems();
        }
        else if (machine.CurrentState == QuestAIStateMachine.State.Idle)
        {
            // do nothing
        }
    }

    private void WaitToSeek()
    {
        print("Leaving the house!");
        machine.CurrentState = QuestAIStateMachine.State.Wander;
        gemMode.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);
        gemMode.SetActive(false);
    }

    private void SearchForQuestItems()
    {
        var gems = GameObject.FindGameObjectsWithTag(goalType.tag);
        target = SearchForNearbyItems(gems);
        if (target != null)
        {
            machine.CurrentState = QuestAIStateMachine.State.Seek;
        }
        else
        {
            machine.CurrentState = QuestAIStateMachine.State.Wander;
        }
    }

    private GameObject SearchForNearbyItems(GameObject[] targets)
    {
        GameObject searchResult = null;

        if (targets != null)
        {
            var target = GameUtils.FindClosestWithinThreshold(this.gameObject, targets, searchRadius);
            if (target != null)
            {
                searchResult = target;
            }
        }

        return searchResult;
    }

    public Vector2 TargetPosition
    {
        get
        {
            // default is to wander
            var pos = RandomPointInRadius();
            if (machine.CurrentState == QuestAIStateMachine.State.ReturnHomeWithGem)
            {
                pos = homePosition;
            }
            else if (machine.CurrentState == QuestAIStateMachine.State.Seek)
            {
                pos = target.transform.position.AsVector2();
            }
            return pos;
        }
    }

    private Vector2 RandomPointInRadius()
    {
        var point = Random.insideUnitCircle * wanderRadius;
        point += transform.position.AsVector2();
        return point;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        CheckIfQuestGoal(collider.gameObject);
    }

    private void CheckIfQuestGoal(GameObject other)
    {
        if (other != null && other.tag == "QuestGoal")
        {
            machine.CurrentState = QuestAIStateMachine.State.ReturnHomeWithGem;
            Destroy(other);
            print("Got Quest Goal");
            print("Returning home - " + machine.CurrentState + " " + homePosition);
        }
    }
}
