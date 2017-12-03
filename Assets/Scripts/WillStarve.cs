using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillStarve : MonoBehaviour
{
    public float canGoWithoutFoodForSecs;

    private float hasGoneWithoutFoodForSecs;

    // Use this for initialization
    void Start()
    {
        hasGoneWithoutFoodForSecs = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        hasGoneWithoutFoodForSecs += Time.deltaTime;

        if (hasGoneWithoutFoodForSecs > canGoWithoutFoodForSecs) {
            Destroy(this.gameObject);
        }
    }

    public void DidEat() {
        hasGoneWithoutFoodForSecs = 0.0f;
    }
}
