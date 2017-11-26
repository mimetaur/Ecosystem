using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameWorld))]
public class SpawnMushrooms : MonoBehaviour
{
    [Range(1, 100)]
    public int numMushrooms = 12;

    public Transform mushroom;

    private GameWorld gameWorld;

    void Start()
    {
        gameWorld = GetComponent<GameWorld>();

        for (int i = 0; i < numMushrooms; i++)
        {
            SpawnMushroomAt(gameWorld.GetRandomUnblockedLocation());
        }
    }

    private void SpawnMushroomAt(Vector2 pos)
    {
        Instantiate(mushroom, pos, Quaternion.identity);
    }
}

