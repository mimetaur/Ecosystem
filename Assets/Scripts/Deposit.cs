using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Deposit : MonoBehaviour
{

    public Transform coin;

    [Range(0, 100)]
    public float percentageOfDropping;

    [Range(0, 30)]
    public float initialDelay;

    [Range(0, 20)]
    public float rate;

    private bool isInBounds = false;

    private EatSounds eatSounds;


    // Use this for initialization
    void Start()
    {
        InvokeRepeating("DropABomb", initialDelay, rate);
        eatSounds = GetComponent<EatSounds>();
    }

    void DropABomb()
    {
        if (!isInBounds) return;

        float v = Random.value;
        float num = GameUtils.Map(v, 0.0f, 1.0f, 0, 100);
        if (num < percentageOfDropping)
            CreateCoin();
    }

    void CreateCoin()
    {
        if (eatSounds != null) eatSounds.Play();
        Instantiate(coin, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "World")
        {
            isInBounds = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "World")
        {
            isInBounds = false;
        }
    }
}
