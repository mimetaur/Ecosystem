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
        var pickups = GameObject.FindGameObjectsWithTag("Pickup");

        var coins = new List<GameObject>();

        for (int i = 0; i < pickups.Length; i++) {
            if (pickups[i].name.Contains("Deposit"))
                coins.Add(pickups[i]);
        }

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
    private GameObject GetClosest(List<GameObject> things)
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
