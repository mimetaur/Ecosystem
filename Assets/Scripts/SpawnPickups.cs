using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{
    public GameObject pickup;

    [Range(0, 100)]
    public int initialPickups = 12;

    [Range(0, 100)]
    public int maxPickups;

    public float initialDelay;
    public float rate;


    private GameWorld world;
    private int numPickups;

    void Start()
    {
        world = GameManager.instance.world;

        for (numPickups = 0; numPickups < initialPickups; numPickups++)
        {
            SpawnPickup();
        }

        InvokeRepeating("SpawnPickup", initialDelay, rate);
    }

    private void SpawnPickup()
    {
        SpawnPickupAt(world.GetRandomUnblockedLocation());
    }

    private void SpawnPickupAt(Vector2 pos)
    {
        Instantiate(pickup, pos, Quaternion.identity);
    }
}

