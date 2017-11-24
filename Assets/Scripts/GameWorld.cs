using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{

    public GameObject world;
    private Bounds gameBounds;

    // Use this for initialization
    void Start()
    {
        gameBounds = world.GetComponent<CompositeCollider2D>().bounds;
    }

    Bounds GetWorldBounds()
    {
        return gameBounds;
    }

    bool IsOutside(Vector2 v)
    {
        return gameBounds.Contains(v.AsVector3());
    }

    bool IsIside(Vector2 v)
    {
        return !gameBounds.Contains(v.AsVector3());
    }
}
