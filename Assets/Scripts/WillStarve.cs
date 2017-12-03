using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillStarve : MonoBehaviour
{
    public float canGoWithoutFoodForSecs;

    private float hasGoneWithoutFoodForSecs;

    private OnDie onDie;

    // Use this for initialization
    void Start()
    {
        hasGoneWithoutFoodForSecs = 0.0f;
        onDie = GetComponent<OnDie>();
    }

    // Update is called once per frame
    void Update()
    {
        hasGoneWithoutFoodForSecs += Time.deltaTime;

        if (hasGoneWithoutFoodForSecs > canGoWithoutFoodForSecs) {
            onDie.Die();
            Destroy(this.gameObject);
        }
    }

    public void DidEat() {
        hasGoneWithoutFoodForSecs = 0.0f;
    }
}
