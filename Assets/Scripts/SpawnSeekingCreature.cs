using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(GameWorld))]
public class SpawnSeekingCreature : MonoBehaviour
{

    [Range(0, 20)]
    public int numCreatures;

    public GameObject creaturePrefab;

    private GameWorld world;

    // Use this for initialization
    void Start()
    {
        world = GameManager.instance.world;

        for (int i = 0; i < numCreatures; i++)
        {
            SpawnCreatureAt(world.GetRandomUnblockedLocation());
        }
    }


    void SpawnCreatureAt(Vector2 location)
    {
        var newCreature = Instantiate(creaturePrefab, location, Quaternion.identity) as GameObject;
        newCreature.name = string.Format("{0}{1}", creaturePrefab.name, newCreature.GetInstanceID());

        var ai = newCreature.GetComponent<IAstarAI>();
        ai.maxSpeed = Random.Range(1.0f, 3.0f);
    }
}
