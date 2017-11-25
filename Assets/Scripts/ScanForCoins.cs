using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanForCoins : MonoBehaviour
{
    public float threshold;

    AIStateMachine machine;

    public Vector2 coinLocation;

    private void Start()
    {
        machine = GetComponent<AIStateMachine>();
    }

    void Update()
    {
        if (machine.currentState != AIStateMachine.State.Wander) return;
            
        var coins = GameObject.FindGameObjectsWithTag("Pickup");
        for (int i = 0; i < coins.Length; i++)
        {
            var coin = coins[i];
            var coinPos = coin.transform.position.AsVector2();
            var myPos = transform.position.AsVector2();

            if (Vector2.Distance(coinPos, myPos) < threshold) {
                machine.currentState = AIStateMachine.State.Seek;
            }
        }
    }
}
