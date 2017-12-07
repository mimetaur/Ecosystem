using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnPickups : MonoBehaviour
{
    public GameObject pickup;

    [Range(0, 100)]
    public int initialPickups = 12;

    [Range(0, 100)]
    public int maxPickups;

    public bool spawnRepeating = true;
    public float initialDelay;
    public float rate;
    public float spawnTime = 0.5f;


    private GameWorld world;
    private int numPickups;

    void Start()
    {
        world = GameManager.instance.world;

        for (numPickups = 0; numPickups < initialPickups; numPickups++)
        {
            SpawnPickup();
        }

        if (spawnRepeating) InvokeRepeating("SpawnPickup", initialDelay, rate);
    }

    private void SpawnPickup()
    {
        SpawnPickupAt(world.GetRandomUnblockedLocation());
    }

    private void SpawnPickupAt(Vector2 pos)
    {
        GameObject newPickup = (GameObject)Instantiate(pickup, pos, Quaternion.identity) as GameObject;
        newPickup.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        newPickup.transform.DOScale(1.0f, spawnTime);
    }
}

