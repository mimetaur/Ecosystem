using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanForCoins : MonoBehaviour
{
    public float threshold;

    AIStateMachine machine;

    public Vector2 closestCoinLocation;

    private void Start()
    {
        machine = GetComponent<AIStateMachine>();
    }

    void Update()
    {            
        var coins = GameObject.FindGameObjectsWithTag("Pickup");
        var closestCoin = GetClosest(coins);
        if (closestCoin != null) {
            var coinPos = closestCoin.transform.position.AsVector2();
            var myPos = transform.position.AsVector2();

            if (Vector2.Distance(coinPos, myPos) < threshold)
            {
                machine.currentState = AIStateMachine.State.Seek;
                closestCoinLocation = coinPos;
            } else {
                machine.currentState = AIStateMachine.State.Wander;
            }
        } else {
            machine.currentState = AIStateMachine.State.Wander;
        }
    }


    // Example algorithm sourced from:
    // https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    private GameObject GetClosest(GameObject[] things)
    {
        GameObject closestThing = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject thing in things)
        {
            Vector3 diff = thing.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestThing = thing;
                distance = curDistance;
            }
        }
        return closestThing;
    }
}
